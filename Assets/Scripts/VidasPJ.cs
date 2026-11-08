using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Gestiona las vidas del jugador, los puntos de reaparicion y el fin de la partida.
public class VidasPJ : MonoBehaviour
{
// Numero maximo de vidas con el que comienza el jugador.
    public int vidaMaxima = 3;
// Indica si el jugador ya ha muerto.
    public bool muerto = false;
// Indica si el jugador esta ejecutando el proceso de reaparicion.
    public bool reapareciendo = false;
// Bloquea la recepcion de mas dano mientras se resuelve el dano actual.
    public bool enDano = false;
// Referencia al texto de TextMeshPro que muestra las vidas en pantalla.
    public TMP_Text textoVidas;

// Cantidad de vidas que tiene actualmente el jugador.
    int vidaActual;
// Posicion a la que volvera el jugador cuando reaparezca.
    Vector3 puntoReaparicion;
// Referencia al Rigidbody2D del jugador.
    Rigidbody2D rb;
// Referencia al Animator del jugador.
    Animator animator;

// Inicializa los datos y referencias al comenzar la escena.
    void Start()
    {
// Empieza con todas las vidas disponibles.
        vidaActual = vidaMaxima;
// Obtiene el Rigidbody2D del jugador.
        rb = GetComponent<Rigidbody2D>();
// Obtiene el Animator del jugador.
        animator = GetComponent<Animator>();
// Guarda la posicion inicial como primer punto de reaparicion.
        puntoReaparicion = transform.position;
// Muestra en pantalla el numero inicial de vidas.
        ActualizarVidas();
    }

// Guarda una nueva posicion como punto de control.
    public void GuardarCheckpoint(Vector3 posicion)
    {
// Actualiza el lugar al que volvera el jugador tras recibir dano mortal para su estado actual.
        puntoReaparicion = posicion;
    }

// Resta vidas al jugador y activa el proceso de dano.
    public void RecibirDanio(int cantidad)
    {
// Si esta muerto, reapareciendo o ya procesando dano, ignora esta llamada.
        if (muerto || reapareciendo || enDano) return;

// Resta la cantidad de dano recibida a las vidas actuales.
        vidaActual -= cantidad;
// Bloquea nuevas llamadas de dano hasta resolver la animacion.
        enDano = true;
// Actualiza el contador visual de vidas.
        ActualizarVidas();
    }

// Se llama al terminar la animacion de dano para decidir si muere o reaparece.
    // Lo llama TerminarDano() al acabar la animacion Damage
// Resuelve el dano una vez terminada su animacion.
public void ResolverDanio()
{
// Permite volver a recibir dano en el futuro.
    enDano = false;
// Detiene cualquier movimiento del Rigidbody2D.
    rb.linearVelocity = Vector2.zero;
// Desactiva temporalmente la simulacion fisica del jugador.
    rb.simulated = false;

// Si las vidas han llegado a cero o menos...
    if (vidaActual <= 0)
    {
// ...marca al jugador como muerto.
        muerto = true;
// ...activa el estado 6, correspondiente a la animacion de muerte.
        animator.SetInteger("estado", 6);
    }
// Si todavia le quedan vidas...
    else
    {
// ...marca que esta reapareciendo.
        reapareciendo = true;
// ...activa el estado 7, correspondiente a la animacion de reaparicion.
        animator.SetInteger("estado", 7);
// ...lo coloca en el ultimo punto de control guardado.
        transform.position = puntoReaparicion;
    }
}
// Evento que debe ejecutarse al final del clip de animacion Respawn.
    // Evento al final del clip Respawn
// Termina el proceso de reaparicion.
 public void TerminarReaparecer()
{
// Reactiva la simulacion fisica del Rigidbody2D.
    rb.simulated = true;
// Indica que ya ha terminado de reaparecer.
    reapareciendo = false;

// Devuelve la animacion al estado 0 (normal/reposo).
    animator.SetInteger("estado", 0);
}

// Evento que debe ejecutarse al final del clip Death.
    // Evento al final del clip Death
// Metodo que se ejecuta al terminar la animacion de muerte.
    public void FinDelJuego()
    {
// Carga la escena llamada Muerte.
        SceneManager.LoadScene("Muerte");
    }

// Actualiza el texto que muestra las vidas.
    void ActualizarVidas()
{
// Convierte el numero de vidas a texto y lo asigna al componente TMP_Text.
    textoVidas.text = vidaActual.ToString();
}
}