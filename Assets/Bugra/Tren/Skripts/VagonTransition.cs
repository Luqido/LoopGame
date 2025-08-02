using UnityEngine;
using System.Collections;

public class VagonTransition : MonoBehaviour
{
    [Header("Ayarlanacaklar")]
    public Transform teleportTarget;        // Oyuncunun ge�i� yapaca�� hedef (vagon ba��)
    public Transform playerTransform;       // Oyuncu transformu
    public Transform cameraTarget;          // Kameran�n ge�i� yapaca�� hedef
    public GameObject mainCamera;
    public Animator fadeAnimator;           // Fade animat�r�
    public float fadeDuration = 1f;         // Fade s�resi

    private bool isTransitioning = false;   // Ge�i� kontrol�

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
        // FadeOut ba�lat
        fadeAnimator.SetTrigger("FadeOut");
        Debug.Log("FadeOut ba�lad�...");

        // FadeOut s�resi kadar bekle
        yield return new WaitForSeconds(fadeDuration);

        // Oyuncuyu hedefe ���nla
        playerTransform.position = teleportTarget.position;
        Debug.Log("Oyuncu ���nland�  " + teleportTarget.name);

        // Kamera hedefe ���nla
        mainCamera.transform.position = new Vector3(cameraTarget.position.x, mainCamera.transform.position.y, mainCamera.transform.position.z);
        Debug.Log("Kamera ���nland� " + cameraTarget.name);

        // FadeIn ba�lat
        fadeAnimator.SetTrigger("FadeIn");
        Debug.Log("FadeIn ba�lad�...");

        // Ge�i�i bitir
        isTransitioning = false;
    }
}
