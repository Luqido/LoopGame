using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class FadeOutController : MonoBehaviour
{
    public string sceneToLoad;
    public RenderPipelineAsset urp2DAsset;
    public RenderPipelineAsset urp3DAsset;

    private void Awake()
    {
        if (urp3DAsset != null)
        {
            GraphicsSettings.defaultRenderPipeline = urp3DAsset;
            Debug.Log("URP: 3D Renderer aktif edildi.");
        }
    }
    public void PlayFadeOut()
    {
        GetComponent<Animator>().SetTrigger("Fade");
    }

    public void OnFadeComplete()
    {
        if (urp2DAsset != null)
        {
            GraphicsSettings.defaultRenderPipeline = urp2DAsset; 
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
