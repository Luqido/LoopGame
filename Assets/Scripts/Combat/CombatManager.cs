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
    
    private ICombatPlayer _player;
    private ICombatPlayer _enemy;
    private bool _isPlayerTurn;

    private void Start()
    {
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
            yield return _player.ExecuteTurn();
        }
        else
        {
            yield return _enemy.ExecuteTurn();
        }

        _isPlayerTurn = !_isPlayerTurn;
    } 
}