using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : Enemy
{
     public static MeleeEnemy instance;

     private float _distanceBetween = 0f;

#if UNITY_EDITOR
     [Header("See Range variables")]
     public bool seeRangeFind;
#endif

     void Start()
     {
          instance = this;
          patrol.waitTime = patrol.startWaitTime;
     }

     void Update()
     {
          MoveToPatrolPoint();
          CheckStunning();
          EnemyFollowPlayer();
          EnemyAnimations();
     }

     public void EnemyFollowPlayer()
     {
          _distanceBetween = Vector3.Distance(PlayerController.instance.transform.position, transform.position);

          if (_distanceBetween <= followPlayer.rangeFind)
          {
               if (movement.stateEnemy != EnemyState.FOLLOWING_PLAYER && movement.stateEnemy != EnemyState.STUNNED)
               {
                    movement.stateEnemy = EnemyState.FOLLOWING_PLAYER;
               }
               movement.enemyAgent.stoppingDistance = movement.maxPlayerDistante;
               movement.enemyAgent.destination = PlayerController.instance.transform.position;
          }
          else
          {
               movement.enemyAgent.stoppingDistance = 0f;
          }
          if (_distanceBetween <= movement.enemyAgent.stoppingDistance)
          {
               FaceTarget();
               Attack();
          }
     }

     public void Attack()
     {
          if (!PlayerController.instance.death.dead && movement.stateEnemy != EnemyState.ATTACKING__PLAYER && movement.stateEnemy != EnemyState.STUNNED)
          {
               movement.stateEnemy = EnemyState.ATTACKING__PLAYER;
          }
          if (PlayerController.instance.death.dead && movement.stateEnemy != EnemyState.IDLE)
          {
               movement.stateEnemy = EnemyState.IDLE;
          }
     }

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          if (seeRangeFind)
          {
               Gizmos.color = Color.red;
               Gizmos.DrawWireSphere(transform.position, followPlayer.rangeFind);
          }
     }
#endif
}
