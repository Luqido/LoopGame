using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public ChatManager chatManager; 
    public NPCChatData defaultChatData; 
    public CameraZoom cameraZoom;
    public PlayerChatBox playerChatBox;

    public void InteractWithObject(Transform target)
    {
        SoundManager.instance.PlaySound(SoundManager.SoundNames.MenuClick1);
        if (target.CompareTag("NPC"))
        {
            cameraZoom.ZoomTo(9.15f);
            StartChat(target); 
            Debug.Log("NPC ile sohbet ba�lat�ld�!");
        }
        else if (target.CompareTag("Door"))
        {
            StartVagonTransition(target); 
        }
        else if (target.CompareTag("nonNPC"))
        {
            nonNPCChat chatSystem = target.GetComponent<nonNPCChat>();
            if (chatSystem != null)
            {
                chatSystem.TriggerChat();
            }
            else
            {
                Debug.LogWarning("nonNPC objesinde NPCChatSystem yok!");
            }

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
            transition.StartTransition(); 
        }
        else
        {
            Debug.LogWarning("Door objesinde VagonTransition yok!");
        }
    }
    private void StartChat(Transform npc)
    {
        NPC npcComponent = npc.GetComponent<NPC>();

        NPCChatData npcChatData = npc.GetComponent<NPC>().npcChatData;
        if (npcChatData != null)
        {
            chatManager.StartChat(npcChatData); 
            chatManager.SetPortrait(npcComponent.npcPortrait);
            chatManager.SetName(npcComponent.npcName);
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
