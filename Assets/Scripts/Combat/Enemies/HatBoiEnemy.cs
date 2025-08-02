using System.Collections;
using UnityEngine;

public class HatBoiEnemy : Enemy
{
    [SerializeField] private HatBoiClone clonePrefab;
    private bool _cloneActive;
    
    public override IEnumerator ExecuteSpecial()
    {
        if(_cloneActive) yield break;
        _cloneActive = true;
        var clone = Instantiate(clonePrefab, transform.position, Quaternion.identity);
        CombatManager.Instance.AddEnemy(clone, true);
        yield return new WaitForSeconds(1f);
    }
}