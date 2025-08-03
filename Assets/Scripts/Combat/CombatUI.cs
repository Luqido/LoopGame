using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CombatUI : MonoBehaviour
{
    [SerializeField] private Button specialAbilityButton;
    [SerializeField] private Button attackButton;
    [SerializeField] private Button blockButton;
    [SerializeField] private Button dodgeButton;
    [SerializeField] private CanvasScaler canvasScaler;
    [SerializeField] private CanvasGroup canvasGroup;
    [SerializeField] private Button[] enemySelectionButtons;
    [SerializeField] private UnitHealthDisplay healthDisplayPrefab;
    
    private void Awake()
    {
        specialAbilityButton.onClick.AddListener(() => Select(Player.CombatOption.SpecialAbility));
        attackButton.onClick.AddListener(() => Select(Player.CombatOption.Attack));
        blockButton.onClick.AddListener(() => Select(Player.CombatOption.Block));
        dodgeButton.onClick.AddListener(() => Select(Player.CombatOption.Dodge));
    }

    private void Start()
    {
        CreateHealthBar(CombatManager.Instance.player);
    }

    public void InitializeTurnUI(/*stats??*/)
    {
        canvasGroup.DOFade(1f, 0.5f);
        //todo init current turn ui according to current stats
    }

    private void Select(Player.CombatOption option)
    {
        CombatManager.Instance.player.CurrentCombatOption = option;
        canvasGroup.DOFade(0f, 0.5f);
    }

    public void SelectEnemyToAttack(Action<Unit> setter)
    {
        for (var i = 0; i < CombatManager.Instance.enemies.Count; i++)
        {
            var i1 = i;
            enemySelectionButtons[i].gameObject.SetActive(true);
            var rectTransform = enemySelectionButtons[i1].transform as RectTransform;
            var bounds = CombatManager.Instance.enemies[i1].GetComponent<SpriteRenderer>().bounds;
            
            var refRes = canvasScaler.referenceResolution;

            var min = RectTransformUtility.WorldToScreenPoint(Camera.main, bounds.min);
            min = new Vector2(min.x / Screen.width * refRes.x, min.y / Screen.height * refRes.y);
            var max = RectTransformUtility.WorldToScreenPoint(Camera.main, bounds.max);
            max = new Vector2(max.x / Screen.width * refRes.x, max.y / Screen.height * refRes.y);
            
            rectTransform.anchoredPosition = min - Vector2.one * 30;
            rectTransform.sizeDelta = max - min + Vector2.one * 60;
            
            enemySelectionButtons[i1].onClick.AddListener(() =>
            {
                setter.Invoke(CombatManager.Instance.enemies[i1]);
                
                DisableEnemySelectionButtons();
            });
        }
    }

    private void DisableEnemySelectionButtons()
    {
        foreach (var button in enemySelectionButtons)
        {
            button.onClick.RemoveAllListeners();
            button.gameObject.SetActive(false);
        }
    }

    public void CreateHealthBar(Unit unit)
    {
        var unitHealthDisplay = Instantiate(healthDisplayPrefab, transform);
        unitHealthDisplay.Initialize(unit, canvasScaler.referenceResolution);
    }
}