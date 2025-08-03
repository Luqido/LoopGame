using System.Collections;
using DG.Tweening;
using UnityEngine;

public class HatBoiClone : Unit
{
    public HatBoiEnemy Enemy { get; set; }
    public override IEnumerator ExecuteTurn()
    {
        yield return base.ExecuteTurn();
        if (Enemy)
        {
            if (Enemy.ActiveSkills.HasFlag(UnitSkill.Dodge))
            {
                yield return DodgeCoroutine();
            }
            else if (Enemy.ActiveSkills.HasFlag(UnitSkill.Block))
            {
                yield return BlockCoroutine();
            }
        }
        
        if (HatBoiEnemy.CloneActive)
        {
            yield return CombatManager.Instance.ui.Say(this, "Mix and match baby!");
            var switchPlaces = Random.value > 0.5f;
            var myPos = transform.position;
            var enemyPos = Enemy.transform.position;

            transform.DOMove(enemyPos, 0.1f).SetLoops(switchPlaces ? 7 : 6, LoopType.Yoyo);
            Enemy.transform.DOMove(myPos, 0.1f).SetLoops(switchPlaces ? 7 : 6, LoopType.Yoyo);
            yield return new WaitForSeconds(1f);
        }
    }
    
    private void OnDestroy()
    {
        HatBoiEnemy.CloneActive = false;
    }
}