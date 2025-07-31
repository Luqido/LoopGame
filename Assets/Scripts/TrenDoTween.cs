using UnityEngine;
using DG.Tweening;

public class TrenDoTween : MonoBehaviour
{
    private Sequence yol;
    [SerializeField] float sure = 2f;
    [SerializeField] AnimationCurve ad;
    Animator TrenAnim;
    SpriteRenderer trenSprite;
    [SerializeField] Sprite sp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        DOTween.SetTweensCapacity(500,500);
    }
    void Start()
    {
        TrenAnim = GetComponent<Animator>();
        trenSprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        SiraliHaraket();
    }
    void SiraliHaraket()
    {
        
        if (yol != null && yol.IsActive()) return; 

        yol = DOTween.Sequence();
        Vector3 hedef1 = new Vector3(-6.55f, 0.2f, 0);
        Vector3 tersScale = new Vector3(-1, 1, 1);
        Vector3 tersScale2 = new Vector3(1, 1, 1);
        Vector3 hedef2 = new Vector3(0.5f, -3.5f, 0);
        Vector3 hedef3 = new Vector3(7f, -0.2f, 0);
        Vector3 hedef4 = new Vector3(0.22f, 3.33f, 0);
        yol.AppendCallback(() => transform.localScale = tersScale2);
        yol.Append(transform.DOMove(hedef1, sure).SetSpeedBased().SetEase(Ease.OutSine));
        yol.AppendCallback(() => TrenAnim.SetTrigger("Trigger1"));
        yol.Append(transform.DOMove(hedef2, sure).SetSpeedBased().SetEase(Ease.Linear));
        yol.AppendCallback(() => TrenAnim.SetTrigger("Trigger2"));
        yol.Append(transform.DOMove(hedef3, sure).SetEase(Ease.Linear));
        yol.AppendCallback(() => TrenAnim.SetTrigger("Trigger3"));
        yol.Append(transform.DOMove(hedef4, sure).SetEase(Ease.Linear));
        yol.AppendCallback(() => TrenAnim.SetTrigger("Trigger4"));
        yol.AppendCallback(() => TrenAnim.SetTrigger("Trigger5"));








        yol.SetLoops(-1);

        
    }


}
