using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerChatBox : MonoBehaviour
{
    public TextMeshProUGUI chatText;
    public GameObject chatPanel;
    public float typingSpeed = 0.05f;
    public float eraseSpeed = 0.03f;
    public float sentenceDelay = 1.5f;
    public float messageDelay = 2f;
    public string[] randomMessages;

    private Coroutine loopCoroutine;
    private bool loopActive = false;
    private Queue<string> messageQueue = new Queue<string>();
    private string lastMessage = "";

    void Start()
    {
        chatPanel.SetActive(false);
        StartTextLoop(); 
    }

    public void StartTextLoop()
    {
        if (!loopActive)
        {
            loopActive = true;
            loopCoroutine = StartCoroutine(ChatLoop());
        }
    }

    public void StopTextLoop()
    {
        loopActive = false;
        if (loopCoroutine != null)
            StopCoroutine(loopCoroutine);

        chatText.text = "";
        chatPanel.SetActive(false);
    }

    private IEnumerator ChatLoop()
    {
        while (loopActive)
        {
            
            chatPanel.SetActive(true);

            
            if (messageQueue.Count == 0)
                FillShuffledQueue();

            
            string nextMessage = messageQueue.Dequeue();
            while (nextMessage == lastMessage && messageQueue.Count > 0)
                nextMessage = messageQueue.Dequeue();

            lastMessage = nextMessage;

            
            yield return StartCoroutine(TypeAndEraseSentences(nextMessage));

            
            chatPanel.SetActive(false);

            yield return new WaitForSeconds(messageDelay);
        }
    }

    private void FillShuffledQueue()
    {
        List<string> shuffled = new List<string>(randomMessages);
        for (int i = 0; i < shuffled.Count; i++)
        {
            string temp = shuffled[i];
            int randIndex = Random.Range(i, shuffled.Count);
            shuffled[i] = shuffled[randIndex];
            shuffled[randIndex] = temp;
        }

        foreach (var msg in shuffled)
            messageQueue.Enqueue(msg);
    }

    private IEnumerator TypeAndEraseSentences(string fullMessage)
    {
        string[] sentences = fullMessage.Split(new[] { '.', '?', '!' }, System.StringSplitOptions.RemoveEmptyEntries);

        foreach (string rawSentence in sentences)
        {
            string sentence = rawSentence.Trim() + ".";
            yield return StartCoroutine(TypeText(sentence));
            yield return new WaitForSeconds(sentenceDelay);
            yield return StartCoroutine(EraseText());
        }
    }

    private IEnumerator TypeText(string message)
    {
        chatText.text = "";
        foreach (char letter in message)
        {
            chatText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private IEnumerator EraseText()
    {
        while (chatText.text.Length > 0)
        {
            chatText.text = chatText.text.Substring(0, chatText.text.Length - 1);
            yield return new WaitForSeconds(eraseSpeed);
        }
    }
}
