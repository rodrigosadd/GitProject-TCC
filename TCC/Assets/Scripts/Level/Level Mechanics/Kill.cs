using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
     void OnTriggerEnter(Collider collider)
     {
          if (collider.transform.tag == "Player" && !PlayerController.instance.death.isInvincible)
          {
               PlayerController.instance.hit.hitCount = PlayerController.instance.hit.maxHitCount;
               PlayerController.instance.CheckDeath();
          }
     }
}
