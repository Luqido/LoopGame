using System.Collections;
using DG.Tweening;
using UnityEngine;

public class BasicEnemy : MonoBehaviour, ICombatPlayer
{
    [SerializeField] private Player player;
    
    public IEnumerator ExecuteTurn()
    {
        player.TakeDamage(100);
        Debug.Log("GWAAAK GWAAAK");
        yield break;
    }

    private Animator anim;
    private float sure;
    private Sequence yol;
    private bool firstRound = true;
    IEnumerator SiraliHaraket()
    {
        Vector3 hedef1 = new Vector3(-6.55f, 0.2f, 0);
        Vector3 tersScale = new Vector3(-1, 1, 1);
        Vector3 tersScale2 = new Vector3(1, 1, 1);
        Vector3 hedef2 = new Vector3(0.5f, -3.5f, 0);
        Vector3 hedef3 = new Vector3(7f, -0.2f, 0);
        Vector3 hedef4 = new Vector3(0.22f, 3.33f, 0);
        var closestPointOnLine = ClosestPointOnLine(transform.position, hedef4, hedef1);
        var timeItTakes = (hedef4 - closestPointOnLine).magnitude / sure;

        var firstTween = transform.DOMove(hedef1, sure).SetSpeedBased().SetEase(Ease.OutSine).Pause();

        if (firstRound)
        {
            firstRound = false;
            firstTween.Goto(timeItTakes, true);
        }

        yield return firstTween.WaitForCompletion();
        transform.localScale = tersScale2;
        yield return transform.DOMove(hedef1, sure).SetSpeedBased().SetEase(Ease.OutSine).WaitForCompletion();
        transform.localScale = tersScale;
        yield return transform.DOMove(hedef2, sure).SetSpeedBased().SetEase(Ease.OutSine).WaitForCompletion();
        anim.SetBool("Anim1", true);
        yield return transform.DOMove(hedef3, sure).SetSpeedBased().SetEase(Ease.OutSine).WaitForCompletion();
        transform.localScale = tersScale2;
        yield return transform.DOMove(hedef4, sure).SetSpeedBased().SetEase(Ease.OutSine).WaitForCompletion();
        anim.SetBool("Anim1", true);
    }
    Vector3 ClosestPointOnLine(Vector3 point, Vector3 a, Vector3 b)
    {
        Vector3 ab = b - a;
        Vector3 ap = point - a;
        float t = Vector3.Dot(ap, ab) / Vector3.Dot(ab, ab);
        return a + t * ab;
    }
}