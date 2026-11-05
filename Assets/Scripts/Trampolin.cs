using UnityEngine;

public class Trampolin : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("PinkMan ha tocado el trampolín");

            if (animator != null)
            {
                animator.SetInteger("estado", 1);
            }

            Rigidbody2D rb2d = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb2d != null)
            {
                rb2d.linearVelocityY = 12f;
            }
        }
    }

    public void TerminarSalto()
    {
        animator.SetInteger("estado", 0);
    }
}