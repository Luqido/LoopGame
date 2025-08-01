using System;
using UnityEngine;
using DG.Tweening;
using System.Collections;
using DG.Tweening.Core.Easing;

public class HamsterDoTween : MonoBehaviour
{
    Animator anim;
    [SerializeField] private TrackMoveHandler trackMoveHandler;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Start()
    {
        trackMoveHandler.MoveAlongTrack(transform, OnWaypointReached);
    }
    
    private void OnWaypointReached(int index)
    {
        Vector3 tersScale = new Vector3(-1, 1, 1);
        Vector3 tersScale2 = new Vector3(1, 1, 1);

        if (index == 0)
        {
            transform.localScale = tersScale;
        }

        if (index == 1)
        {
            anim.SetBool("Anim1", true);
        }

        if (index == 2)
        {
            transform.localScale = tersScale2;
        }

        if (index == 3)
        {
            anim.SetBool("Anim1", false);
            transform.localScale = tersScale2;
        }
    }
}
