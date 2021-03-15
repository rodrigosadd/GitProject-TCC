using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
     public float slowValue;
     private float _currentMaxSpeed;

     void OnTriggerStay(Collider collider)
     {
          if (!PlayerController.instance.movement.slowing && collider.transform.tag == "Player" && PlayerController.instance.push.pushingObj == false)
          {
               _currentMaxSpeed = PlayerController.instance.movement.fixedMaxSpeed;
               PlayerController.instance.movement.maxSpeed = slowValue;
               PlayerController.instance.push.slowReference = this;
               PlayerController.instance.movement.slowing = true;
          }
     }

     void OnTriggerExit(Collider collider)
     {
          if (PlayerController.instance.movement.slowing && collider.transform.tag == "Player" && PlayerController.instance.push.pushingObj == false)
          {
               PlayerController.instance.movement.maxSpeed = _currentMaxSpeed;
               PlayerController.instance.movement.slowing = false;
          }
     }
}
