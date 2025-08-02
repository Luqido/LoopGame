using System;
using System.Collections;

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
    [SerializeField] private BasicEnemy enemy;
    private bool _isPlayerTurn;
    public static CombatManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        player.currentEnemy = enemy;
        StartCoroutine(StartCombat());
    }

    private IEnumerator StartCombat()
    {
        while (true)
        {
            yield return ExecuteNextTurn();
        }
    }

    private IEnumerator ExecuteNextTurn()
    {
        if (_isPlayerTurn)
        {
            yield return player.ExecuteTurn();
        }
        else
        {
            yield return enemy.ExecuteTurn();
        }

        yield return new WaitForSeconds(1f);
        _isPlayerTurn = !_isPlayerTurn;
    } 
}