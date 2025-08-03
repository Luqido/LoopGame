using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class HatBoiEnemy : Enemy
{
    [SerializeField] private Unit clonePrefab;
    public static bool CloneActive;
    [SerializeField] private string[] cloneLines;
    public override IEnumerator ExecuteSpecial()
    {
        if (CloneActive)
        {
            yield return ExecuteTurn(); // Keep executing turn until something else comes up
        }
        else
        {
            yield return CombatManager.Instance.ui.Say(this, cloneLines[Random.Range(0, cloneLines.Length)]);
            CloneActive = true;
            var clone = Instantiate(clonePrefab, transform.position, Quaternion.identity);
            CombatManager.Instance.AddEnemy(clone, true);
            yield return new WaitForSeconds(1f);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        CloneActive = false;
    }
}