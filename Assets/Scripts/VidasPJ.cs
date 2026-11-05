using UnityEngine;
using UnityEngine.SceneManagement;

public class VidasPJ : MonoBehaviour
{
    public int vidaMaxima = 3;
    public bool muerto = false;
    public bool reapareciendo = false;
    public bool enDano = false;

    int vidaActual;
    Vector3 puntoReaparicion;
    Rigidbody2D rb;
    Animator animator;

    void Start()
    {
        vidaActual = vidaMaxima;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        puntoReaparicion = transform.position;
    }

    public void GuardarCheckpoint(Vector3 posicion)
    {
        puntoReaparicion = posicion;
    }

    public void RecibirDanio(int cantidad)
    {
        if (muerto || reapareciendo || enDano) return;

        vidaActual -= cantidad;
        enDano = true;
        Debug.Log("Vida: " + vidaActual);
    }

    // Lo llama TerminarDano() al acabar la animación Damage
    public void ResolverDanio()
    {
        enDano = false;
        rb.linearVelocity = Vector2.zero;
        rb.simulated = false;

        if (vidaActual <= 0)
        {
            muerto = true;
            animator.SetInteger("estado", 6);
        }
        else
        {
            reapareciendo = true;
            transform.position = puntoReaparicion;
            animator.SetInteger("estado", 7);
        }
    }

    // Evento al final del clip Respawn
 public void TerminarReaparecer()
{
    rb.simulated = true;
    reapareciendo = false;

    animator.SetInteger("estado", 0);
}

    // Evento al final del clip Death
    public void FinDelJuego()
    {
        SceneManager.LoadScene("Muerte");
    }
}