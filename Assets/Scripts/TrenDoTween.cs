using UnityEngine;
using DG.Tweening;

public class TrenDoTween : MonoBehaviour
{
    Animator TrenAnim;
    SpriteRenderer trenSprite;
    [SerializeField] Sprite sp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TrenAnim = GetComponent<Animator>();
        trenSprite = GetComponent<SpriteRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void changeSprite()
    {
        TrenAnim.SetBool("Soldan", true);
        if(TrenAnim.GetBool("Soldan"))
        {
            trenSprite.sprite = sp;
        }

        
    }
   
   
}
