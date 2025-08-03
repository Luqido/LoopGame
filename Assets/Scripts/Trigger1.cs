using UnityEngine;

public class Trigger1 : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Oyuncu trigger alanýna girdi!");
        }
    }
    

}
