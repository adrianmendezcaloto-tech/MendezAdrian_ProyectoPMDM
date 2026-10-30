
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
    [SerializeField] private LayerMask wallLayer;

    Vector2 valorMovimiento;

    float velocidad = 5f;
    float fuerzaSalto = 7f;
    int saltosRestantes = 2;
    int estado = 0;

    bool estabaEnSuelo = false;
    bool wallJumping = false;
    bool recibiendoDano = false;

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

        valorMovimiento = movimiento.ReadValue<Vector2>();

        rb2d.linearVelocityX = valorMovimiento.x * velocidad;

        GirarJugador();

        bool tocaSuelo = TocaSuelo()
            && rb2d.linearVelocityY <= 0;

        int direccionPared = DireccionPared();

        if (tocaSuelo)
        {
            saltosRestantes = 2;
            wallJumping = false;
            CambiarAnimacion();
        }

        estabaEnSuelo = tocaSuelo;

        if (saltar.WasPressedThisFrame())
        {
            if (direccionPared != 0 && !tocaSuelo)
            {
                SaltoPared(direccionPared);
            }
            else if (saltosRestantes > 0)
            {
                SaltoNormal();
            }
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

    private int DireccionPared()
    {
        if (Physics2D.BoxCast(
            collider2d.bounds.center,
            collider2d.bounds.size,
            0f,
            Vector2.left,
            .1f,
            wallLayer))
        {
            return -1;
        }

        if (Physics2D.BoxCast(
            collider2d.bounds.center,
            collider2d.bounds.size,
            0f,
            Vector2.right,
            .1f,
            wallLayer))
        {
            return 1;
        }

        return 0;
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


    void SaltoPared(int direccionPared)
    {
        rb2d.linearVelocity = new Vector2(
            -direccionPared * velocidad,
            fuerzaSalto
        );

        saltosRestantes = 0;

        estado = 4;
        animator.SetInteger("estado", estado);

        if (direccionPared < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        wallJumping = true;
    }

    void SaltoNormal()
    {
        saltosRestantes--;

        rb2d.linearVelocityY = fuerzaSalto;

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




    // ANIMACION DAÑO
    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pinchos") && !recibiendoDano)
        {
            recibiendoDano = true;
            estado = 5;
            animator.SetInteger("estado", estado);
        }
    }

    public void TerminarDano()
    {
        recibiendoDano = false;

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

    // SALIR DE LA ANIMACION DE DAÑO
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pinchos"))
        {
            recibiendoDano = false;
        }
    }
}



