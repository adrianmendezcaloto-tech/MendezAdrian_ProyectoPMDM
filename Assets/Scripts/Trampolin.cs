using UnityEngine;

// Este componente anima el trampolin y lanza al jugador hacia arriba al tocarlo.
public class Trampolin : MonoBehaviour
{
// Referencia al Animator del trampolin.
    Animator animator;

// Start se ejecuta una vez al iniciar el objeto.
    void Start()
    {
// Obtiene el Animator del mismo GameObject.
        animator = GetComponent<Animator>();
    }

// Unity llama a este metodo cuando comienza una colision 2D.
    void OnCollisionEnter2D(Collision2D collision)
    {
// Comprueba que quien ha tocado el trampolin es el jugador.
        if (collision.gameObject.CompareTag("Player"))
        {
            

// Evita intentar cambiar la animacion si el trampolin no tiene Animator.
            if (animator != null)
            {
// Activa el estado 1 de la animacion del trampolin (por ejemplo, comprimirse).
                animator.SetInteger("estado", 1);
            }

// Busca el Rigidbody2D del objeto que ha chocado; normalmente, el jugador.
            Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();

// Comprueba que el jugador tiene Rigidbody2D antes de modificarlo.
            if (rb2d != null)
            {
// Le da velocidad vertical hacia arriba para impulsarlo.
                rb2d.linearVelocityY = 12f;
            }
        }
    }

// Metodo publico pensado para llamarse mediante un evento al terminar la animacion.
    public void TerminarSalto()
    {
// Devuelve el Animator al estado 0, normalmente la posicion normal del trampolin.
        animator.SetInteger("estado", 0);
    }
}