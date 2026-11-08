using UnityEngine;
using UnityEngine.SceneManagement;

// Contiene las acciones de los botones de la pantalla de fin de juego.
public class FinDelJuego : MonoBehaviour
{
// Reinicia la partida cargando el nivel inicial.
    public void VolverJugar()
    {
// Carga la escena Nivel1.
        SceneManager.LoadScene("Nivel1");
    }

// Vuelve a la pantalla principal.
    public void MenuPrincipal()
    {
// Carga la escena Inicio.
        SceneManager.LoadScene("Inicio");
    }

// Cierra la aplicacion cuando se ejecuta una version compilada del juego.
    public void Salir()
    {
// Solicita a la aplicacion que se cierre; en el Editor no cierra Unity.
        Application.Quit();
    }
}