using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    Animator animator;
    int estado;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            estado = 1;
            animator.SetInteger("estado", estado);

            VidasPJ vidas = collision.GetComponent<VidasPJ>();
            if (vidas != null)
            {
                vidas.GuardarCheckpoint(transform.position);
            }
        }
    }
}