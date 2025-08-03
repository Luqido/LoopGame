using System;
using UnityEngine;

public class UIController : MonoBehaviour
{
    
    [SerializeField] BaseMenu baseMenu;
    [SerializeField] Animator _loseAnimator;
    [SerializeField] StartButton _startButton;

    private void Start()
    {
        SoundManager.instance.PlaySound(SoundManager.SoundNames.TrainWindTheme);
    }

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
