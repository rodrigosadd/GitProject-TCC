using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
     public Transform targetAttack;
     public float maxDistanceAttack;
     public LayerMask layerPlayer;
     public bool seeAttackRange;

     public void AttackDetection()
     {
          Collider[] _hitPlayer = Physics.OverlapSphere(targetAttack.position, maxDistanceAttack, layerPlayer);

          foreach (Collider _hit in _hitPlayer)
          {
               if (_hit.transform.tag == "Player")
               {
                    PlayerController.instance.TakeDamage();
               }
          }
     }

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          if (seeAttackRange)
          {
               Gizmos.color = Color.magenta;
               Gizmos.DrawWireSphere(targetAttack.position, maxDistanceAttack);
          }
     }
#endif
}
