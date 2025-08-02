using System;
using System.Collections;
using UnityEngine;

public class BasicEnemy : Unit
{
    public override IEnumerator ExecuteTurn()
    {
        yield return AttackCoroutine(CombatManager.Instance.player);
    }
}