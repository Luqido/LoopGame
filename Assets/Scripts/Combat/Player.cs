using System;
using System.Collections;
using UnityEngine;

public class Player : Unit
{
    public enum CombatOption
    {
        Attack,
        Dodge,
        Block,
        SpecialAbility,
    }
    
    [SerializeField] private CombatUI combatUI;
    [SerializeField] private Animator slashAnimator;
    [SerializeField] private Animator smokeAnimator;
    [SerializeField] private int specialPerTurn;
    [SerializeField] private int specialDamage;

    private int _turnCount;
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
        yield return base.ExecuteTurn();

        _turnCount++;
        combatUI.InitializeTurnUI(_turnCount % specialPerTurn == 0);
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
            case CombatOption.SpecialAbility:
                if (CombatManager.Instance.enemies.Count == 1)
                {
                    yield return UseSpecialAbility(CombatManager.Instance.enemies[0]);
                }
                else
                {
                    Unit enemyToAttack = null;
                    combatUI.SelectEnemyToAttack((unit) => { enemyToAttack = unit; });
                    yield return new WaitUntil(() => enemyToAttack != null);
                    yield return UseSpecialAbility(enemyToAttack);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        CurrentCombatOption = null;
    }

    private IEnumerator UseSpecialAbility(Unit to)
    {
        animator.SetTrigger("SpecialAbility");
        yield return new WaitForSeconds(.8f);
        smokeAnimator.transform.position = to.GetComponent<SpriteRenderer>().bounds.center;
        smokeAnimator.SetTrigger("SpecialAbility");
        yield return new WaitForSeconds(0.6f);
        to.CurrentHp -= specialDamage;
        yield return new WaitForSeconds(0.9f);
    }
}