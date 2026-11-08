using UnityEngine;
using TMPro;

// Lleva la cuenta de las manzanas recogidas y actualiza el texto de la interfaz.
public class EstadisticasMundo : MonoBehaviour
{
// Numero de manzanas recogidas; empieza en cero.
    public int manzanas = 0;
// Texto de TextMeshPro donde se muestra el contador.
    public TMP_Text textoManzanas;

// Inicializa el contador visual al comenzar la escena.
    void Start()
    {
// Muestra el valor inicial (x 0).
        ActualizarTexto();
    }

// Suma una manzana al contador.
    public void RecogerManzana()
    {
// Incrementa en una unidad la cantidad de manzanas.
        manzanas++;
// Actualiza el texto para reflejar el nuevo total.
        ActualizarTexto();
    }

// Construye y muestra el texto del contador.
    void ActualizarTexto()
    {
// Asigna al texto el formato 'x ' seguido del numero de manzanas.
        textoManzanas.text = "x " + manzanas;
    }
}