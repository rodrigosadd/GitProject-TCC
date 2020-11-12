using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
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
          public EnemyState stateEnemy;
          public float stunnedTime;
          public float maxPlayerDistante;
          public bool stunned;
     }

     [Header("Follow Player variables")]
     public FollowPlayer followPlayer;
     private Vector3 _directionFace;

     [System.Serializable]
     public class FollowPlayer
     {
          public float rangeFind;
     }

     public void MoveToPatrolPoint()
     {
          movement.enemyAgent.destination = patrol.patrolPoints[patrol.patrolSpot].position;
          float _distanceBetween = Vector3.Distance(transform.position, patrol.patrolPoints[patrol.patrolSpot].position);
          Debug.Log(_distanceBetween);

          if (_distanceBetween < 2f)
          {
               if (patrol.waitTime <= 0)
               {
                    if (movement.stateEnemy != EnemyState.PATROLLING)
                    {
                         movement.stateEnemy = EnemyState.PATROLLING;
                    }
                    patrol.patrolSpot++;
                    patrol.waitTime = patrol.startWaitTime;
               }
               else
               {
                    if (movement.stateEnemy != EnemyState.IDLE)
                    {
                         movement.stateEnemy = EnemyState.IDLE;
                    }
                    patrol.waitTime -= Time.deltaTime;
               }
               if (patrol.patrolSpot >= patrol.patrolPoints.Length)
               {
                    patrol.patrolSpot = 0;
               }
          }
     }

     public void FaceTarget()
     {
          if (!movement.stunned)
          {
               _directionFace = (PlayerController.instance.transform.position - transform.position).normalized;

               Quaternion _lookRotation = Quaternion.LookRotation(new Vector3(_directionFace.x, 0f, _directionFace.z));
               transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 5f);
          }
     }

     public void CheckStunning()
     {
          if (hit.hitCount >= hit.maxHitCount)
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
                    if (movement.stateEnemy != EnemyState.STUNNED)
                    {
                         movement.stateEnemy = EnemyState.STUNNED;
                    }
                    _countdownStunning += Time.deltaTime / movement.stunnedTime;
               }
               else
               {
                    if (movement.stateEnemy != EnemyState.PATROLLING)
                    {
                         movement.stateEnemy = EnemyState.PATROLLING;
                    }
                    _countdownStunning = 0;
                    movement.enemyAgent.speed = _currentMaxSpeed;
                    movement.stunned = false;
                    hit.hitCount = 0;
               }
          }
     }

     #region Enemy Animations
     public void EnemyAnimations()
     {
          switch (movement.stateEnemy)
          {
               case EnemyState.IDLE:
                    animator.SetBool("Idle", true);
                    animator.SetBool("Walking", false);
                    animator.SetBool("Atacking", false);
                    animator.SetBool("Spoted", false);
                    animator.SetBool("Stunned", false);
                    break;
               case EnemyState.PATROLLING:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Walking", true);
                    animator.SetBool("Atacking", false);
                    animator.SetBool("Spoted", false);
                    animator.SetBool("Stunned", false);
                    break;
               case EnemyState.SPOTED:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Walking", true);
                    animator.SetBool("Atacking", false);
                    animator.SetBool("Spoted", true);
                    animator.SetBool("Stunned", false);
                    break;
               case EnemyState.FOLLOWING_PLAYER:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Walking", true);
                    animator.SetBool("Atacking", false);
                    animator.SetBool("Spoted", false);
                    animator.SetBool("Stunned", false);
                    break;
               case EnemyState.ATTACKING__PLAYER:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Walking", false);
                    animator.SetBool("Atacking", true);
                    animator.SetBool("Spoted", false);
                    animator.SetBool("Stunned", false);
                    break;
               case EnemyState.STUNNED:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Walking", false);
                    animator.SetBool("Atacking", false);
                    animator.SetBool("Spoted", false);
                    animator.SetBool("Stunned", true);
                    break;
          }
     }
     #endregion
}
