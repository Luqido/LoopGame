using UnityEngine;
using DG.Tweening;
using System.Collections;

public class HamsterDoTween : MonoBehaviour
{
    bool firstRound = true;
    private Sequence yol;
    [SerializeField] float sure = 2;
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    IEnumerator  Start()
    {
        anim = GetComponent<Animator>();
        //SiraliHaraket();
        while(true)
        {
            yield return SiraliHaraket();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //void SiraliHaraket()
    //{
    //    if (yol != null && yol.IsActive()) return;
    //    yol = DOTween.Sequence();
    //    Vector3 hedef1 = new Vector3(-6.55f, 0.2f, 0);
    //    Vector3 tersScale = new Vector3(-1, 1, 1);
    //    Vector3 tersScale2 = new Vector3(1, 1, 1);
    //    Vector3 hedef2 = new Vector3(0.5f, -3.5f, 0);
    //    Vector3 hedef3 = new Vector3(7f, -0.2f, 0);
    //    Vector3 hedef4 = new Vector3(0.22f, 3.33f, 0);
    //    yol.AppendCallback(() => transform.localScale = tersScale2);
    //    yol.Append(transform.DOMove(hedef1, sure).SetSpeedBased().SetEase(Ease.OutSine));
    //    yol.Append(transform.DOScale(tersScale, 0).SetEase(Ease.Linear));
    //    yol.Append(transform.DOMove(hedef2, sure).SetSpeedBased().SetEase(Ease.OutSine));
    //    yol.AppendCallback (()=> anim.SetBool("Anim1", true));
    //    yol.Append(transform.DOMove(hedef3, sure).SetSpeedBased().SetEase(Ease.OutSine));
    //    yol.Append(transform.DOScale(tersScale2,0));
    //    yol.Append(transform.DOMove(hedef4, sure).SetSpeedBased().SetEase(Ease.OutSine));
    //    yol.AppendCallback(() => anim.SetBool("Anim1", false));



    //    yol.SetLoops(-1);

    //}
    IEnumerator SiraliHaraket()
    {
        Vector3 hedef1 = new Vector3(-6.55f, 0.2f, 0);
        Vector3 tersScale = new Vector3(-1, 1, 1);
        Vector3 tersScale2 = new Vector3(1, 1, 1);
        Vector3 hedef2 = new Vector3(0.5f, -3.5f, 0);
        Vector3 hedef3 = new Vector3(7f, -0.2f, 0);
        Vector3 hedef4 = new Vector3(0.22f, 3.33f, 0);
        //var closestPointOnLine = ClosestPointOnLine(transform.position, hedef4, hedef1);
        var timeItTakes = (hedef4 - transform.position).magnitude / sure;

        var firstTween = transform.DOMove(hedef1, sure).SetSpeedBased().SetEase(Ease.OutSine).Pause();

        if (firstRound)
        {
            firstRound = false;
            firstTween.Goto(timeItTakes);

        }
        firstTween.Play();

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
