using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatManager : MonoBehaviour
{
    public TextMeshProUGUI npcNameText;
    public TextMeshProUGUI npcMessageText;
    public Button answerButton1;
    public Button answerButton2;
    public Button answerButton3;
    public GameObject chatPanel;

    private NPCChatData currentChatData;
    private Coroutine typeCoroutine;
    public float typingSpeed = 0.03f;

    private bool isTyping = false;
    private string fullMessage = "";
    public GameObject player;
    public Image npcPortraitImage;
    public Image npcLandscapeImage;


    void Start()
    {
        chatPanel.SetActive(false);
        answerButton1.onClick.AddListener(() => OnAnswerButtonClicked(0));
        answerButton2.onClick.AddListener(() => OnAnswerButtonClicked(1));
        answerButton3.onClick.AddListener(() => OnAnswerButtonClicked(2));
    }

    public void StartChat(NPCChatData chatData)
    {
        currentChatData = chatData;
        chatPanel.SetActive(true);

        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = false; 
        }
        ShowNPCMessage();
    }

    private void Update()
    {
        
        if (isTyping && Input.GetKeyDown(KeyCode.Space))
        {
            if (typeCoroutine != null)
                StopCoroutine(typeCoroutine);

            npcMessageText.text = fullMessage;
            isTyping = false;
            ShowAnswerButtons();
        }
    }

    private void ShowNPCMessage()
    {
       

        if (typeCoroutine != null)
            StopCoroutine(typeCoroutine);

        fullMessage = currentChatData.npcMessage;
        typeCoroutine = StartCoroutine(TypeMessage(fullMessage));

        
        answerButton1.gameObject.SetActive(false);
        answerButton2.gameObject.SetActive(false);
        answerButton3.gameObject.SetActive(false);
    }

    private IEnumerator TypeMessage(string message)
    {
        isTyping = true;
        npcMessageText.text = "";

        foreach (char letter in message)
        {
            npcMessageText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;

      
        if (currentChatData.answers == null || currentChatData.answers.Length == 0)
        {
            Invoke(nameof(EndChat), 1.5f);
        }
        else
        {
            ShowAnswerButtons();
        }
    }

    private void ShowAnswerButtons()
    {
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

                int capturedIndex = i;
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
                            EndChat(); 
                        }
                    }
                });
            }
        }
    }

    private void OnAnswerButtonClicked(int index)
    {
        if (currentChatData.answers.Length > index)
        {
            NPCChatData next = currentChatData.answers[index].nextChat;
            if (next != null)
            {
                currentChatData = next;
                ShowNPCMessage();
            }
            else
            {
                EndChat();
            }
        }
    }

    private void EndChat()
    {
        chatPanel.SetActive(false);
        if (player != null)
        {
            player.GetComponent<PlayerMovement>().enabled = true; 
        }
        Debug.Log("Sohbet bitti!");
    }
    public void SetPortrait(Sprite portrait )
    {
        if (npcPortraitImage != null)
            npcPortraitImage.sprite = portrait;
    }
    public void SetName(string name)
    {
        npcNameText.text = name;
    }
    public void SetLandScape(Sprite npcLandScape)
    {
        npcLandscapeImage.sprite = npcLandScape;
        
    }

}
