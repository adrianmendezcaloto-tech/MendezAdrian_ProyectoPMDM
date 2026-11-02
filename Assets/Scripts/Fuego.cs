
using System.Collections;
using UnityEngine;

public class Fuego : MonoBehaviour
{
    Animator animator;
    SpriteRenderer sprite;
    PolygonCollider2D pc2d;

    Sprite spriteAnterior;

    void Start()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        pc2d = GetComponent<PolygonCollider2D>();

        animator.SetInteger("estado", 0);
        tag = "Suelo";

        StartCoroutine(CicloFuego());
    }

    void Update()
    {
        ActualizarCollider();
    }

    IEnumerator CicloFuego()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);

            animator.SetInteger("estado", 1);
            tag = "Obstaculos";

            yield return new WaitForSeconds(5f);

            animator.SetInteger("estado", 0);
            tag = "Suelo";
        }
    }

    void ActualizarCollider()
    {
        if (sprite.sprite != null && sprite.sprite != spriteAnterior)
        {
            spriteAnterior = sprite.sprite;

            pc2d.pathCount = sprite.sprite.GetPhysicsShapeCount();

            for (int i = 0; i < pc2d.pathCount; i++)
            {
                System.Collections.Generic.List<Vector2> puntos = new();

                sprite.sprite.GetPhysicsShape(i, puntos);

                pc2d.SetPath(i, puntos);
            }
        }
    }
}
