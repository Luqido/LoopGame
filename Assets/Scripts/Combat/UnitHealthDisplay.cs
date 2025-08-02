using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthDisplay : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private SlicedFilledImage slicedFilledHealthBar;
    [SerializeField] private Unit unit;
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
    }

    public void Initialize(Unit unit, Vector2 referenceResolution)
    {
        this.unit = unit;
        unit.HealthChanged += UpdateHealthBar;
        _refRes = referenceResolution;
        _rectTransform = transform as RectTransform;

        _rectTransform.anchorMin = Vector2.zero;
        _rectTransform.anchorMax = Vector2.zero;
        _rectTransform.sizeDelta = new Vector2(200, 50);
    }

    private void LateUpdate()
    {
        if (unit)
        {
            var position = RectTransformUtility.WorldToScreenPoint(Camera.main, unit.healthBarPosition.position);
            position = new Vector2(position.x / Screen.width * _refRes.x, position.y / Screen.height * _refRes.y);
            _rectTransform.anchoredPosition = position;
        }
    }
}