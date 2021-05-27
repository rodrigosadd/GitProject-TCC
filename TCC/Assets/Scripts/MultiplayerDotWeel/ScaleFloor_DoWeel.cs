using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class ScaleFloor_DoWeel : MonoBehaviourPun
{
    Transform cube;
    public float animDuration;
    public float dist;

    private void Start()
    {
        photonView.RPC("Mecanica",RpcTarget.AllBuffered);
    }

    public void Mecanica()
    {
        cube = this.gameObject.transform;
        float durat = Random.Range(1, 4);
        cube.DOScale(new Vector3(dist, 0.5f, dist), durat).SetLoops(99, LoopType.Yoyo);
    }
}
