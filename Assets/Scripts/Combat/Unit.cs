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
    [SerializeField] protected Animator animator;
    [SerializeField] private Transform damageTakePosition;
    public Transform healthBarPosition;
    [SerializeField] private UnitStats stats;
    [SerializeField] private SpriteRenderer blockSprite;
    [SerializeField] private SpriteRenderer dodgeSprite;
    [SerializeField] private float blockSpriteTargetOpaqueness = 1f;
    [SerializeField] private float dodgeSpriteTargetOpaqueness = 1f;
    [SerializeField] private Vector3 dodgeMovement;
    
    protected UnitSkill _activeSkills;
    
    public event UnityAction<int, int, int> HealthChanged;

    public float AttackMultiplier { get; set; } = 1f;
    
    private Vector3 _startPosition;
    private int _currentHp = 1;
    public int CurrentHp
    {
        get => _currentHp;
        set
        {
            if (_currentHp <= 0) return;
            
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
                        transform.DOMove(dodgeMovement, 0.2f).SetRelative(true).SetLoops(2, LoopType.Yoyo);
                    }
                }

                animator.SetTrigger("TakeDamage");
            }
            if (value < 0)
            {
                value = 0;
                if (CombatManager.Instance.enemies.Contains(this))
                {
                    CombatManager.Instance.enemies.Remove(this);
                }

                if (CombatManager.Instance.player == this as Player)
                {
                    Debug.Log("LOST");
                }
                
                transform.DOMoveY(0.4f, 1f).SetDelay(0.6f).SetRelative(true);
                GetComponent<SpriteRenderer>().DOFade(0.1f, 1f).SetDelay(0.6f);
                blockSprite.DOFade(0f, 1f).SetDelay(0.6f);
                dodgeSprite.DOFade(0f, 1f).SetDelay(0.6f);
                Destroy(gameObject, 1.61f);
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
        transform.DOMove(to.damageTakePosition.position, 0.6f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(0.4f);
        animator.SetTrigger("Attack");
        to.CurrentHp -= (int)(stats.baseDamage * AttackMultiplier);
        yield return new WaitForSeconds(0.2f);
        yield return new WaitForSeconds(0.3f);
        yield return transform.DOMove(_startPosition, 1f).WaitForCompletion();
    }

    public IEnumerator DodgeCoroutine()
    {
        AddSkill(UnitSkill.Dodge);
        yield return dodgeSprite.DOFade(dodgeSpriteTargetOpaqueness, 1f).WaitForCompletion();
        yield return new WaitForSeconds(0.3f);
        // CombatManager.Instance.onTurnStart.AddListener(WearOffDodge);
    }

    protected IEnumerator WearOffDodge()
    {
        RemoveSkill(UnitSkill.Dodge);
        // CombatManager.Instance.onTurnStart.RemoveListener(WearOffDodge);
        yield return dodgeSprite.DOFade(0f, 1f).WaitForCompletion();
    }

    public IEnumerator BlockCoroutine()
    {
        AddSkill(UnitSkill.Block);
        yield return blockSprite.DOFade(blockSpriteTargetOpaqueness, 1f).WaitForCompletion();
        yield return new WaitForSeconds(0.3f);
        // CombatManager.Instance.onTurnStart.AddListener(WearOffBlock);
    }

    protected IEnumerator WearOffBlock()
    {
        RemoveSkill(UnitSkill.Block);
        // CombatManager.Instance.onTurnStart.RemoveListener(WearOffBlock);
        yield return blockSprite.DOFade(0f, 1f).WaitForCompletion();
    }

    public virtual IEnumerator ExecuteTurn()
    {
        if (_activeSkills.HasFlag(UnitSkill.Block))
        {
            yield return WearOffBlock();
        }
        else if (_activeSkills.HasFlag(UnitSkill.Dodge))
        {
            yield return WearOffDodge();
        }
    }
}