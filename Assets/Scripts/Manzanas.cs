using UnityEngine;

// Detecta cuando el jugador recoge una manzana, actualiza el contador y la elimina.
public class Manzanas : MonoBehaviour
{
// Referencia al Animator de la manzana.
    Animator animator;

// Inicializa la referencia al comenzar el objeto.
    void Start()
    {
// Obtiene el Animator de la manzana.
        animator = GetComponent<Animator>();
    }

// Unity llama a este metodo cuando otro collider entra en el trigger de la manzana.
    void OnTriggerEnter2D(Collider2D collision)
    {
// Comprueba que el objeto que entra es el jugador.
        if (collision.gameObject.CompareTag("Player"))
        {
// Activa el estado 1 de la animacion de recogida.
            animator.SetInteger("estado", 1);
// Busca el primer EstadisticasMundo de la escena y suma una manzana.
            FindFirstObjectByType<EstadisticasMundo>().RecogerManzana();
        }
    }

// Metodo que puede llamarse desde un evento de animacion al terminar la recogida.
    public void DestruirManzana()
    {
// Destruye el GameObject de la manzana, eliminandolo de la escena.
        Destroy(gameObject);
    }
}