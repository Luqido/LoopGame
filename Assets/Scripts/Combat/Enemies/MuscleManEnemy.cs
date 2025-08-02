using System.Collections;
using UnityEngine;

public class MuscleManEnemy : Enemy
{
    public override IEnumerator ExecuteSpecial()
    {
        AttackMultiplier += 0.2f;
        Debug.Log("Powered up! Attack multiplier: " + AttackMultiplier);
        yield break;
    }
}
