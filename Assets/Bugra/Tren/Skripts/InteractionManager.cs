using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public ChatManager chatManager; // ChatManager referansý, sohbeti yönlendirecek
    public NPCChatData defaultChatData; // NPC'nin sohbet verisi (ilk mesaj için)
    public CameraZoom cameraZoom;
    public void InteractWithObject(Transform target)
    {
        if (target.CompareTag("NPC"))
        {
            cameraZoom.ZoomTo(9.15f);
            StartChat(target); // NPC ile sohbet baþlat
            Debug.Log("NPC ile sohbet baþlatýldý!");
        }
        else if (target.CompareTag("Door"))
        {
            StartVagonTransition(target); // Her door'un kendi transition'u var
        }
        else
        {
            Debug.Log("Bilinmeyen etkileþim!");
        }
    }

    private void StartChat()
    {
        Debug.Log("NPC ile sohbet baþlatýldý!");
    }

    private void StartVagonTransition(Transform doorTransform)
    {
        VagonTransition transition = doorTransform.GetComponent<VagonTransition>();
        if (transition != null)
        {
            transition.StartTransition(); //  Burada target deðil, transition objesinin içindeki teleportTarget kullanýlýyor
        }
        else
        {
            Debug.LogWarning("Door objesinde VagonTransition yok!");
        }
    }
    private void StartChat(Transform npc)
    {
        NPC npcComponent = npc.GetComponent<NPC>();

        NPCChatData npcChatData = npc.GetComponent<NPC>().npcChatData; // NPC'nin konuþma verisini al
        if (npcChatData != null)
        {
            chatManager.StartChat(npcChatData); // ChatManager üzerinden sohbeti baþlat
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
        Debug.Log("Kapý açýldý!");
    }
}
