using UnityEngine;
using System.Collections;

public class VagonTransition : MonoBehaviour
{
    [Header("Ayarlanacaklar")]
    public Transform teleportTarget;        // Oyuncunun geçiþ yapacaðý hedef (vagon baþý)
    public Transform playerTransform;       // Oyuncu transformu
    public Transform cameraTarget;          // Kameranýn geçiþ yapacaðý hedef
    public GameObject mainCamera;
    public Animator fadeAnimator;           // Fade animatörü
    public float fadeDuration = 1f;         // Fade süresi

    private bool isTransitioning = false;   // Geçiþ kontrolü

    public void StartTransition()
    {
        if (isTransitioning) return;

        if (teleportTarget == null || playerTransform == null || fadeAnimator == null || cameraTarget == null)
        {
            Debug.LogError("VagonTransition: Eksik referans var!");
            return;
        }

        isTransitioning = true;
        StartCoroutine(HandleTransition());
    }

    private IEnumerator HandleTransition()
    {
        // FadeOut baþlat
        fadeAnimator.SetTrigger("FadeOut");
        Debug.Log("FadeOut baþladý...");

        // FadeOut süresi kadar bekle
        yield return new WaitForSeconds(fadeDuration);

        // Oyuncuyu hedefe ýþýnla
        playerTransform.position = teleportTarget.position;
        Debug.Log("Oyuncu ýþýnlandý  " + teleportTarget.name);

        // Kamera hedefe ýþýnla
        mainCamera.transform.position = new Vector3(cameraTarget.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        Debug.Log("Kamera ýþýnlandý " + cameraTarget.name);

        // FadeIn baþlat
        fadeAnimator.SetTrigger("FadeIn");
        Debug.Log("FadeIn baþladý...");

        // Geçiþi bitir
        isTransitioning = false;
    }
}
