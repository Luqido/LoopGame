using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text nameText2;
    [SerializeField] private Image healthBar;
    [SerializeField] private SlicedFilledImage slicedFilledHealthBar;
    [SerializeField] private Unit unit;
    [SerializeField] private Color playerColor = Color.green;
    [SerializeField] private Color enemyColor = Color.red;
    
    private RectTransform _rectTransform;
    private Vector2 _refRes;

    private void UpdateHealthBar(int previousHp, int currentHp, int startingHp)
    {
        if (healthBar != null)
            healthBar.DOFillAmount((float)currentHp / startingHp, 0.3f);

        if (slicedFilledHealthBar != null)
            DOTween.To(
                () => slicedFilledHealthBar.fillAmount, 
                (f) => slicedFilledHealthBar.fillAmount = f,
                (float)currentHp / startingHp, 
                0.3f);

        var oldHp = previousHp;
        
        DOTween.To(
            () => oldHp,
            x => {
                oldHp = x;
                nameText.text = $"{unit.stats.unitName} ({oldHp}/{startingHp})";
                nameText2.text = $"{unit.stats.unitName} ({oldHp}/{startingHp})";
            },
            currentHp,
            0.3f
        ).SetEase(Ease.OutCubic);

        if (currentHp <= 0)
        {
            Destroy(gameObject, 1f);
        }
    }

    private void OnDestroy()
    {
        if (unit)
        {
            unit.HealthChanged -= UpdateHealthBar;
        }
    }

    public void Initialize(Unit unit, bool isPlayer)
    {
        this.unit = unit;
        unit.HealthChanged += UpdateHealthBar;
        // _refRes = referenceResolution;
        _rectTransform = transform as RectTransform;
        slicedFilledHealthBar.color = isPlayer ? playerColor : enemyColor;
        nameText2.color = isPlayer ? playerColor : enemyColor;
        nameText.text = $"{unit.stats.unitName} ({unit.startingHealth}/{unit.startingHealth})";
        nameText2.text = $"{unit.stats.unitName} ({unit.startingHealth}/{unit.startingHealth})";
        // _rectTransform.anchorMin = Vector2.zero;
        // _rectTransform.anchorMax = Vector2.zero;
        // _rectTransform.sizeDelta = new Vector2(200, 50);
    }

    // private void LateUpdate()
    // {
    //     if (unit)
    //     {
    //         var position = RectTransformUtility.WorldToScreenPoint(Camera.main, unit.healthBarPosition.position);
    //         position = new Vector2(position.x / Screen.width * _refRes.x, position.y / Screen.height * _refRes.y);
    //         _rectTransform.anchoredPosition = position;
    //     }
    // }
}