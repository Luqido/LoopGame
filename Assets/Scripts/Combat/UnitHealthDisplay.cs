using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthDisplay : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private SlicedFilledImage slicedFilledHealthBar;
    [SerializeField] private Unit unit;
    
    private void Awake()
    {
        unit.HealthChanged += UpdateHealthBar;
    }

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
}