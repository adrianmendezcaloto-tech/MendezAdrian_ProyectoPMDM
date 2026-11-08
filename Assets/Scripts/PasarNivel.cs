using UnityEngine;
using UnityEngine.SceneManagement;

// Este componente permite pasar a la siguiente escena al tocar su trigger.
public class PasarNivel : MonoBehaviour
{
// Unity ejecuta este metodo cuando otro Collider2D entra en el trigger.
    void OnTriggerEnter2D(Collider2D collision)
    {
// Solo permite avanzar si el objeto que entra tiene la etiqueta Player.
        if (collision.gameObject.CompareTag("Player"))
        {
// Comprueba que existe otra escena despues de la actual en Build Settings.
            if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
// Carga la escena cuyo indice es el siguiente al de la escena actual.
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
    }
}