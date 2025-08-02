using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

[Flags]
public enum UnitSkill
{
    Block = 1 << 0,
    Dodge = 1 << 1,
}
public abstract class Unit : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Transform damageTakePosition;
    public Transform healthBarPosition;
    [SerializeField] private UnitStats stats;
    [SerializeField] private SpriteRenderer blockSprite;
    [SerializeField] private SpriteRenderer dodgeSprite;
    protected UnitSkill _activeSkills;
    
    public event UnityAction<int, int, int> HealthChanged;
    
    private Vector3 _startPosition;
    private int _currentHp;
    public int CurrentHp
    {
        get => _currentHp;
        set
        {
            if (value < _currentHp)
            {
                if (_activeSkills.HasFlag(UnitSkill.Block))
                {
                    if (stats.blockChance > Random.value)
                    {
                        var oldValue = value;
                        var diff = _currentHp - value;
                        diff = (int)(diff * (stats.blockPercentage));
                        value += diff;
                        Debug.Log($"Blocked! Old: {oldValue}, new: {value}");
                    }
                }
                else if (_activeSkills.HasFlag(UnitSkill.Dodge))
                {
                    if (stats.dodgeChance > Random.value)
                    {
                        var oldValue = value;
                        var diff = _currentHp - value;
                        diff = (int)(diff * (stats.dodgePercentage));
                        value += diff;
                        Debug.Log($"Dodged! Old: {oldValue}, new: {value}");
                    }
                }

                animator.SetTrigger("TakeDamage");
            }
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
        // _startPosition = transform.position;
    }

    protected virtual void Start()
    {
        CurrentHp = stats.health;
    }
    
    private void AddSkill(UnitSkill skill)
    {
        _activeSkills |= skill;
    }
    
    private void RemoveSkill(UnitSkill skill)
    {
        _activeSkills ^= skill;
    }

    public IEnumerator AttackCoroutine(Unit to)
    {
        _startPosition = transform.position;
        yield return transform.DOMove(to.damageTakePosition.position, 0.6f).SetEase(Ease.InCubic).WaitForCompletion();
        animator.SetTrigger("Attack");
        to.CurrentHp -= stats.baseDamage;
        yield return new WaitForSeconds(0.3f);
        yield return transform.DOMove(_startPosition, 1f).WaitForCompletion();
    }

    public IEnumerator DodgeCoroutine()
    {
        AddSkill(UnitSkill.Dodge);
        yield return dodgeSprite.DOFade(1f, 1f).WaitForCompletion();
        yield return new WaitForSeconds(0.3f);
        // CombatManager.Instance.onTurnStart.AddListener(WearOffDodge);
    }

    protected void WearOffDodge()
    {
        RemoveSkill(UnitSkill.Dodge);
        // CombatManager.Instance.onTurnStart.RemoveListener(WearOffDodge);
        dodgeSprite.DOFade(0f, 1f);
    }

    public IEnumerator BlockCoroutine()
    {
        AddSkill(UnitSkill.Block);
        yield return blockSprite.DOFade(1f, 1f).WaitForCompletion();
        yield return new WaitForSeconds(0.3f);
        // CombatManager.Instance.onTurnStart.AddListener(WearOffBlock);
    }

    protected void WearOffBlock()
    {
        RemoveSkill(UnitSkill.Block);
        // CombatManager.Instance.onTurnStart.RemoveListener(WearOffBlock);
        blockSprite.DOFade(0f, 1f);
    }
    public abstract IEnumerator ExecuteTurn();
}