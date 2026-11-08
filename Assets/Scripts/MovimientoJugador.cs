using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
// Referencia al Animator: permite cambiar las animaciones del personaje.
    Animator animator;
// Referencia al componente que controla las vidas, la muerte y la reaparicion.
    VidasPJ vidas;
// Accion de entrada configurada en el Input System para leer el movimiento.
    InputAction movimiento;
// Accion de entrada configurada en el Input System para detectar el salto.
    InputAction saltar;
// Rigidbody2D del jugador: controla su movimiento mediante fisicas 2D.
    Rigidbody2D rb2d;
// Collider con forma de capsula: se usa para detectar suelo y paredes.
    CapsuleCollider2D collider2d;

// Capas que se consideran suelo valido para saltar.
    [SerializeField] private LayerMask jumpableGround;
// Capas que se consideran paredes para realizar el salto contra ellas.
    [SerializeField] private LayerMask wallLayer;

// Guarda el valor de movimiento leido del teclado/mando (horizontal y vertical).
    Vector2 valorMovimiento;

// Velocidad horizontal del jugador.
    float velocidad = 5f;
// Fuerza vertical aplicada al saltar.
    float fuerzaSalto = 7f;
// Numero de saltos disponibles; empieza en 2 para permitir doble salto.
    int saltosRestantes = 2;
// Numero que identifica la animacion actual en el Animator.
    int estado = 0;

// Indica si el jugador estaba tocando el suelo en el fotograma anterior.
    bool estabaEnSuelo = false;
// Indica si el jugador esta realizando un salto contra una pared.
    bool wallJumping = false;
// Evita activar repetidamente la animacion de dano mientras sigue recibiendolo.
    bool recibiendoDano = false;

// Start se ejecuta una sola vez al comenzar el objeto o la escena.
    void Start()
    {
// Busca la accion llamada Move en la configuracion del Input System.
        movimiento = InputSystem.actions.FindAction("Move");
// Busca la accion llamada Jump en la configuracion del Input System.
        saltar = InputSystem.actions.FindAction("Jump");

// Obtiene el Rigidbody2D que esta en este mismo GameObject.
        rb2d = GetComponent<Rigidbody2D>();
// Obtiene el collider de capsula del jugador.
        collider2d = GetComponent<CapsuleCollider2D>();
// Obtiene el Animator del jugador para controlar sus animaciones.
        animator = GetComponent<Animator>();
// Obtiene el script VidasPJ del mismo objeto.
        vidas = GetComponent<VidasPJ>();
    }


// Update se ejecuta una vez por fotograma; aqui se procesa el control del jugador.
    void Update()
    {

// Si el jugador esta muerto o reapareciendo, no debe moverse ni saltar.
        if (vidas.muerto || vidas.reapareciendo)
        {
// Detiene por completo la velocidad del jugador.
            rb2d.linearVelocity = Vector2.zero;
// Sale de Update para no ejecutar el resto de controles.
            return;
        }

// Lee el valor actual de la accion Move (por ejemplo, A/D o flechas).
        valorMovimiento = movimiento.ReadValue<Vector2>();

// Aplica el movimiento horizontal multiplicando la entrada por la velocidad.
        rb2d.linearVelocityX = valorMovimiento.x * velocidad;

// Cambia la orientacion del sprite segun la direccion del movimiento.
        GirarJugador();

// Comprueba si toca el suelo y ademas esta descendiendo o quieto.
        bool tocaSuelo = TocaSuelo()
            && rb2d.linearVelocityY <= 0;

// Comprueba si hay una pared a la izquierda o a la derecha.
        int direccionPared = DireccionPared();

// Si esta en el suelo, reinicia las condiciones de salto.
        if (tocaSuelo)
        {
// Recupera los dos saltos disponibles al tocar el suelo.
            saltosRestantes = 2;
// Cancela el estado de salto contra pared al aterrizar.
            wallJumping = false;
// Actualiza la animacion para mostrar quieto o caminar.
            CambiarAnimacion();
        }

// Guarda el estado de suelo para poder compararlo en otros calculos.
        estabaEnSuelo = tocaSuelo;

// Comprueba si se ha pulsado el boton de salto en este fotograma.
        if (saltar.WasPressedThisFrame())
        {
// Si esta junto a una pared y no esta en el suelo, intenta saltar contra ella.
            if (direccionPared != 0 && !tocaSuelo)
            {
// Ejecuta el salto en direccion contraria a la pared detectada.
                SaltoPared(direccionPared);
            }
// Si no corresponde un salto contra pared, comprueba si quedan saltos normales.
            else if (saltosRestantes > 0)
            {
// Realiza un salto normal o el segundo salto del doble salto.
                SaltoNormal();
            }
        }
    }


// Devuelve true si el collider del jugador detecta suelo debajo.
    private bool TocaSuelo()
    {
// BoxCast lanza una caja imaginaria desde el centro del collider.
        return Physics2D.BoxCast(
// Punto desde el que comienza la deteccion: centro del collider.
            collider2d.bounds.center,
// Tamano de la caja de deteccion: igual al collider del jugador.
            collider2d.bounds.size,
            0f,
// Direccion de la deteccion: hacia abajo.
            Vector2.down,
// Distancia corta que sobresale por debajo del collider.
            .1f,
// Solo detecta objetos incluidos en la LayerMask jumpableGround.
            jumpableGround
        );
    }

