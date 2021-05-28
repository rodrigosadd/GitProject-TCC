using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PressurePlate : MonoBehaviourPun
{
    public GameObject gameObject_Active;
    float time;
    bool OnTime;

    void Update()
    {
        if (OnTime)
        {
            photonView.RPC("TimeCount", RpcTarget.AllBuffered);
        }
    }
    [PunRPC]
    public void ActiveObject()
    {
        gameObject_Active.SetActive(true);
        OnTime = true;
    }

    [PunRPC]
    public void TimeCount()
    {
        time = time + 1 * Time.deltaTime;
        if(time > 4)
        {
            OnTime = false;
            time = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            photonView.RPC("ActiveObject",RpcTarget.AllBuffered);
        }
    }
}
