using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutController : MonoBehaviour
{
    public string sceneToLoad;

    public void PlayFadeOut()
    {
        GetComponent<Animator>().SetTrigger("Fade");
    }

    // Bu method animasyonun sonuna Event olarak eklenmeli
    public void OnFadeComplete()
    {
        SceneManager.LoadScene(sceneToLoad);
    }
}
