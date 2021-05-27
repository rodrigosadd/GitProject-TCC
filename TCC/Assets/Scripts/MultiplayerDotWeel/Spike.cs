using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    public float timeCountdown;
    float time;
    public GameObject off, on;

    void Update()
    {
        TimeCountdown();
        Spikae();
    }

    void TimeCountdown()
    {
        time = time + 1 * Time.deltaTime;
    }

    void Spikae()
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
