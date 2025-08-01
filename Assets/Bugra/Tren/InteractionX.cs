using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    public GameObject interactableUI; // X simgesinin GameObject'i (2D Sprite)
    public float interactionDistance = 3f; // Etkileþim mesafesi
    private bool isInTrigger = false; // Trigger içinde olup olmadýðýný kontrol etmek için
    private Transform currentTarget; // Etkileþimde olduðumuz nesne
    private SpriteRenderer objRenderer; // X simgesinin SpriteRenderer bileþeni

    void Start()
    {
        objRenderer = interactableUI.GetComponent<SpriteRenderer>(); // X simgesinin SpriteRenderer bileþenini al
        interactableUI.SetActive(false); // Baþlangýçta X simgesini gizle
    }

    void Update()
    {
        // Eðer etkileþim alanýndaysak
        if (isInTrigger)
        {
            // Mesafeyi hesapla
            float distance = Vector3.Distance(transform.position, currentTarget.position);
            // Mesafeye göre þeffaflýk deðerini ayarla
            float alpha = Mathf.Clamp01(1 - (distance / interactionDistance)); // 1 tam görünür, 0 þeffaf

            // Þeffaflýk deðerini ayarla
            Color color = objRenderer.color;
            color.a = alpha; // Þeffaflýk deðerini güncelle
            objRenderer.color = color;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Interactable")) // Eðer etkileþime geçilebilen bir nesneye yaklaþýldýysa
        {
            currentTarget = other.transform; // Etkileþimde olduðumuz nesneyi belirle
            isInTrigger = true; // Trigger içine girildi
            interactableUI.SetActive(true); // X simgesini göster
            Debug.Log("Etkileþim alanýna girildi.");
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Interactable")) // Eðer etkileþim alanýndan çýkýldýysa
        {
            isInTrigger = false; // Trigger dýþýna çýkýldý
            interactableUI.SetActive(false); // X simgesini gizle
            Debug.Log("Etkileþim alanýndan çýkýldý.");
        }
    }
}
