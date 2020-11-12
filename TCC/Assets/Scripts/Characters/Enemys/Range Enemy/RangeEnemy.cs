using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeEnemy : Enemy
{
     public static RangeEnemy instance;


     [Header("Attack variables")]
     public Attack attack;
     private float _distanceBetween = 0f;

     [System.Serializable]
     public class Attack
     {
          public GameObject spellObj;
          public Transform targetSpell;
          public float timeBtwAttack;
          public float startTimeBtwAttack;
     }

#if UNITY_EDITOR
     [Header("See Range variables")]
     public bool seeRangeFind;
#endif

     void Start()
     {
          instance = this;
          patrol.waitTime = patrol.startWaitTime;
          attack.timeBtwAttack = attack.startTimeBtwAttack;
     }

     void Update()
     {
          MoveToPatrolPoint();
          CheckStunning();
          //EnemyAnimations();
          EnemyFollowPlayer();
     }

     public void EnemyFollowPlayer()
     {
          _distanceBetween = Vector3.Distance(PlayerController.instance.transform.position, transform.position);

          if (_distanceBetween <= followPlayer.rangeFind)
          {
               if (movement.stateEnemy != EnemyState.FOLLOWING_PLAYER)
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
               AttackRange();
          }
     }

     public void AttackRange()
     {
          if (!PlayerController.instance.death.dead && movement.stateEnemy != EnemyState.ATTACKING__PLAYER)
          {
               movement.stateEnemy = EnemyState.ATTACKING__PLAYER;
               if (attack.timeBtwAttack <= 0f)
               {
                    Instantiate(attack.spellObj, attack.targetSpell);
                    attack.timeBtwAttack = attack.startTimeBtwAttack;
               }
               else
               {
                    attack.timeBtwAttack -= Time.deltaTime;
               }
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
