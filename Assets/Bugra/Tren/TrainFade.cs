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
    public InteractionType interactionType; // Nesnenin türünü belirt
    public GameObject interactableUI; // X simgesi veya etkileþim arayüzü
    public string npcChatMessage; // NPC ile etkileþimde kullanýlacak mesaj

    private void Start()
    {
        interactableUI.SetActive(false); // Baþlangýçta UI'yi gizle
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            interactableUI.SetActive(true); // X simgesini göster
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
        // Etkileþim türüne göre farklý aksiyonlar
        switch (interactionType)
        {
            case InteractionType.NPC:
                StartChat(); // NPC ile konuþma baþlat
                break;
            case InteractionType.Vagon:
                StartVagonTransition(); // Vagon geçiþini baþlat
                break;
            case InteractionType.Door:
                OpenDoor(); // Kapý açma
                break;
            case InteractionType.Other:
                // Diðer etkileþimler
                break;
        }
    }

    private void StartChat()
    {
        Debug.Log("Chat Baþlatýldý: " + npcChatMessage);
        // Burada NPC ile chat penceresini açabilirsiniz
    }

    private void StartVagonTransition()
    {
        Debug.Log("Vagon Geçiþi Baþlatýldý");
        // Vagon geçiþi baþlatýlabilir (kamera geçiþi, oyuncu ýþýnlama vb.)
    }

    private void OpenDoor()
    {
        Debug.Log("Kapý Açýldý");
        // Kapý açma aksiyonlarý buraya eklenebilir
    }
}
