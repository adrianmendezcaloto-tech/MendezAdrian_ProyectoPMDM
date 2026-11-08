using UnityEngine;

// Activa la animacion del checkpoint y guarda su posicion para reaparecer alli.
public class CheckPoint : MonoBehaviour
{
// Referencia al Animator del punto de control.
    Animator animator;
// Numero que identifica el estado de animacion del checkpoint.
    int estado;

// Se ejecuta una vez al iniciar el objeto.
    void Start()
    {
// Obtiene el Animator del checkpoint.
        animator = GetComponent<Animator>();
    }

// Se ejecuta cuando otro Collider2D entra en el trigger del checkpoint.
    void OnTriggerEnter2D(Collider2D collision)
    {
// Solo activa el punto de control cuando entra el jugador.
        if (collision.gameObject.CompareTag("Player"))
        {
// Selecciona el estado 1, que representa el checkpoint activado.
            estado = 1;
// Envia ese estado al Animator para reproducir la animacion.
            animator.SetInteger("estado", estado);

// Obtiene el componente de vidas del jugador que ha entrado.
            VidasPJ vidas = collision.GetComponent<VidasPJ>();
// Comprueba que el jugador tiene el script VidasPJ.
            if (vidas != null)
            {
// Guarda la posicion de este checkpoint como nuevo punto de reaparicion.
                vidas.GuardarCheckpoint(transform.position);
            }
        }
    }
}