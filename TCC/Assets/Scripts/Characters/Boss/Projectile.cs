using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{   
    [HideInInspector]
    public Rigidbody rbody;
    public float delayDeactivateObject;
    private float _countdown;

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();          
    }

    void Update()
    {
        CountdownDeactivateObject();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameObject.SetActive(false);  
            _countdown = 0;   
        }
    }

    void CountdownDeactivateObject()
    {
        if(_countdown < 1)
        {
            _countdown += Time.deltaTime / delayDeactivateObject;
        }
        else
        {
            _countdown = 0;
            gameObject.SetActive(false);
        }
    }
}
