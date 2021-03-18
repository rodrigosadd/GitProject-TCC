using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
     public BoxCollider Collider;

#if UNITY_EDITOR
     public bool seeDeathPart = false;
#endif

     void OnTriggerEnter(Collider collider)
     {
          if (collider.transform.tag == "Player")
          {
               PlayerController.instance.hit.hitCount = PlayerController.instance.hit.maxHitCount;
               PlayerController.instance.CheckDeath();
          }
     }

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          if(seeDeathPart)
          {
               Gizmos.color = Color.red;
               Gizmos.DrawCube(transform.position + Collider.center, Collider.size);
          }
     }
#endif
}
