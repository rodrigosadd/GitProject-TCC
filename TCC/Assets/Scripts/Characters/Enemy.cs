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
          public Transform[] patrolPoints;
          public int patrolSpot;
          public float waitTime;
          public float startWaitTime;
     }

     [Header("Movement variables")]
     public Movement movement;
     private float _countdownStunning;
     private float _currentMaxSpeed;

     [System.Serializable]
     public class Movement
     {
          public NavMeshAgent enemyAgent;
          public float stunnedTime;
          public bool stunned;
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
          movement.enemyAgent.destination = patrol.patrolPoints[patrol.patrolSpot].position;

          if (Vector3.Distance(transform.position, patrol.patrolPoints[patrol.patrolSpot].position) < movement.enemyAgent.stoppingDistance + 1)
          {
               if (patrol.waitTime <= 0)
               {
                    if (stateEnemy != EnemyState.PATROLLING)
                    {
                         stateEnemy = EnemyState.PATROLLING;
                    }
                    patrol.patrolSpot++;
                    patrol.waitTime = patrol.startWaitTime;
               }
               else
               {
                    if (stateEnemy != EnemyState.IDLE)
                    {
                         stateEnemy = EnemyState.IDLE;
                    }
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
               movement.enemyAgent.destination = PlayerController.instance.transform.position;
          }
          if (_distanceBetween <= movement.enemyAgent.stoppingDistance)
          {
               FaceTarget();
               if (stateEnemy != EnemyState.ATTACKING__PLAYER)
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

     public void CheckStunning()
     {
          if (stunCount >= 3)
          {
               if (!movement.stunned)
               {
                    _currentMaxSpeed = movement.enemyAgent.speed;
                    movement.enemyAgent.speed = 0f;
                    movement.stunned = true;
               }
          }

          if (movement.stunned)
          {
               if (_countdownStunning < 1)
               {
                    if (stateEnemy != EnemyState.STUNNED)
                    {
                         stateEnemy = EnemyState.STUNNED;
                    }
                    _countdownStunning += Time.deltaTime / movement.stunnedTime;
               }
               else
               {
                    _countdownStunning = 0;
                    movement.enemyAgent.speed = _currentMaxSpeed;
                    movement.stunned = false;
                    stunCount = 0;
               }
          }
     }
}
