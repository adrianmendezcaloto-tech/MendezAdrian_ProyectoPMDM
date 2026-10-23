using UnityEngine;

public class CamaraJugador : MonoBehaviour
{
    [SerializeField]
    GameObject jugador;
    
    void Update()
    {
          transform.position = new Vector3(
            jugador.transform.position.x,
            jugador.transform.position.y,
            transform.position.z
         );
    }
}
