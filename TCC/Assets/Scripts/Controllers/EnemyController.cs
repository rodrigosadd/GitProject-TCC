using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy
{
     public static EnemyController instance;

#if UNITY_EDITOR
     [Header("See Range variables")]
     public bool seeRangeFind;
#endif

     void Start()
     {
          instance = this;
     }

     void Update()
     {
          MoveToPatrolPoint();
          EnemyFollowPlayer();
          CheckStunning();
          EnemyAnimations();
     }

     #region Enemy Animations
     void EnemyAnimations()
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
