using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCChatData", menuName = "Chat/NPC Chat Data")]
public class NPCChatData : ScriptableObject
{
    
   // public string npcName; // NPC'nin adý
    [TextArea(3, 10)]
    public string npcMessage; // NPC'nin verdiði mesaj (baþlangýç mesajý)

    public Answer[] answers; // 3 farklý cevap (cevaplar ve sonraki mesajlar)
}

[System.Serializable]
public class Answer
{
    public string answerText; // Cevap metni
    public NPCChatData nextChat; // Cevaba baðlý bir sonraki sohbet
    public bool endsConversation;
}