// Devuelve -1 si hay pared a la izquierda, 1 si hay pared a la derecha y 0 si no hay pared.
    private int DireccionPared()
    {
// Comprueba si hay una pared a la izquierda del jugador.
        if (Physics2D.BoxCast(
// Centro de la caja de deteccion.
            collider2d.bounds.center,
// Tamano de la caja de deteccion.
            collider2d.bounds.size,
            0f,
// Direccion de la comprobacion: izquierda.
            Vector2.left,
// Distancia de deteccion.
            .1f,
// Capas que cuentan como pared.
            wallLayer))
        {
// Devuelve -1 para indicar que la pared esta a la izquierda.
            return -1;
        }

// Si no encontro pared a la izquierda, comprueba la derecha.
        if (Physics2D.BoxCast(
// Centro de la caja de deteccion.
            collider2d.bounds.center,
// Tamano de la caja de deteccion.
            collider2d.bounds.size,
            0f,
// Direccion de la comprobacion: derecha.
            Vector2.right,
// Distancia de deteccion.
            .1f,
// Capas que cuentan como pared.
            wallLayer))
        {
// Devuelve 1 para indicar que la pared esta a la derecha.
            return 1;
        }

// Cero significa que no se detecto ninguna pared.
        return 0;
    }


// Orienta visualmente al jugador segun la direccion horizontal.
    void GirarJugador()
    {
// Si se mueve hacia la izquierda...
        if (valorMovimiento.x < 0)
        {
// ...invierte el eje X para que el sprite mire a la izquierda.
            transform.localScale = new Vector3(-1, 1, 1);
        }
// Si se mueve hacia la derecha...
        else if (valorMovimiento.x > 0)
        {
// ...deja la escala positiva para mirar a la derecha.
            transform.localScale = new Vector3(1, 1, 1);
        }
    }


// Decide si la animacion debe ser de reposo o de movimiento.
    void CambiarAnimacion()
    {
// Si existe movimiento horizontal...
        if (valorMovimiento.x != 0)
        {
// ...usa el estado 1, que corresponde a caminar/correr segun el Animator.
            estado = 1;
        }
        else
        {
// Si no hay movimiento horizontal...
            estado = 0;
// ...usa el estado 0, normalmente la animacion de reposo.
        }

// Envia el valor de estado al parametro entero llamado estado del Animator.
        animator.SetInteger("estado", estado);
    }


// Realiza un salto alejandose de la pared detectada.
    void SaltoPared(int direccionPared)
    {
// Establece simultaneamente la velocidad horizontal y vertical del jugador.
        rb2d.linearVelocity = new Vector2(
// Impulso horizontal contrario a la pared.
            -direccionPared * velocidad,
// Impulso vertical del salto.
            fuerzaSalto
        );

        saltosRestantes = 0;

        estado = 4;
// Actualiza el parametro estado del Animator.
        animator.SetInteger("estado", estado);

// Si la pared esta a la izquierda...
        if (direccionPared < 0)
        {
// ...orienta al jugador hacia la derecha, alejandolo de ella.
            transform.localScale = new Vector3(1, 1, 1);
        }
// Si la pared esta a la derecha...
        else
        {
// ...orienta al jugador hacia la izquierda, alejandolo de ella.
            transform.localScale = new Vector3(-1, 1, 1);
        }

// Marca que el jugador esta en el proceso de salto contra pared.
        wallJumping = true;
    }

// Realiza un salto normal o un doble salto.
    void SaltoNormal()
    {
// Resta uno al numero de saltos disponibles.
        saltosRestantes--;

// Aplica la fuerza vertical del salto sin modificar aqui la velocidad horizontal.
        rb2d.linearVelocityY = fuerzaSalto;

// Si queda un salto, significa que acaba de hacer el primer salto.
        if (saltosRestantes == 1)
        {
// Estado 2: animacion del primer salto.
            estado = 2;
        }
// Si no queda ningun salto, acaba de realizar el segundo salto.
        else
        {
// Estado 3: animacion del doble salto.
            estado = 3;
        }

// Actualiza la animacion con el estado seleccionado.
        animator.SetInteger("estado", estado);
    }

    // ANIMACION DANO

// Metodo publico que puede llamar un evento de animacion al terminar el clip de dano.
    public void TerminarDano()
    {
// Indica que la animacion/estado de dano ya ha terminado.
        recibiendoDano = false;

// Si el jugador sigue moviendose...
        if (valorMovimiento.x != 0)
        {
// ...vuelve a la animacion de movimiento.
            estado = 1;
        }
// Si no se mueve...
        else
        {
// ...vuelve a la animacion de reposo.
            estado = 0;
        }

// Actualiza el Animator con el estado normal correspondiente.
        animator.SetInteger("estado", estado);

        vidas.ResolverDanio();
    }


// ANIMACION DANO
void OnCollisionStay2D(Collision2D collision)
{
    if (collision.gameObject.CompareTag("Obstaculos") && !recibiendoDano)
    {
        recibiendoDano = true;

        vidas.RecibirDanio(1);

        estado = 5;
        animator.SetInteger("estado", estado);
    }
}


// Unity llama a este metodo cuando termina el contacto fisico con otro collider.
    //SALIR DE LA ANIMACION DE DANO
// Detecta cuando el jugador deja de tocar un objeto.
    void OnCollisionExit2D(Collision2D collision)
    {
// Comprueba que el objeto del que se separa tiene la etiqueta Pinchos.
        if (collision.gameObject.CompareTag("Obstaculos"))
        {
// Desactiva la marca de dano al dejar de tocar los pinchos.
            recibiendoDano = false;
        }
    }
}