using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;

    private int selectedIndex = 0;
    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }


       
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            pausePanel.SetActive(true);
        }
        else
        {
            pausePanel.SetActive(false);
        }
    }

  

    // --- BUTTON FONKSÝYONLARI ---
    public void ResumeGame()
    {
        TogglePause();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
