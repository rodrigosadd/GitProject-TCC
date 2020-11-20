using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
     public Transform spot;

     void OnTriggerEnter(Collider collider)
     {
          if (PlayerController.instance.death.currentPoint != spot)
          {
               if (collider.tag == "Player")
               {
                    PlayerController.instance.death.currentPoint = spot;
                    Debug.Log("Checkpoint");
               }
          }
     }
}
