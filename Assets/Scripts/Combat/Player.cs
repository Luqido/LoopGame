using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, ICombatPlayer
{
    public enum CombatOption
    {
        Attack,
        Dodge,
        Parry
    }
    
    [SerializeField] private CombatUI combatUI;
    [SerializeField] private PlayerStats stats;

    public CombatOption? CurrentCombatOption { get; set; } = null;
    public IEnumerator ExecuteTurn()
    {
        combatUI.InitializeTurnUI();
        yield return new WaitUntil(() => CurrentCombatOption != null);
        
        yield break;
    }

    public void TakeDamage(int amount)
    {
        stats.health -= amount;
    }
}