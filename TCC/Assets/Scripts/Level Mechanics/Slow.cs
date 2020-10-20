using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
     public float slowValue;
     public bool slowed = false;

     private float maxSpeedAux;

     void OnTriggerStay(Collider collider)
     {
          if (!slowed && collider.transform.tag == "Player")
          {
               maxSpeedAux = PlayerController.instance.maxSpeed;
               PlayerController.instance.maxSpeed = slowValue;
               slowed = true;
          }
     }

     void OnTriggerExit(Collider collider)
     {
          if (slowed && collider.transform.tag == "Player")
          {
               PlayerController.instance.maxSpeed = maxSpeedAux;
               slowed = false;
          }
     }
}
