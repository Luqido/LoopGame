using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

[Flags]
public enum EnemyType
{
    NormalAdam = 1 << 0,
    Tazi = 1 << 1,
    MuscleMan = 1 << 2,
    HatBoi = 1 << 3,
    FanBoi = 1 << 4,
    Grandma = 1 << 5,
    LariyeCroft = 1 << 6,
}

public class CombatManager : MonoBehaviour
{
    public UnityEvent onTurnEnd;
    public UnityEvent onTurnStart;
    
    [Header("References")] 
    public Player player;
    public List<Unit> enemies = new();
    [SerializeField] public CombatUI ui;
    [SerializeField] private List<Unit> enemyPrefabs;
    [Header("Positioning")]
    [SerializeField] private Transform enemyInitialPosition;
    [SerializeField] private float distanceBetweenEnemies;
    [Header("Debug")]
    [SerializeField] private Unit[] debugEnemies;
    [SerializeField] Animator HamAnim;
    [SerializeField] CanvasGroup canvas;
    
    public bool IsPlayerTurn { get; private set; } = true;
    public static CombatManager Instance { get; private set; }
    private static EnemyType _enemyToFightAgainst;
    public CommonEnemyStats commonEnemyStats;

    public static void SetEnemiesToFightAgainst(params EnemyType[] enemyTypes)
    {
        _enemyToFightAgainst = 0;
        foreach (var enemyType in enemyTypes)
        {
            _enemyToFightAgainst |= enemyType;
        }
    }
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (_enemyToFightAgainst != 0)
        {
            List<Unit> enemiesToSummon = new();
            for (int i = 0; i < 7; i++)
            {
                if ((_enemyToFightAgainst & (EnemyType)(1 << i)) != 0)
                {
                    var enemy = Instantiate(enemyPrefabs[i]);
                    enemiesToSummon.Add(enemy);
                }
            }

            StartCoroutine(StartCombat(enemiesToSummon.ToArray()));
            // _enemyToFightAgainst = 0;
        }
        else if (debugEnemies.Length > 0)
        {
            StartCoroutine(StartCombat(debugEnemies));
        }
    }

    public IEnumerator StartCombat(params Unit[] against)
    {
        foreach (var unit in against)
        {
            AddEnemy(unit);
        }
        
        while (enemies.Count > 0)
        {
            yield return ExecuteNextTurn();
        }

        GameWon();
        
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
            var count = enemies.Count;
            for (var i = 0; i < count; i++)
            {
                yield return enemies[i].ExecuteTurn();
            }
        }
        onTurnEnd.Invoke();
        
        IsPlayerTurn = !IsPlayerTurn;
    }

    public void AddEnemy(Unit enemy, bool animate = false)
    {
        enemies.Add(enemy);
        ui.CreateHealthBar(enemy, false);
        
        RepositionEnemies(animate);
    }

    public void RepositionEnemies(bool animate = false)
    {
        var offset = distanceBetweenEnemies * (enemies.Count - 1) / -2f; 
        if (!animate)
        {
            foreach (var unit in enemies)
            {
                unit.transform.position = enemyInitialPosition.position + offset * Vector3.right;
                offset += distanceBetweenEnemies;
            }
        }
        else
        {
            foreach (var unit in enemies)
            {
                unit.transform.DOMove(enemyInitialPosition.position + offset * Vector3.right, 0.4f);
                offset += distanceBetweenEnemies;
            }
        }
    }

    public void GameLost()
    {
        Debug.Log("You lost");
        PlayerLevels.Instance.Reset();
        canvas.DOFade(1f, 0.3f);
        HamAnim.SetTrigger("HConductor");
        Invoke(nameof(OpenMenuScene), 4f);
    }

    private void OpenMenuScene()
    {
        SceneManager.LoadScene(0);
    }

    private void GameWon()
    {
        Debug.Log("CONGRATS");
        if (_enemyToFightAgainst.HasFlag(EnemyType.NormalAdam))
        {
            PeopleDoTween.LastBeatenEnemy = EnemyType.NormalAdam;
        }
        else
        {
            PeopleDoTween.LastBeatenEnemy = _enemyToFightAgainst;
        }


        canvas.DOFade(1f, 0.5f);
        switch (PeopleDoTween.LastBeatenEnemy)
        {
            case EnemyType.NormalAdam:
                HamAnim.SetTrigger("HNormalAdam");
                break;
            case EnemyType.MuscleMan:
                HamAnim.SetTrigger("HKasliAdam");
                break;
            case EnemyType.HatBoi:
                HamAnim.SetTrigger("HSapkali");
                break;
            case EnemyType.FanBoi:
                HamAnim.SetTrigger("HFanBoi");
                break;
            case EnemyType.Grandma:
                HamAnim.SetTrigger("HYasliKadin");
                break;

            default:
                Debug.LogWarning("Tetiklenecek animasyon bulunamad�.");
                break;
        }

        StartCoroutine(WaitForAnimAndContinue());

    }

    private void OnDestroy()
    {
        _enemyToFightAgainst = 0;
    }
    private IEnumerator WaitForAnimAndContinue()
    {
        yield return new WaitForSeconds(4f); // animasyon s�resi kadar bekle
        SceneManager.LoadScene(1); // veya ba�ka sahne
    }
}