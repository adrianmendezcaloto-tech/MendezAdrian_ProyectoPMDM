
using UnityEngine;

public class Manzanas : MonoBehaviour
{
    Animator animator;
    int estado = 0;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            estado = 1;
            animator.SetInteger("estado", 1);
            FindFirstObjectByType<EstadisticasMundo>().RecogerManzana();
        }
    }

    public void DestruirManzana()
    {
        Destroy(gameObject);
    }
}
