using UnityEngine;

// Hace que la camara siga la posicion del jugador en los ejes X e Y.
public class CamaraJugador : MonoBehaviour
{
// Permite asignar el objeto jugador desde el Inspector de Unity.
    [SerializeField]
    GameObject jugador;
    
// Update actualiza la posicion de la camara en cada fotograma.
    void Update()
    {
// Coloca la camara en una nueva posicion calculada con los datos siguientes.
          transform.position = new Vector3(
// Copia la coordenada horizontal del jugador.
            jugador.transform.position.x,
// Copia la coordenada vertical del jugador.
            jugador.transform.position.y,
// Conserva la coordenada Z de la camara para no alterar su profundidad.
            transform.position.z
         );
    }
}