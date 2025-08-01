using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UnitHealthDisplay : MonoBehaviour
{
    [SerializeField] private Image healthBar;
    [SerializeField] private Unit unit;

    private void Awake()
    {
        unit.HealthChanged += UpdateHealthBar;
    }

    private void UpdateHealthBar(int previousHp, int currentHp, int startingHp)
    {
        healthBar.DOFillAmount((float)currentHp / startingHp, 0.3f);
    }
}