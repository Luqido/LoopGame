using UnityEngine;

public class NPC : MonoBehaviour
{
    public NPCChatData npcChatData; // NPC'nin sohbet verisi (ScriptableObject)
    public Sprite npcPortrait;
    public string npcName;
    public Sprite npcImage;
    public EnemyType npcType; //TODO editorde doldurulacak
    public GameObject xIcon;
}
