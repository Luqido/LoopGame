using UnityEngine;
using UnityEngine.Rendering;
using Unity.Cinemachine;
using System.Collections;

public class CameraZoom : MonoBehaviour
{
    public CinemachineCamera virtualCam; // Yeni sistemde bu kullanýlýyor
    public float zoomDuration = 1f;

    private float targetSize;
    private float startSize;

    public void ZoomTo(float newSize)
    {
        StopAllCoroutines();
        targetSize = newSize;
        startSize = virtualCam.Lens.OrthographicSize;
        StartCoroutine(SmoothZoom());
    }

    private IEnumerator SmoothZoom()
    {
        float t = 0f;
        while (t < zoomDuration)
        {
            t += Time.deltaTime;
            float newSize = Mathf.Lerp(startSize, targetSize, t / zoomDuration);
            virtualCam.Lens.OrthographicSize = newSize;
            yield return null;
        }
        virtualCam.Lens.OrthographicSize = targetSize;
    }
}
