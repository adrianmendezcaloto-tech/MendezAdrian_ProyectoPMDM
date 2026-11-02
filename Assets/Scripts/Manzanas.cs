
using UnityEngine;

public class Manzanas : MonoBehaviour
{
    

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            Destroy(gameObject);
            
        }
    }
}
