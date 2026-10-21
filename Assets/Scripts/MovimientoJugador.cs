using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoJugador : MonoBehaviour
{
    InputAction movimiento;
    InputAction saltar;
    Rigidbody2D rb2d;
    CapsuleCollider2D collider2d;
    [SerializeField] private LayerMask jumpableGround;


    float velocidad = 5f;
    float fuerzaSalto = 7f;

    int saltosRestantes = 1;

    void Start()
    {
        movimiento = InputSystem.actions.FindAction("Move");
        saltar = InputSystem.actions.FindAction("Jump");

        rb2d = GetComponent<Rigidbody2D>();
        collider2d = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        Vector2 valorMovimiento = movimiento.ReadValue<Vector2>();

        rb2d.linearVelocityX = valorMovimiento.x * velocidad;

        if (TocaSuelo())
        {
            saltosRestantes = 1;
        }

        if (saltar.WasPressedThisFrame() && saltosRestantes > 0)
        {
            rb2d.linearVelocityY = fuerzaSalto;
            saltosRestantes--;
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
}
