using UnityEngine;

[CreateAssetMenu]
public class PlayerStats : ScriptableObject
{
    public int baseHealth;
    [Range(0f, 1f)] public float healthIncreasePercentage; 
    public int baseDamage;
    [Range(0f, 1f)] public float damageIncreasePercentage;
    [Range(0f, 1f)] public float luckIncreasePercentage;
    [Range(0f, 1f)] public float defenseIncreasePercentage;
    public int baseSpecialDamage = 30;
    public int specialPerTurn = 3;
}