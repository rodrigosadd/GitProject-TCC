using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class Obstaculo_Dotweel_Rotate : MonoBehaviourPun
{
    public Transform[] walls;
    public float animDuration;
    public Vector3 rotate;
    public Ease Effect;

    void Start()
    {
        photonView.RPC("Rotat", RpcTarget.AllBuffered);
    }

    [PunRPC]
    public void Rotat()
    {
        for (int x = 0; x < walls.Length; x++)
        {
            rotate.y = Random.Range(-180,180);
            walls[x].DORotate(rotate, animDuration,RotateMode.LocalAxisAdd).SetLoops(99,LoopType.Yoyo);
        }
    }
}
