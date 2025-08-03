using System.Collections;
using UnityEngine;

public class MuscleManEnemy : Enemy
{
    public override IEnumerator ExecuteSpecial()
    {
        AttackMultiplier += 0.2f;
        yield return CombatManager.Instance.ui.Say(this, "Hell yeah, here is nothing like after gym pump.\n<i>STRENGTH +%20</i>");
    }
}
