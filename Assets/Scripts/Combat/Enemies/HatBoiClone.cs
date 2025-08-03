using System.Collections;

public class HatBoiClone : Unit
{
    public override IEnumerator ExecuteTurn()
    {
        yield break;
    }
    
    private void OnDestroy()
    {
        HatBoiEnemy.CloneActive = false;
    }
}