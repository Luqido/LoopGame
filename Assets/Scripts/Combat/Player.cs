using System;
using System.Collections;
using UnityEngine;

public class PlayerLevels
{
    private static PlayerLevels _instance;
    private static readonly object _lock = new();

    public static PlayerLevels Instance
    {
        get
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = new PlayerLevels();
                    }
                }
            }

            return _instance;
        }
    }

    // Private constructor ensures no external instantiation
    private PlayerLevels() { }

    public int healthLevel;
    public int attackLevel;
    public int defenseLevel;
    public int luckLevel;

    public void Reset()
    {
        healthLevel = 0;
        attackLevel = 0;
        defenseLevel = 0;
        luckLevel = 0;
    }
}

public class Player : Unit
{
    [SerializeField] private PlayerStats playerStats;
    
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

    private int _turnCount;
    public CombatOption? CurrentCombatOption { get; set; } = null;
    private int currentSpecialDamage;

    protected override void Awake()
    {
        base.Awake();
        
        HealthChanged += OnHealthChanged;
        ApplyUpgrade();
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
        combatUI.InitializeTurnUI(_turnCount % playerStats.specialPerTurn == 0);
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
        to.CurrentHp -= currentSpecialDamage;
        yield return new WaitForSeconds(0.9f);
    }
    
    // can
    // atak
    // blok
    // luck

    public override float BlockAbsorbPercentage => stats.blockPercentage + PlayerLevels.Instance.defenseLevel * playerStats.defenseIncreasePercentage;
    public override float DodgeHitChance =>  stats.dodgeChance + PlayerLevels.Instance.luckLevel * playerStats.luckIncreasePercentage;

    public void ApplyUpgrade()
    {
        startingHealth = (int)(playerStats.baseHealth * (1 + PlayerLevels.Instance.healthLevel * playerStats.healthIncreasePercentage));
        currentDamage = (int)(playerStats.baseDamage * (1 + PlayerLevels.Instance.attackLevel * playerStats.damageIncreasePercentage));
        currentSpecialDamage = (int)(playerStats.baseSpecialDamage * (1 + PlayerLevels.Instance.attackLevel * playerStats.damageIncreasePercentage));
    }
}