using UnityEngine;
public enum InteractionType
{
    NPC,
    Vagon,
    Door,
    Other
}

public class Interaction : MonoBehaviour
{
    public InteractionType interactionType; // Nesnenin t�r�n� belirt
    public GameObject interactableUI; // X simgesi veya etkile�im aray�z�
    public string npcChatMessage; // NPC ile etkile�imde kullan�lacak mesaj

    private void Start()
    {
        interactableUI.SetActive(false); // Ba�lang��ta UI'yi gizle
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactableUI.SetActive(true); // X simgesini g�ster
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactableUI.SetActive(false); // X simgesini gizle
        }
    }

    public void Interact()
    {
        // Etkile�im t�r�ne g�re farkl� aksiyonlar
        switch (interactionType)
        {
            case InteractionType.NPC:
                StartChat(); // NPC ile konu�ma ba�lat
                break;
            case InteractionType.Vagon:
                StartVagonTransition(); // Vagon ge�i�ini ba�lat
                break;
            case InteractionType.Door:
                OpenDoor(); // Kap� a�ma
                break;
            case InteractionType.Other:
                // Di�er etkile�imler
                break;
        }
    }

    private void StartChat()
    {
        Debug.Log("Chat Ba�lat�ld�: " + npcChatMessage);
        // Burada NPC ile chat penceresini a�abilirsiniz
    }

    private void StartVagonTransition()
    {
        Debug.Log("Vagon Ge�i�i Ba�lat�ld�");
        // Vagon ge�i�i ba�lat�labilir (kamera ge�i�i, oyuncu ���nlama vb.)
    }

    private void OpenDoor()
    {
        Debug.Log("Kap� A��ld�");
        // Kap� a�ma aksiyonlar� buraya eklenebilir
    }
}
