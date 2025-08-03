using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class HatBoiEnemy : Enemy
{
    [SerializeField] private HatBoiClone clonePrefab;
    public static bool CloneActive;
    [SerializeField] private string[] cloneLines;
    private HatBoiClone _clone;
    

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
            _clone = Instantiate(clonePrefab, transform.position, Quaternion.identity);
            _clone.Enemy = this;
            CombatManager.Instance.AddEnemy(_clone, true);
            yield return new WaitForSeconds(1f);
        }
    }
}