using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Obstaculo_Dotweel_Moviment : MonoBehaviour
{
    public string type;
    Transform cube;
    public float animDuration;
    public float dist;
    public Ease effects;
    public Vector3 direc;
    //public PathType pathType;
    //public PathMode pathMode;
    public Vector3[] path;

    private void Start()
    {
            cube = this.gameObject.transform;
        if (type == "elevator")
        {
            cube.DOMoveY(dist, animDuration).SetLoops(99, LoopType.Yoyo);
        }

        if (type == "push")
        {
           cube.DOMove(direc, animDuration).SetLoops(99, LoopType.Yoyo);
        }
    }
}
