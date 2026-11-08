using UnityEngine;
using UnityEngine.SceneManagement;

// Controla las acciones disponibles en la pantalla de muerte.
public class MenuMuerte : MonoBehaviour
{
// Nombre de la escena a cargar al volver a jugar; se puede cambiar en el Inspector.
    public string nombreNivel = "Nivel1";

// Reinicia la partida cargando el nivel indicado en nombreNivel.
    public void VolverAJugar()
    {
// Carga la escena cuyo nombre esta guardado en nombreNivel.
        SceneManager.LoadScene(nombreNivel);
    }

// Cierra la aplicacion al pulsar el boton de salir.
    public void Salir()
    {
// Solicita cerrar la aplicacion compilada; en el Editor no cierra Unity.
        Application.Quit();
    }
}