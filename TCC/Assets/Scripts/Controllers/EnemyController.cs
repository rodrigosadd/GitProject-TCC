using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy
{
     public static EnemyController instance;
     public bool seeRangeFind;

     void Start()
     {
          instance = this;
     }

     void Update()
     {
          MoveToPatrolPoint();
          EnemyFollowPlayer();
          EnemyAnimations();
     }

     void EnemyAnimations()
     {
          switch (stateEnemy)
          {
               case EnemyState.IDLE:
                    animatorEnemy.SetBool("Idle", true);
                    animatorEnemy.SetBool("Walking", false);
                    animatorEnemy.SetBool("Atacking", false);
                    animatorEnemy.SetBool("Spoted", false);
                    break;
               case EnemyState.PATROLLING:
                    animatorEnemy.SetBool("Idle", false);
                    animatorEnemy.SetBool("Walking", true);
                    animatorEnemy.SetBool("Atacking", false);
                    animatorEnemy.SetBool("Spoted", false);
                    break;
               case EnemyState.SPOTED:
                    animatorEnemy.SetBool("Idle", false);
                    animatorEnemy.SetBool("Walking", true);
                    animatorEnemy.SetBool("Atacking", false);
                    animatorEnemy.SetBool("Spoted", true);
                    break;
               case EnemyState.FOLLOWING_PLAYER:
                    animatorEnemy.SetBool("Idle", false);
                    animatorEnemy.SetBool("Walking", true);
                    animatorEnemy.SetBool("Atacking", false);
                    animatorEnemy.SetBool("Spoted", false);
                    break;
               case EnemyState.ATTACKING__PLAYER:
                    animatorEnemy.SetBool("Idle", false);
                    animatorEnemy.SetBool("Walking", false);
                    animatorEnemy.SetBool("Atacking", true);
                    animatorEnemy.SetBool("Spoted", false);
                    break;
               case EnemyState.STUNNED:
                    animatorEnemy.SetBool("Idle", true);
                    animatorEnemy.SetBool("Walking", false);
                    animatorEnemy.SetBool("Atacking", false);
                    animatorEnemy.SetBool("Spoted", false);
                    break;
          }
     }

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          if (seeRangeFind)
          {
               Gizmos.color = Color.red;
               Gizmos.DrawWireSphere(transform.position, rangeFind);
          }
     }
#endif
}
