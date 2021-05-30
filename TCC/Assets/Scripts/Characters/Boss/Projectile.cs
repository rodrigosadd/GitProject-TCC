using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{   
    [HideInInspector]
    public Rigidbody rbody;
    public float delayDeactivateObject;
    public bool checkCollisionInGround;
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
        if(other.tag == "Player" && !PlayerController.instance.death.isInvincible)
        {
            _countdown = 0;   
            gameObject.SetActive(false);
            BossController.instance.StopCoroutine("DelayDeactivateBoss");
            BossController.instance.StartCoroutine("DelayDeactivateBoss");
        }
        else if(other.tag == "Ground" && checkCollisionInGround)
        {
            _countdown = 0;  
            gameObject.SetActive(false);  
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
