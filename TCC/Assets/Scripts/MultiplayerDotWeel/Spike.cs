using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Spike : MonoBehaviourPun
{
    public float timeCountdown;
    float time;
    public GameObject off, on;

    void Update()
    {
        TimeCountdown();
        SpikePUN();
    }

    void TimeCountdown()
    {
        time = time + 1 * Time.deltaTime;
    }

    void SpikePUN()
    {
        if (time > timeCountdown)
        {
            off.SetActive(false);
            on.SetActive(true);
            if (time > timeCountdown + 2)
            {
                off.SetActive(true);
                on.SetActive(false);
                time = 0;
            }
        }
    }
}
