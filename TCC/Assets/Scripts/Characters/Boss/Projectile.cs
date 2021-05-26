using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{   
    [HideInInspector]
    public Rigidbody rbody;
    public float delayDeactivateObject;
    private GameObject _shadow;
    public bool hasShadow;
    private float _countdown;
    private bool _canActivateShadow = true;

    void Awake()
    {
        rbody = GetComponent<Rigidbody>();          
    }
    
    void Update()
    {
        CountdownDeactivateObject();
        Shadow();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(hasShadow)
            {
                _canActivateShadow = true;
                _shadow.gameObject.SetActive(false);
                _shadow = null;
            }

            _countdown = 0;   
            gameObject.SetActive(false);
        }
        else if(other.tag == "Ground" && hasShadow)
        {
            _canActivateShadow = true;
            _shadow.gameObject.SetActive(false);
            _shadow = null;           
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
            if(hasShadow)
            {
                _canActivateShadow = true;
                _shadow.gameObject.SetActive(false);
                _shadow = null;
            }

            _countdown = 0;
            gameObject.SetActive(false);
        }
    }

    public void Shadow()
    {   
        if(_canActivateShadow && hasShadow)
        {
            RaycastHit _hitInfo;
            
            if(Physics.Raycast(transform.position, Vector3.down, out _hitInfo, 100f))
            {  
                if(_hitInfo.transform.tag == "Ground")
                {
                    _shadow = GameManager.instance.poolSystem.TryToGetShadow();
                    _shadow.transform.position = _hitInfo.point + new Vector3(0f, 0.05f, 0f);    
                    _canActivateShadow = false;        
                } 
            }
        }
    }
}
