using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBoxes : MonoBehaviour
{
     public float timeToRespawnBox;
     private GameObject _currentBox;
     private Rigidbody _rigidbodyCurrentBox;
     private bool _deactiveBox;
     private float _countdown;

     void Update()
     {
          ActiveBox();
     }

     void OnTriggerEnter(Collider collider)
     {
          if ((collider.tag == "Heavy" ||
              collider.tag == "Light") &&
              !_deactiveBox)
          {
               _deactiveBox = true;
               _currentBox = collider.gameObject;
               collider.transform.position = collider.GetComponent<WeightBox>().targetToRespawn;
               _rigidbodyCurrentBox = collider.GetComponent<Rigidbody>();
               _rigidbodyCurrentBox.velocity = Vector3.zero;
               collider.gameObject.SetActive(false);
          }
     }

     void ActiveBox()
     {
          if (_deactiveBox)
          {
               if (_countdown < 1)
               {
                    _countdown += Time.deltaTime / timeToRespawnBox;
               }
               else
               {
                    _countdown = 0;
                    _deactiveBox = false;
                    _rigidbodyCurrentBox.velocity = Vector3.zero;
                    _rigidbodyCurrentBox.isKinematic = false;
                    _currentBox.gameObject.SetActive(true);
               }
          }
     }
}
