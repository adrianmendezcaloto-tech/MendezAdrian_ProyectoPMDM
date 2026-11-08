using UnityEngine;

// Hace que el objeto al que esta unido este script gire continuamente.
public class PlataformaGiratoria : MonoBehaviour
{
// Velocidad de giro en grados por segundo; el valor negativo indica sentido horario.
  float velocidad = -200f;
  
// Update se ejecuta cada fotograma para actualizar la rotacion.
    void Update()
    {
// Gira alrededor del eje Z; Time.deltaTime hace que la velocidad sea independiente de los FPS.
            transform.Rotate(0f, 0f, velocidad * Time.deltaTime);

    }
}