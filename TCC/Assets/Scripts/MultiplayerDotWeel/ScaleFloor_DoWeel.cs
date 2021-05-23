using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class ScaleFloor_DoWeel : MonoBehaviour
{
    Transform cube;
    public float animDuration;
    public float dist;

    private void Start()
    {
        cube = this.gameObject.transform;
        cube.DOScale(new Vector3(dist,0.5f,dist), animDuration).SetLoops(99, LoopType.Yoyo);
    }
}
