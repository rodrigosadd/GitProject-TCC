using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
     public float slowValue;
     public bool slowed = false;

     private float _currentMaxSpeed;

     void OnTriggerStay(Collider collider)
     {
          if (!slowed && collider.transform.tag == "Player" && PlayerController.instance.push.pushingObj == false)
          {
               _currentMaxSpeed = PlayerController.instance.movement.maxSpeed;
               PlayerController.instance.movement.maxSpeed = slowValue;
               PlayerController.instance.push.slowReference = this;
               slowed = true;
          }
     }

     void OnTriggerExit(Collider collider)
     {
          if (slowed && collider.transform.tag == "Player" && PlayerController.instance.push.pushingObj == false)
          {
               Debug.Log("Trigou");
               PlayerController.instance.movement.maxSpeed = _currentMaxSpeed;
               slowed = false;
          }
     }
}
