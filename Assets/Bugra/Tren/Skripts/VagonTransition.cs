using UnityEngine;
using System.Collections;

public class VagonTransition : MonoBehaviour
{
    [Header("Ayarlanacaklar")]
    public Transform teleportTarget;        
    public Transform playerTransform;       
    public Animator fadeAnimator;            
    public float fadeDuration = 1f;          

    private bool isTransitioning = false;

    public void StartTransition()
    {
        if (isTransitioning) return;

        if (teleportTarget == null || playerTransform == null || fadeAnimator == null)
        {
            Debug.LogError("VagonTransition: Eksik referans var!");
            return;
        }

        isTransitioning = true;
        StartCoroutine(HandleTransition());
    }

    private IEnumerator HandleTransition()
    {
        fadeAnimator.SetTrigger("FadeOut");
        Debug.Log("FadeOut baþladý...");

        yield return new WaitForSeconds(fadeDuration); 

        playerTransform.position = teleportTarget.position;
        Debug.Log("Oyuncu ýþýnlandý  " + teleportTarget.name);

        fadeAnimator.SetTrigger("FadeIn");
        Debug.Log("FadeIn baþladý...");

        isTransitioning = false;
    }
}
