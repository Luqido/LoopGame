using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, ICombatPlayer
{
    [SerializeField] private Player player;
    
    public IEnumerator ExecuteTurn()
    {
        player.TakeDamage(100);
        Debug.Log("GWAAAK GWAAAK");
        yield break;
    }
}