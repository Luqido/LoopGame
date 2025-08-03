using UnityEngine;

public class UIController : MonoBehaviour
{
    
    [SerializeField] BaseMenu baseMenu;
    [SerializeField] Animator _loseAnimator;
    [SerializeField] StartButton _startButton;
    public void RestartGame() { 
    }

    public void GoMenu()
    {
        CameraController.Instance.MoveCamera(false);
        _startButton.UnSelect();
        // GamePlayManager.Instance.StopGame();
        _loseAnimator.SetTrigger("Off");
        baseMenu.BaseMenuAction(true);
    }
}
