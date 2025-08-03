using System.Collections;
using UnityEngine;

public class HatBoiEnemy : Enemy
{
    [SerializeField] private Unit clonePrefab;
    private static bool _cloneActive;
    
    public override IEnumerator ExecuteSpecial()
    {
        if (_cloneActive)
        {
            yield return ExecuteTurn(); // Keep executing turn until something else comes up
        }
        else
        {
            _cloneActive = true;
            var clone = Instantiate(clonePrefab, transform.position, Quaternion.identity);
            CombatManager.Instance.AddEnemy(clone, true);
            yield return new WaitForSeconds(1f);
        }
    }
}