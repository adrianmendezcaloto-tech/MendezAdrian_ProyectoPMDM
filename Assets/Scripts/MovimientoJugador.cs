using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    Animator animator;
    InputAction movimiento;
    InputAction saltar;
    Rigidbody2D rb2d;
    CapsuleCollider2D collider2d;
    [SerializeField] private LayerMask jumpableGround;

    Vector2 valorMovimiento;

    float velocidad = 5f;
    float fuerzaSalto = 7f;
    int saltosRestantes = 1;
    int estado = 0;
    bool estabaEnSuelo = false;
    void Start()
    {
        movimiento = InputSystem.actions.FindAction("Move");
        saltar = InputSystem.actions.FindAction("Jump");

        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<CapsuleCollider2D>();

        animator = GetComponent<Animator>();
    }

    void Update()
    {
        valorMovimiento = movimiento.ReadValue<Vector2>();
        rb2d.linearVelocityX = valorMovimiento.x * velocidad;

        GirarJugador();

        bool tocaSuelo = TocaSuelo() && rb2d.linearVelocityY <= 0;

        if (tocaSuelo && !estabaEnSuelo)
        {
            saltosRestantes = 2;
            CambiarAnimacion();
        }

        estabaEnSuelo = tocaSuelo;

        if (saltar.WasPressedThisFrame() && saltosRestantes > 0)
        {
            saltosRestantes--;
            rb2d.linearVelocityY = fuerzaSalto;
            estabaEnSuelo = false;

            if (saltosRestantes == 1)
            {
                estado = 2;
            }
            else
            {
                estado = 3;
            }

            animator.SetInteger("estado", estado);
        }
    }
    private bool TocaSuelo()
    {
        return Physics2D.BoxCast(
            collider2d.bounds.center,
            collider2d.bounds.size,
            0f,
            Vector2.down,
            .1f,
            jumpableGround
        );
    }

    void GirarJugador()
    {
        if (valorMovimiento.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (valorMovimiento.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    void CambiarAnimacion()
    {
        if (valorMovimiento.x != 0)
        {
            estado = 1;
        }
        else
        {
            estado = 0;
        }

        animator.SetInteger("estado", estado);
    }
}
