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
        float durat = Random.Range(1, 4);
        cube.DOScale(new Vector3(dist,0.5f,dist), durat).SetLoops(99, LoopType.Yoyo);
    }
}
