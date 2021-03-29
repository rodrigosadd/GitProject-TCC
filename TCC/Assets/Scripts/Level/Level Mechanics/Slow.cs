using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
     public float slowValue;
     private float _currentMaxSpeed;

     void OnTriggerStay(Collider collider)
     {
          if (collider.transform.tag == "Player" &&
              !PlayerController.instance.levelMechanics.slowing &&
              !PlayerController.instance.push.pushingObj &&
              !PlayerController.instance.levelMechanics.slowing)
          {
               _currentMaxSpeed = PlayerController.instance.movement.fixedMaxSpeed;
               PlayerController.instance.movement.maxSpeed = slowValue;
               PlayerController.instance.push.slowReference = this;
               PlayerController.instance.levelMechanics.slowing = true;
          }
     }

     void OnTriggerExit(Collider collider)
     {
          if (PlayerController.instance.levelMechanics.slowing && collider.transform.tag == "Player" && PlayerController.instance.push.pushingObj == false)
          {
               PlayerController.instance.movement.maxSpeed = _currentMaxSpeed;
               PlayerController.instance.levelMechanics.slowing = false;
          }
     }
}
