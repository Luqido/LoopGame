using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PauseMenuController : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;
    public Button[] menuButtons;

    private int selectedIndex = 0;
    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }

        if (!isPaused || !pausePanel.activeInHierarchy) return;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedIndex = (selectedIndex + 1) % menuButtons.Length;
            SelectButton(selectedIndex);
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedIndex = (selectedIndex - 1 + menuButtons.Length) % menuButtons.Length;
            SelectButton(selectedIndex);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            menuButtons[selectedIndex].onClick.Invoke();
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f;
            pausePanel.SetActive(true);
            selectedIndex = 0;
            SelectButton(selectedIndex);
        }
        else
        {
            Time.timeScale = 1f;
            pausePanel.SetActive(false);
        }
    }

    void SelectButton(int index)
    {
        if (menuButtons.Length == 0) return;

        // UI navigasyonu
        EventSystem.current.SetSelectedGameObject(menuButtons[index].gameObject);

        // Tüm buton renklerini sýfýrla (normal color yap)
        foreach (Button btn in menuButtons)
        {
            var colors = btn.colors;
            btn.targetGraphic.color = colors.normalColor;
        }

        // Seçilen butonun rengini highlighted yap
        var selectedButton = menuButtons[index];
        var selectedColors = selectedButton.colors;
        selectedButton.targetGraphic.color = selectedColors.highlightedColor;
    }

    // --- BUTTON FONKSÝYONLARI ---
    public void ResumeGame()
    {
        TogglePause();
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }
}
