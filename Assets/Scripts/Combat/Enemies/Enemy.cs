using System.Collections;
using UnityEngine;

public abstract class Enemy : Unit
{
    [Range(0f, 1f)] [SerializeField] protected float attackChance;
    [Range(0f, 1f)] [SerializeField] protected float dodgeChance;
    [Range(0f, 1f)] [SerializeField] protected float blockChance;
    [Range(0f, 1f)] [SerializeField] protected float specialChance;

    public override IEnumerator ExecuteTurn()
    {
        if (_activeSkills.HasFlag(UnitSkill.Block))
        {
            WearOffBlock();
            yield return new WaitForSeconds(1f);
        }
        else if (_activeSkills.HasFlag(UnitSkill.Dodge))
        {
            WearOffDodge();
            yield return new WaitForSeconds(1f);
        }
        var randomValue = Random.value;

        if (randomValue < attackChance)
        {
            yield return AttackCoroutine(CombatManager.Instance.player);
        }
        else if (randomValue < attackChance + dodgeChance)
        {
            yield return DodgeCoroutine();
        }
        else if (randomValue < attackChance + dodgeChance + blockChance)
        {
            yield return BlockCoroutine();
        }
        else if (randomValue < attackChance + dodgeChance + blockChance + specialChance)
        {
            yield return ExecuteSpecial();
        }
    }

    public abstract IEnumerator ExecuteSpecial();
}