using UnityEngine;
using UnityEngine.SceneManagement;

// Script asociado al menu inicial para avanzar a la siguiente escena.
public class NewMonoBehaviourScript : MonoBehaviour
{
// Metodo publico que normalmente se conecta al boton Jugar del menu.
       public void Jugar()
    {
// Comprueba que existe una escena siguiente en Build Settings.
        if (SceneManager.GetActiveScene().buildIndex + 1 < SceneManager.sceneCountInBuildSettings)
            {
// Carga la escena siguiente segun el orden de Build Settings.
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
    }
}