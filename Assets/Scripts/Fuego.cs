using System.Collections;
using UnityEngine;

// Alterna el estado del fuego y adapta su collider a la forma del sprite actual.
public class Fuego : MonoBehaviour
{
// Referencia al Animator que controla la animacion del fuego.
    Animator animator;
// Referencia al SpriteRenderer que muestra el sprite actual.
    SpriteRenderer sprite;
// Collider poligonal que se ajusta a la forma fisica del sprite.
    PolygonCollider2D pc2d;

// Guarda el ultimo sprite procesado para evitar recalcular el collider sin cambios.
    Sprite spriteAnterior;

// Inicializa componentes, estado, etiqueta y ciclo automatico del fuego.
    void Start()
    {
// Obtiene el Animator del fuego.
        animator = GetComponent<Animator>();
// Obtiene el SpriteRenderer.
        sprite = GetComponent<SpriteRenderer>();
// Obtiene el PolygonCollider2D.
        pc2d = GetComponent<PolygonCollider2D>();

// Empieza con el estado 0 (fuego apagado, segun la configuracion del Animator).
        animator.SetInteger("estado", 0);
// Asigna la etiqueta Suelo mientras el fuego esta apagado.
        tag = "Suelo";

// Inicia la corrutina que alterna el fuego encendido y apagado.
        StartCoroutine(CicloFuego());
    }

// Update se ejecuta cada fotograma.
    void Update()
    {
// Comprueba si cambio el sprite y adapta el collider a su forma.
        ActualizarCollider();
    }

// Corrutina que alterna indefinidamente entre fuego apagado y encendido.
    IEnumerator CicloFuego()
    {
// Repite el ciclo continuamente mientras el objeto siga activo.
        while (true)
        {
// Espera cinco segundos antes de encender el fuego.
            yield return new WaitForSeconds(5f);

// Activa el estado 1 de la animacion (fuego encendido).
            animator.SetInteger("estado", 1);
// Cambia la etiqueta para que el fuego se considere un obstaculo.
            tag = "Obstaculos";

// Espera cinco segundos con el fuego encendido.
            yield return new WaitForSeconds(5f);

// Vuelve al estado 0 de la animacion (fuego apagado).
            animator.SetInteger("estado", 0);
// Vuelve a etiquetarlo como Suelo.
            tag = "Suelo";
        }
    }

// Actualiza el collider poligonal solo cuando cambia el sprite mostrado.
    void ActualizarCollider()
    {
// Comprueba que existe un sprite y que es distinto del ultimo procesado.
        if (sprite.sprite != null && sprite.sprite != spriteAnterior)
        {
// Guarda el sprite actual para comparar en la proxima llamada.
            spriteAnterior = sprite.sprite;

// Ajusta el numero de contornos del collider al numero de formas fisicas del sprite.
            pc2d.pathCount = sprite.sprite.GetPhysicsShapeCount();

// Recorre todos los contornos que componen la forma fisica del sprite.
            for (int i = 0; i < pc2d.pathCount; i++)
            {
// Crea una lista vacia donde se guardaran los puntos del contorno actual.
                System.Collections.Generic.List<Vector2> puntos = new();

// Copia en la lista los puntos de la forma fisica del sprite.
                sprite.sprite.GetPhysicsShape(i, puntos);

// Asigna esos puntos al contorno correspondiente del PolygonCollider2D.
                pc2d.SetPath(i, puntos);
            }
        }
    }
}