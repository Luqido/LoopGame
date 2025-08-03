using System.Collections;
using UnityEngine;

public class Enemy : Unit
{
    protected override void Awake()
    {
        base.Awake();
        startingHealth = (int)(CombatManager.Instance.player.startingHealth 
                               * Random.Range
                               (
                                   CombatManager.Instance.commonEnemyStats.enemyHealthMultiplierMin, 
                                   CombatManager.Instance.commonEnemyStats.enemyHealthMultiplierMax
                               ) 
                               * aiProbabilities.hpMultiplier
                               );
        currentDamage = (int)(CombatManager.Instance.player.currentDamage
                              * Random.Range
                              (
                                  CombatManager.Instance.commonEnemyStats.enemyDamageMultiplierMin,
                                  CombatManager.Instance.commonEnemyStats.enemyDamageMultiplierMax
                              )
                              * aiProbabilities.damageMultiplier
            );
    }

    [SerializeField] protected EnemyAIProbabilities aiProbabilities;

    public override IEnumerator ExecuteTurn()
    {
        yield return base.ExecuteTurn();

        var randomValue = Random.value;

        if (randomValue < aiProbabilities.attackUseChance)
        {
            yield return AttackCoroutine(CombatManager.Instance.player);
            yield break;
        }
        randomValue -= aiProbabilities.attackUseChance;
        
        if (randomValue < aiProbabilities.dodgeUseChance)
        {
            yield return DodgeCoroutine();
            yield break;
        }
        randomValue -= aiProbabilities.dodgeUseChance;
        
        if (randomValue < aiProbabilities.blockUseChance)
        {
            yield return BlockCoroutine();
            yield break;
        }
        randomValue -= aiProbabilities.blockUseChance;
        
        if (randomValue < aiProbabilities.specialUseChance)
        {
            yield return ExecuteSpecial();
        }
    }

    public virtual IEnumerator ExecuteSpecial()
    {
        yield break;
    }
}