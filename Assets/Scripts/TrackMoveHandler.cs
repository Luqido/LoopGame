using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TrackMoveHandler :  MonoBehaviour
{
    HamsterDoTween hamster;
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float speed = 2f;
    
    private List<Transform> _currentlyMovingTransforms = new();
    private Dictionary<Transform, int> _targetWaypoints = new();
    private Dictionary<Transform, Tween> _tweens = new();
    private void Start()
    {
        hamster = FindAnyObjectByType<HamsterDoTween>();
    }
    public void MoveAlongTrack(Transform t, Action<int> onWaypointReached)
    {
        _currentlyMovingTransforms.Add(t);
        _targetWaypoints.Add(t, 0);

        StartCoroutine(EndlessLoopRoutine(t, onWaypointReached));
    }

    public void SetPause(bool value)
    {
        if (value)
        {
            foreach (var t in _currentlyMovingTransforms)
            {
                _tweens[t].Pause();
                hamster.Animasyon1(false);
                hamster.Animasyon2(false);

            }
        }
        else
        {
            foreach (var t in _currentlyMovingTransforms)
            {
                _tweens[t].Play();
                
            }
        }
    }

    private IEnumerator EndlessLoopRoutine(Transform t, Action<int> onWaypointReached)
    {
        while (t != null)
        {
            yield return MoveToNextTarget(t, onWaypointReached);
        }
    }
    
    private IEnumerator MoveToNextTarget(Transform t, Action<int> onWaypointReached)
    {
        var targetWaypoint = _targetWaypoints[t];
        yield return (_tweens[t] = t.DOMove(waypoints[targetWaypoint].position, speed).SetSpeedBased().SetEase(Ease.Linear)).WaitForCompletion();
        onWaypointReached?.Invoke(targetWaypoint);
        _targetWaypoints[t] = (targetWaypoint + 1) % waypoints.Length;
        
    }
    
}
