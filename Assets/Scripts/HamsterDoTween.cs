using UnityEngine;
using DG.Tweening;

public class HamsterDoTween : MonoBehaviour
{
    [SerializeField] float sure = 2f;
    Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        SiraliHaraket();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SiraliHaraket()
    {
        Sequence yol = DOTween.Sequence();
        Vector3 hedef1 = new Vector3(-6.55f, 0.2f, 0);
        Vector3 tersScale = new Vector3(-1, 1, 1);
        Vector3 tersScale2 = new Vector3(1, 1, 1);
        Vector3 hedef2 = new Vector3(0.5f, -3.5f, 0);
        Vector3 hedef3 = new Vector3(7f, -0.2f, 0);
        Vector3 hedef4 = new Vector3(0.22f, 3.33f, 0);
        yol.AppendCallback(() => transform.localScale = tersScale2);
        yol.Append(transform.DOMove(hedef1, sure).SetEase(Ease.Linear));
        yol.Append(transform.DOScale(tersScale, 0).SetEase(Ease.Linear));
        yol.Append(transform.DOMove(hedef2, sure).SetEase(Ease.Linear));
        yol.AppendCallback (()=> anim.SetBool("Anim1", true));
        yol.Append(transform.DOMove(hedef3, sure).SetEase(Ease.Linear));
        yol.Append(transform.DOScale(tersScale2,0));
        yol.Append(transform.DOMove(hedef4, sure).SetEase(Ease.Linear));
        yol.AppendCallback(() => anim.SetBool("Anim1", false));
        


        yol.SetLoops(-1);
        
    }
}
