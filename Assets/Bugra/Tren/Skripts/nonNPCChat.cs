using System.Collections;
using TMPro;
using UnityEngine;

public class nonNPCChat : MonoBehaviour
{
    [TextArea]
    public string messageToDisplay = "Merhaba! Bilgi panosunu okuyorsun.";
    public float displayTime = 2f;
    public float typingSpeed = 0.05f;

    private bool hasBeenTriggered = false;

    [Header("UI")]
    public GameObject chatPanel;
    public TextMeshProUGUI chatText;

    //public AudioSource typeSound; // Opsiyonel: her harf için ses

    private Coroutine typeCoroutine;

    void Start()
    {
        if (chatPanel != null)
            chatPanel.SetActive(false);
    }

    public void TriggerChat()
    {
        if (!hasBeenTriggered)
        {
            hasBeenTriggered = true;
            StartCoroutine(ShowMessage());
        }
    }

    private IEnumerator ShowMessage()
    {
        chatPanel.SetActive(true);
        chatText.text = "";

        if (typeCoroutine != null)
            StopCoroutine(typeCoroutine);

        typeCoroutine = StartCoroutine(TypeText(messageToDisplay));

        yield return new WaitForSeconds(displayTime + messageToDisplay.Length * typingSpeed);

        chatPanel.SetActive(false);
    }

    private IEnumerator TypeText(string message)
    {
        chatText.text = "";

        foreach (char c in message)
        {
            chatText.text += c;

         //   if (typeSound != null)
               // typeSound.Play();

            yield return new WaitForSeconds(typingSpeed);
        }
    }
}
