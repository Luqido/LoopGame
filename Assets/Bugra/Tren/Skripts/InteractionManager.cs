using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public ChatManager chatManager; // ChatManager referans�, sohbeti y�nlendirecek
    public NPCChatData defaultChatData; // NPC'nin sohbet verisi (ilk mesaj i�in)
    public CameraZoom cameraZoom;
    public void InteractWithObject(Transform target)
    {
        if (target.CompareTag("NPC"))
        {
            cameraZoom.ZoomTo(9.15f);
            StartChat(target); // NPC ile sohbet ba�lat
            Debug.Log("NPC ile sohbet ba�lat�ld�!");
        }
        else if (target.CompareTag("Door"))
        {
            StartVagonTransition(target); // Her door'un kendi transition'u var
        }
        else
        {
            Debug.Log("Bilinmeyen etkile�im!");
        }
    }

    private void StartChat()
    {
        Debug.Log("NPC ile sohbet ba�lat�ld�!");
    }

    private void StartVagonTransition(Transform doorTransform)
    {
        VagonTransition transition = doorTransform.GetComponent<VagonTransition>();
        if (transition != null)
        {
            transition.StartTransition(); //  Burada target de�il, transition objesinin i�indeki teleportTarget kullan�l�yor
        }
        else
        {
            Debug.LogWarning("Door objesinde VagonTransition yok!");
        }
    }
    private void StartChat(Transform npc)
    {
        NPC npcComponent = npc.GetComponent<NPC>();

        NPCChatData npcChatData = npc.GetComponent<NPC>().npcChatData; // NPC'nin konu�ma verisini al
        if (npcChatData != null)
        {
            chatManager.StartChat(npcChatData); // ChatManager �zerinden sohbeti ba�lat
            chatManager.SetPortrait(npcComponent.npcPortrait); // Portre setleniyor
            chatManager.SetName(npcComponent.name);
            chatManager.SetLandScape(npcComponent.npcImage);


        }
        else
        {
            Debug.LogWarning("NPC'nin sohbet verisi yok!");
        }
    }
    private void OpenDoor()
    {
        Debug.Log("Kap� a��ld�!");
    }
}
