using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class Obstaculo_Dotweel_Moviment : MonoBehaviourPun
{
    public string type;
    Transform cube;
    public float animDuration;
    public float dist;
    public Ease effects;
    public Vector3 direc;
    public Vector3[] path;

    private void Start()
    {
        photonView.RPC("ElevatorMethod", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void ElevatorMethod()
    {
        cube = this.gameObject.transform;
        if (type == "elevator")
        {
            cube.DOMoveY(dist, animDuration).SetLoops(99, LoopType.Yoyo);
        }
    }


}
