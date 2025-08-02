
using System.Collections;
using UnityEngine;

public class FanBoiEnemy : Enemy
{
    [SerializeField] private int playerWontGetHarmedTurnCount = 3;
    private bool _playerHurtMeThisTurn;
    
    protected override void Start()
    {
        base.Start();
        HealthChanged += OnHealthChanged;
    }

    private void OnHealthChanged(int oldHp, int newHp, int maxHp)
    {
        if (newHp < oldHp)
        {
            _playerHurtMeThisTurn = true;
            playerWontGetHarmedTurnCount--;
            Debug.Log("What have I done to you..."); //todo
        }
    }

    public override IEnumerator ExecuteTurn()
    {
        if (playerWontGetHarmedTurnCount > 0)
        {
            Debug.Log("I refuse to attack you.");
            yield break;
        }
        yield return base.ExecuteTurn();
    }
}
