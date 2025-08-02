using System.Collections;
using UnityEngine;
using DG.Tweening;

public class TrenDoTween : MonoBehaviour
{
    Animator TrenAnim;
    [SerializeField] private TrackMoveHandler trackMoveHandler;
    
    void Awake()
    {
        TrenAnim = GetComponent<Animator>();
    }

    void Start()
    {

        trackMoveHandler.MoveAlongTrack(transform, OnWaypointReached);
    }

    private void OnWaypointReached(int index)
    {
        switch (index)
        {
            case 0:
                TrenAnim.SetTrigger("Trigger1");
                break;
            case 1:
                TrenAnim.SetTrigger("Trigger2");
                break;
            case 2:
                TrenAnim.SetTrigger("Trigger3");
                break;
            case 3:
                TrenAnim.SetTrigger("Trigger4");
                TrenAnim.SetTrigger("Trigger5");
                transform.localScale = Vector3.one;
                break;
        }
    }
}
