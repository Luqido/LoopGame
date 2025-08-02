using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


//TODO Player selection 
//TODO Player stats
//TODO Enemy turn, ai, stats
//TODO Animation of combat
//

public class CombatManager : MonoBehaviour
{
    public UnityEvent onTurnEnd;
    public UnityEvent onTurnStart;
    public Player player;
    public List<Unit> enemies = new();
    
    [Header("Debug")]
    [SerializeField] private Unit debugEnemy;
    [SerializeField] private Unit[] debugEnemies;
    public bool IsPlayerTurn { get; private set; } = true;
    public static CombatManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if(debugEnemy)
        {
            StartCoroutine(StartCombat(debugEnemy));
        }
        else if (debugEnemies.Length > 0)
        {
            StartCoroutine(StartCombat(debugEnemies));
        }
    }

    public IEnumerator StartCombat(params Unit[] against)
    {
        enemies.AddRange(against);
        while (enemies.Count > 0)
        {
            yield return ExecuteNextTurn();
        }

        Debug.Log("Won");
    }

    private IEnumerator ExecuteNextTurn()
    {
        onTurnStart.Invoke();
        if (IsPlayerTurn)
        {
            yield return player.ExecuteTurn();
        }
        else
        {
            foreach (var enemy in enemies)
            {
                yield return enemy.ExecuteTurn();
            }
        }
        onTurnEnd.Invoke();
        
        IsPlayerTurn = !IsPlayerTurn;
    } 
}