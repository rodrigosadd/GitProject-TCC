using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
     private bool alive = true;

     void Update()
     {
          CheckDeath();
     }

     void OnTriggerEnter(Collider collider)
     {
          if (collider.transform.tag == "Player")
          {
               if (alive)
               {
                    RaycastHit _hitInfo;

                    if (Physics.Raycast(PlayerController.instance.transform.position, Vector3.down, out _hitInfo, 10f))
                    {
                         PlayerController.instance.hit.hitCount = PlayerController.instance.hit.maxHitCount;
                         PlayerController.instance.CheckDeath();
                         alive = false;
                    }
               }
          }
     }

     void CheckDeath()
     {
          if (!alive)
          {
               PlayerController.instance.CountdownAfterDeath();
               alive = true;
          }
     }
}
