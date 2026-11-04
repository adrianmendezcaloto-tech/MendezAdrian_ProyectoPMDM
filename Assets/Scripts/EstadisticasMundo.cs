using UnityEngine;
using TMPro;

public class EstadisticasMundo : MonoBehaviour
{
    public int manzanas = 0;
    public TMP_Text textoManzanas;

    void Start()
    {
        ActualizarTexto();
    }

    public void RecogerManzana()
    {
        manzanas++;
        ActualizarTexto();
    }

    void ActualizarTexto()
    {
        textoManzanas.text = "x " + manzanas;
    }
}