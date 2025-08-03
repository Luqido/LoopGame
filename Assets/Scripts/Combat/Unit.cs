using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

[Flags]
public enum UnitSkill
{
    Block = 1 << 0,
    Dodge = 1 << 1,
}
public abstract class Unit : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Animator animator;
    [SerializeField] private Transform damageTakePosition;
    public Transform healthBarPosition;
    public Transform talkBubblePosition;
    [SerializeField] public UnitStats stats;
    [SerializeField] private SpriteRenderer blockSprite;
    [SerializeField] private SpriteRenderer dodgeSprite;
    [SerializeField] private float blockSpriteTargetOpaqueness = 1f;
    [SerializeField] private float dodgeSpriteTargetOpaqueness = 1f;
    [SerializeField] private Vector3 dodgeMovement;
    
    public UnitSkill ActiveSkills { get; set; }
    
    public event UnityAction<int, int, int> HealthChanged;

    public float AttackMultiplier { get; set; } = 1f;
    
    private Vector3 _startPosition;
    private int _currentHp = 1;
    //todo
    public int startingHealth { get; protected set; }
    public int currentDamage { get; protected set; }

    public virtual float BlockAbsorbPercentage => stats.blockPercentage;
    public virtual float DodgeHitChance => stats.dodgeChance;
    public int CurrentHp
    {
        get => _currentHp;
        set
        {
            if (_currentHp <= 0) return;
            
            if (value < _currentHp)
            {
                if (ActiveSkills.HasFlag(UnitSkill.Block))
                {
                    var oldValue = value;
                    var diff = _currentHp - value;
                    diff = (int)(diff * (BlockAbsorbPercentage));
                    value += diff; 
                    Debug.Log($"Blocked! Old: {oldValue}, new: {value}");
                    
                }
                else if (ActiveSkills.HasFlag(UnitSkill.Dodge))
                {
                    if (DodgeHitChance > Random.value)
                    {
                        value = _currentHp;
                        transform.DOMove(dodgeMovement, 0.2f).SetRelative(true).SetLoops(2, LoopType.Yoyo);
                    }
                }

                if (value < _currentHp)
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
                    CombatManager.Instance.GameLost();
                }
                
                transform.DOMoveY(0.4f, 1f).SetDelay(0.6f).SetRelative(true);
                GetComponent<SpriteRenderer>().DOFade(0.1f, 1f).SetDelay(0.6f);
                blockSprite.DOFade(0f, 1f).SetDelay(0.6f);
                dodgeSprite.DOFade(0f, 1f).SetDelay(0.6f).OnComplete(() =>
                {
                    CombatManager.Instance.RepositionEnemies(true);
                });
                Destroy(gameObject, 1.61f);
            }
            
            HealthChanged?.Invoke(_currentHp, value, startingHealth);
            _currentHp = value;
        }
    }
    protected virtual void Awake()
    {
        // _startPosition = transform.position;
    }

    protected virtual void Start()
    {
        CurrentHp = startingHealth;
    }
    
    private void AddSkill(UnitSkill skill)
    {
        ActiveSkills |= skill;
    }
    
    private void RemoveSkill(UnitSkill skill)
    {
        ActiveSkills ^= skill;
    }

    public IEnumerator AttackCoroutine(Unit to)
    {
        _startPosition = transform.position;
        transform.DOMove(to.damageTakePosition.position, 0.6f).SetEase(Ease.InCubic);
        yield return new WaitForSeconds(0.4f);
        animator.SetTrigger("Attack");
        to.CurrentHp -= (int)(currentDamage * AttackMultiplier);
        SoundManager.instance.PlaySound(SoundManager.SoundNames.Punch1 + Random.Range(0, 2));
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
        if (ActiveSkills.HasFlag(UnitSkill.Block))
        {
            yield return WearOffBlock();
        }
        else if (ActiveSkills.HasFlag(UnitSkill.Dodge))
        {
            yield return WearOffDodge();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        CombatManager.Instance.ui.ShowTooltip(this, stats.shortDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        CombatManager.Instance.ui.HideTooltip(this);
    }
}