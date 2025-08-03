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
        Animasyon1(true);

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
            Animasyon1(false);
            Animasyon2(true);
        }

        if (index == 2)
        {
            transform.localScale = tersScale2;
        }

        if (index == 3)
        {
            Animasyon2(false);
            Animasyon1(true);
            transform.localScale = tersScale2;
        }
    }
    public void Animasyon1(bool index)
    {
        anim.SetBool("Anim1", index);
    }
    public void Animasyon2(bool index)
    {
        anim.SetBool("Anim2", index);
    }
}
