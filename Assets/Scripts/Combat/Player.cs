using System;
using System.Collections;
using UnityEngine;

public class Player : Unit
{
    public enum CombatOption
    {
        Attack,
        Dodge,
        Block
    }
    
    [SerializeField] private CombatUI combatUI;
    [SerializeField] private Animator slashAnimator;
    
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
                if(CombatManager.Instance.enemies.Count == 1)
                {
                    yield return AttackCoroutine(CombatManager.Instance.enemies[0]);
                }
                else
                {
                    Unit enemyToAttack = null;
                    combatUI.SelectEnemyToAttack((unit) =>
                    {
                        enemyToAttack = unit;
                    });
                    yield return new WaitUntil(() => enemyToAttack != null);
                    yield return AttackCoroutine(enemyToAttack);
                }
                break;
            case CombatOption.Dodge:
                yield return DodgeCoroutine();
                break;
            case CombatOption.Block:
                yield return BlockCoroutine();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        CurrentCombatOption = null;
    }
}