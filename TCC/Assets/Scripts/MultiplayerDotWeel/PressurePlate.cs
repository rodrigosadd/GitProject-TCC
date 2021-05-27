using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject gameObject_Active;
    float time;
    bool OnTime;

    void Update()
    {
        if (OnTime)
        {
            TimeCount();
        }
    }
    public void ActiveObject()
    {
        gameObject_Active.SetActive(true);
        OnTime = true;
    }

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
            ActiveObject();
        }
    }
}
