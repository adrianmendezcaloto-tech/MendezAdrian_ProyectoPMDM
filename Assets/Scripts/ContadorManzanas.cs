
using UnityEngine;
using TMPro;

public class ContadorManzanas : MonoBehaviour
{
    public TMP_Text textoManzanas;
    int manzanas = 0;

    public void SumarManzana()
    {
        manzanas++;
        textoManzanas.text = manzanas.ToString();
    }
}
