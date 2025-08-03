using UnityEngine;

[CreateAssetMenu]
public class UnitStats : ScriptableObject
{
    public string unitName;
    [Multiline] public string shortDescription;
    [Range(0f, 1f)] public float blockPercentage;
    [Range(0f, 1f)] public float dodgeChance;
}