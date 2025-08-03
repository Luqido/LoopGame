using UnityEngine;

[CreateAssetMenu]
public class EnemyAIProbabilities : ScriptableObject
{
    [Range(0f, 1f)] public float attackUseChance;
    [Range(0f, 1f)] public float dodgeUseChance;
    [Range(0f, 1f)] public float blockUseChance;
    [Range(0f, 1f)] public float specialUseChance;
    
    [Header("Multipliers")]
    public float damageMultiplier = 1f;
    public float hpMultiplier = 1f;
    
}