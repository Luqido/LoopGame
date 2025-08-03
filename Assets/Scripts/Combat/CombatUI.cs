using System;
using System.Collections;
using DG.Tweening;
using TMPro;
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
    [SerializeField] private Transform healthBarParent;
    [SerializeField] private TMP_Text descriptionText;
    private Unit _currentTooltipUnit;

    private void Awake()
    {
        specialAbilityButton.onClick.AddListener(() => Select(Player.CombatOption.SpecialAbility));
        attackButton.onClick.AddListener(() => Select(Player.CombatOption.Attack));
        blockButton.onClick.AddListener(() => Select(Player.CombatOption.Block));
        dodgeButton.onClick.AddListener(() => Select(Player.CombatOption.Dodge));
    }

    private void Start()
    {
        CreateHealthBar(CombatManager.Instance.player, true);
    }

    public void InitializeTurnUI(bool specialEnabled)
    {
        specialAbilityButton.interactable = specialEnabled;
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

    public void CreateHealthBar(Unit unit, bool isPlayer)
    {
        var unitHealthDisplay = Instantiate(healthDisplayPrefab, healthBarParent);
        unitHealthDisplay.Initialize(unit, isPlayer);
    }

    [Header("Talk Bubble")]
    [SerializeField] private RectTransform talkBubbleTransform;
    [SerializeField] private CanvasGroup talkBubbleCanvasGroup;
    [SerializeField] private TMP_Text talkBubbleText;
    [SerializeField] private GameObject descriptionObject;


    public IEnumerator Say(Unit unit, string message)
    {
        var position = RectTransformUtility.WorldToScreenPoint(Camera.main, unit.talkBubblePosition.position);
        position = new Vector2(position.x / Screen.width * canvasScaler.referenceResolution.x,
            position.y / Screen.height * canvasScaler.referenceResolution.y);
        talkBubbleTransform.anchoredPosition = position;

        talkBubbleText.text = "";
        talkBubbleCanvasGroup.DOFade(1f, 0.3f);
        var waitForSeconds = new WaitForSeconds(0.05f);
        foreach (var ch in message)
        {
            talkBubbleText.text += ch;
            yield return waitForSeconds;
        }

        yield return new WaitForSeconds(1.8f);
        yield return talkBubbleCanvasGroup.DOFade(0f, 0.3f).WaitForCompletion();
    }


    public void ShowTooltip(Unit unit, string desc)
    {
        _currentTooltipUnit = unit;
        descriptionObject.SetActive(true);
        descriptionText.text = desc;
    }

    public void HideTooltip(Unit unit)
    {
        if (_currentTooltipUnit == unit)
            descriptionObject.SetActive(false);
    }
}