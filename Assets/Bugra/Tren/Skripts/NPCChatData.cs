using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCChatData", menuName = "Chat/NPC Chat Data")]
public class NPCChatData : ScriptableObject
{
    
   // public string npcName; // NPC'nin ad�
    [TextArea(3, 10)]
    public string npcMessage; // NPC'nin verdi�i mesaj (ba�lang�� mesaj�)

    public Answer[] answers; // 3 farkl� cevap (cevaplar ve sonraki mesajlar)
}

[System.Serializable]
public class Answer
{
    public string answerText; // Cevap metni
    public NPCChatData nextChat; // Cevaba ba�l� bir sonraki sohbet
    public bool endsConversation;
}
