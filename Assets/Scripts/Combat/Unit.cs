using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public abstract class Unit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform damageTakePosition;
    [SerializeField] private UnitStats stats;
    public event UnityAction<int, int, int> HealthChanged;
    
    private Vector3 _startPosition;
    private int _currentHp;
    public int CurrentHp
    {
        get => _currentHp;
        set
        {
            if (value < 0)
            {
                value = 0;
            }
            
            HealthChanged?.Invoke(_currentHp, value, stats.health);
            _currentHp = value;
        }
    }
    protected virtual void Awake()
    {
        _startPosition = transform.position;
    }

    private void Start()
    {
        CurrentHp = stats.health;
    }

    public IEnumerator AttackCoroutine(Unit to)
    {
        yield return transform.DOMove(to.damageTakePosition.position, 0.6f).SetEase(Ease.InCubic).WaitForCompletion();
        animator.SetTrigger("Attack");
        to.CurrentHp -= stats.baseDamage;
        yield return new WaitForSeconds(0.5f);
        yield return transform.DOMove(_startPosition, 1f).WaitForCompletion();
    }

    public abstract IEnumerator ExecuteTurn();
}