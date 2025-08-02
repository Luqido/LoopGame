using System;
using System.Collections;
using UnityEngine;

public class Player : Unit
{
    public enum CombatOption
    {
        Attack,
        Dodge,
        Parry
    }
    
    [SerializeField] private CombatUI combatUI;
    [SerializeField] private Animator slashAnimator;
    
    public Unit currentEnemy;

    public CombatOption? CurrentCombatOption { get; set; } = null;

    protected override void Awake()
    {
        base.Awake();
        HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int prevHp, int currentHp, int maxHp)
    {
        if (currentHp < prevHp)
        {
            slashAnimator.SetTrigger("Slash");
        }
    }

    public override IEnumerator ExecuteTurn()
    {
        combatUI.InitializeTurnUI();
        yield return new WaitUntil(() => CurrentCombatOption != null);
        switch (CurrentCombatOption)
        {
            case CombatOption.Attack:
                yield return AttackCoroutine(currentEnemy);
                break;
            case CombatOption.Dodge:
                break;
            case CombatOption.Parry:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        CurrentCombatOption = null;
    }
}