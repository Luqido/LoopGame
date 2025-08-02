using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [SerializeField] private Button attackButton;
    [SerializeField] private Button parryButton;
    [SerializeField] private Button dodgeButton;

    [SerializeField] private Player player;
    [SerializeField] private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        attackButton.onClick.AddListener(() => Select(Player.CombatOption.Attack));
        parryButton.onClick.AddListener(() => Select(Player.CombatOption.Parry));
        dodgeButton.onClick.AddListener(() => Select(Player.CombatOption.Dodge));
    }

    public void InitializeTurnUI(/*stats??*/)
    {
        canvasGroup.DOFade(1f, 0.5f);
        //todo init current turn ui according to current stats
    }

    private void Select(Player.CombatOption option)
    {
        player.CurrentCombatOption = option;
        canvasGroup.DOFade(0f, 0.5f);
    }
    
}