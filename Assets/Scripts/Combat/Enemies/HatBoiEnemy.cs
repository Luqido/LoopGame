using System.Collections;
using UnityEngine;

public class HatBoiEnemy : Enemy
{
    [SerializeField] private Unit clonePrefab;
    private static bool _cloneActive;
    [SerializeField] private string[] cloneLines;
    public override IEnumerator ExecuteSpecial()
    {
        if (_cloneActive)
        {
            yield return ExecuteTurn(); // Keep executing turn until something else comes up
        }
        else
        {
            yield return CombatManager.Instance.ui.Say(this, cloneLines[Random.Range(0, cloneLines.Length)]);
            _cloneActive = true;
            var clone = Instantiate(clonePrefab, transform.position, Quaternion.identity);
            CombatManager.Instance.AddEnemy(clone, true);
            yield return new WaitForSeconds(1f);
        }
    }
}