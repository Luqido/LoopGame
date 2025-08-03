
using System.Collections;
using UnityEngine;

public class FanBoiEnemy : Enemy
{
    [SerializeField] private int playerWontGetHarmedTurnCount = 3;
    private bool _playerHurtMeThisTurn;
    private Coroutine _sayRoutine;
    
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
            _sayRoutine = StartCoroutine(CombatManager.Instance.ui.Say(this, "What have I done to you..?"));
        }
    }

    public override IEnumerator ExecuteTurn()
    {
        if (playerWontGetHarmedTurnCount > 0)
        {
            if (_sayRoutine != null)
            {
                yield return _sayRoutine;
            }
            yield return CombatManager.Instance.ui.Say(this, "I refuse to attack you.");
            yield break;
        }
        yield return base.ExecuteTurn();
    }
}
