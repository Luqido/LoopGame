using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public GameObject interactableUI; // X simgesinin GameObject'i (2D Sprite)
    public float interactionDistance = 3f; // Etkile�im mesafesi
    private bool isInTrigger = false; // Trigger i�inde olup olmad���n� kontrol etmek i�in
    private Transform currentTarget; // Etkile�imde oldu�umuz nesne
    private SpriteRenderer objRenderer; // X simgesinin SpriteRenderer bile�eni

    void Start()
    {
        objRenderer = interactableUI.GetComponent<SpriteRenderer>(); // X simgesinin SpriteRenderer bile�enini al
        interactableUI.SetActive(false); // Ba�lang��ta X simgesini gizle
    }

    void Update()
    {
        // E�er etkile�im alan�ndaysak
        if (isInTrigger)
        {
            // Mesafeyi hesapla
            float distance = Vector3.Distance(transform.position, currentTarget.position);
            // Mesafeye g�re �effafl�k de�erini ayarla
            float alpha = Mathf.Clamp01(1 - (distance / interactionDistance)); // 1 tam g�r�n�r, 0 �effaf

            // �effafl�k de�erini ayarla
            Color color = objRenderer.color;
            color.a = alpha; // �effafl�k de�erini g�ncelle
            objRenderer.color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable")) // E�er etkile�ime ge�ilebilen bir nesneye yakla��ld�ysa
        {
            currentTarget = other.transform; // Etkile�imde oldu�umuz nesneyi belirle
            isInTrigger = true; // Trigger i�ine girildi
            interactableUI.SetActive(true); // X simgesini g�ster
            Debug.Log("Etkile�im alan�na girildi.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable")) // E�er etkile�im alan�ndan ��k�ld�ysa
        {
            isInTrigger = false; // Trigger d���na ��k�ld�
            interactableUI.SetActive(false); // X simgesini gizle
            Debug.Log("Etkile�im alan�ndan ��k�ld�.");
        }
    }
}
