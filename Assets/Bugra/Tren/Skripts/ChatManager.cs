using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public TextMeshProUGUI npcNameText; // NPC adý
    public TextMeshProUGUI npcMessageText; // NPC mesajý
    public Button answerButton1; // 1. cevap butonu
    public Button answerButton2; // 2. cevap butonu
    public Button answerButton3; // 3. cevap butonu
    public GameObject chatPanel; // Chat paneli

    private NPCChatData currentChatData; // Þu anki NPC'nin sohbet verisi

    void Start()
    {
        chatPanel.SetActive(false); // Baþlangýçta chat panelini gizle
        answerButton1.onClick.AddListener(() => OnAnswerButtonClicked(0));
        answerButton2.onClick.AddListener(() => OnAnswerButtonClicked(1));
        answerButton3.onClick.AddListener(() => OnAnswerButtonClicked(2));
    }

    // NPC ile sohbet baþlat
    public void StartChat(NPCChatData chatData)
    {
        currentChatData = chatData;
        chatPanel.SetActive(true); // Chat panelini aç
        ShowNPCMessage(); // Ýlk mesajý göster
    }

    // NPC'nin mesajýný göster
    private void ShowNPCMessage()
    {
        npcNameText.text = currentChatData.npcName;
        npcMessageText.text = currentChatData.npcMessage;

        // Eðer cevap yoksa konuþmayý kýsa bir gecikmeyle bitir
        if (currentChatData.answers == null || currentChatData.answers.Length == 0)
        {
            Invoke(nameof(EndChat), 1.5f); // 1.5 saniye sonra otomatik kapat
            return;
        }

        ShowAnswerButtons(); // Butonlarý göster
    }

    // Cevap butonlarýný göster
    private void ShowAnswerButtons()
    {
        // Hepsini önce kapatalým
        answerButton1.gameObject.SetActive(false);
        answerButton2.gameObject.SetActive(false);
        answerButton3.gameObject.SetActive(false);

        for (int i = 0; i < currentChatData.answers.Length; i++)
        {
            Button button = null;
            switch (i)
            {
                case 0: button = answerButton1; break;
                case 1: button = answerButton2; break;
                case 2: button = answerButton3; break;
            }

            if (button != null)
            {
                button.gameObject.SetActive(true);
                button.GetComponentInChildren<TextMeshProUGUI>().text = currentChatData.answers[i].answerText;

                int capturedIndex = i; // for closure
                button.onClick.RemoveAllListeners();
                button.onClick.AddListener(() =>
                {
                    if (currentChatData.answers[capturedIndex].endsConversation)
                    {
                        EndChat();
                    }
                    else
                    {
                        NPCChatData next = currentChatData.answers[capturedIndex].nextChat;
                        if (next != null)
                        {
                            currentChatData = next;
                            ShowNPCMessage();
                        }
                        else
                        {
                            EndChat(); // fallback
                        }
                    }
                });
            }
        }
    }

    // Cevap butonuna týklanýnca
    private void OnAnswerButtonClicked(int answerIndex)
    {
        if (currentChatData.answers.Length > answerIndex)
        {
            NPCChatData nextChat = currentChatData.answers[answerIndex].nextChat; // Seçilen cevaba göre yeni sohbeti al
            if (nextChat != null)
            {
                currentChatData = nextChat; // Yeni sohbeti al
                ShowNPCMessage(); // Yeni mesajý göster
            }
            else
            {
                EndChat(); // Konuþma bitti
            }
        }
    }

    // Konuþmayý bitir
    private void EndChat()
    {
        chatPanel.SetActive(false); // Chat panelini kapat
        Debug.Log("Sohbet bitti!");
    }
}
