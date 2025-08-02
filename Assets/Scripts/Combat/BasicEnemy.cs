using System;
using System.Collections;
using UnityEngine;

public class BasicEnemy : Unit
{
    [SerializeField] private Player player;
    
    public override IEnumerator ExecuteTurn()
    {
        yield return AttackCoroutine(player);
    }
}