using UnityEngine;

[CreateAssetMenu(fileName = "NewNPCChatData", menuName = "Chat/NPC Chat Data")]
public class NPCChatData : ScriptableObject
{
    
  
    [TextArea(3, 10)]
    public string npcMessage; 

    public Answer[] answers; 
}

[System.Serializable]
public class Answer
{
    public string answerText; 
    public NPCChatData nextChat; 
    public bool endsConversation;
    public bool startsCombat;
}
