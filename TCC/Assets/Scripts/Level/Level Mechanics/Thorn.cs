using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Thorn : MonoBehaviour
{
    public Collider myCollider;
    public float endPosition;
    public float intervalTimeToUp;
    public float intervalTimeToDown;
    public float durationTimeToUp;
    public float durationTimeToDown;
    public float durationShake;
    public float strengthShake;
    private float startPositionY;

    void Start()
    {
        startPositionY = transform.position.y;
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(intervalTimeToUp)
                .Append(transform.DOShakePosition(durationShake, strengthShake).OnComplete(() => myCollider.enabled = true))
                .Append(transform.DOMoveY(endPosition, durationTimeToUp))
                .AppendInterval(intervalTimeToDown)
                .Append(transform.DOMoveY(startPositionY, durationTimeToDown).OnComplete(() => myCollider.enabled = false))
                .SetLoops(-1);
    }
}
