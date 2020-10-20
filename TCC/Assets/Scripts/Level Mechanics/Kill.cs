using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
     public Transform currentPoint;

     void OnTriggerEnter(Collider collider)
     {
          if (collider.transform.tag == "Player")
          {
               PlayerController.instance.transform.position = currentPoint.position;
          }
     }
}
