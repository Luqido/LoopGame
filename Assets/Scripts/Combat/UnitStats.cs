using UnityEngine;

[CreateAssetMenu]
public class UnitStats : ScriptableObject
{
    public string unitName;
    public int health;
    public int baseDamage;
    [Range(0f, 1f)] public float blockPercentage;
    [Range(0f, 1f)] public float blockChance;
    [Range(0f, 1f)] public float dodgePercentage;
    [Range(0f, 1f)] public float dodgeChance;
}