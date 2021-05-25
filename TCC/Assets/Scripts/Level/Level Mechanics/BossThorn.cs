using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class BossThorn : MonoBehaviour
{
    public Collider myCollider;
    public float endPosition;
    public float durantion;
    public float durationShake;
    public float strengthShake;
    public float intervalTimeToUp;
    public float timeToDeactivate;

    public UnityEvent CompleteUpMovement;

    void OnEnable()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOShakePosition(durationShake, strengthShake).OnComplete(() => myCollider.enabled = true))
                .Append(transform.DOMoveY(endPosition, durantion))
                .AppendInterval(timeToDeactivate).OnComplete(() => CompleteUpMovement?.Invoke());
    }
}
