using System.Collections;
using UnityEngine;

public class GrandmaEnemy : Enemy
{
    [SerializeField] private int debuffTurnCount = 2;
    [SerializeField] private float debuffMultiplier = .5f;
    
    private int _remainingTurnsToDebuffWearOff;

    public override IEnumerator ExecuteTurn()
    {
        if (_remainingTurnsToDebuffWearOff > 0)
        {
            if (--_remainingTurnsToDebuffWearOff == 0)
            {
                Debug.Log("Debuff wears off");
                CombatManager.Instance.player.AttackMultiplier /= debuffMultiplier;
            }
        }
        
        return base.ExecuteTurn();
    }

    public override IEnumerator ExecuteSpecial()
    {
        //todo book anim smt??
        CombatManager.Instance.player.AttackMultiplier *= debuffMultiplier;
        _remainingTurnsToDebuffWearOff += debuffTurnCount;
        Debug.Log($"Player debuffed with {debuffMultiplier} for {_remainingTurnsToDebuffWearOff} turns.");
        yield break;
    }
}
