using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
     public EnemyState stateEnemy;

     [Header("Patrol variables")]
     public Patrol patrol;

     [System.Serializable]
     public class Patrol
     {
          public NavMeshAgent enemyAgent;
          public Transform[] patrolPoints;
          public int patrolSpot;
          public float waitTime;
          public float startWaitTime;
     }

     [Header("Follow Player variables")]
     public FollowPlayer followPlayer;
     private Vector3 _directionFace;
     private float _distanceBetween = 0f;

     [System.Serializable]
     public class FollowPlayer
     {
          public float rangeFind;
     }

     public void MoveToPatrolPoint()
     {
          patrol.enemyAgent.destination = patrol.patrolPoints[patrol.patrolSpot].position;

          if (Vector3.Distance(transform.position, patrol.patrolPoints[patrol.patrolSpot].position) < patrol.enemyAgent.stoppingDistance + 1)
          {
               if (patrol.waitTime <= 0)
               {
                    stateEnemy = EnemyState.PATROLLING;
                    patrol.patrolSpot++;
                    patrol.waitTime = patrol.startWaitTime;
               }
               else
               {
                    stateEnemy = EnemyState.IDLE;
                    patrol.waitTime -= Time.deltaTime;
               }
               if (patrol.patrolSpot >= patrol.patrolPoints.Length)
               {
                    patrol.patrolSpot = 0;
               }
          }
     }

     public void EnemyFollowPlayer()
     {
          _distanceBetween = Vector3.Distance(PlayerController.instance.transform.position, transform.position);

          if (_distanceBetween <= followPlayer.rangeFind)
          {
               if (stateEnemy != EnemyState.FOLLOWING_PLAYER)
               {
                    stateEnemy = EnemyState.FOLLOWING_PLAYER;
               }
               patrol.enemyAgent.destination = PlayerController.instance.transform.position;
          }
          if (_distanceBetween <= patrol.enemyAgent.stoppingDistance)
          {
               FaceTarget();
               if (PlayerController.instance.stateCharacter == CharacterState.DISABLED)
               {
                    stateEnemy = EnemyState.IDLE;
               }
               else
               {
                    stateEnemy = EnemyState.ATTACKING__PLAYER;
               }
          }
     }

     public void FaceTarget()
     {
          _directionFace = (PlayerController.instance.transform.position - transform.position).normalized;

          Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_directionFace.x, 0f, _directionFace.z));
          transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 5f);
     }
}
