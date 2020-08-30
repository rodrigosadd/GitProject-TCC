using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public EnemyState stateEnemy;

    [Header("Patrol variables")]
    public NavMeshAgent enemyAgent;
    public Transform[] patrolPoints;
    public int patrolSpot = 0;
    public float waitTime = 2f;
    public float startWaitTime = 2f;

    [Header("Follow Player variables")]
    public float rangeFind = 2f;

    private float _distanceBetWeen = 0f;
    private Vector3 _directionFace;

    public void MoveToPatrolPoint()
    {
        enemyAgent.destination = patrolPoints[patrolSpot].position;

        if (Vector3.Distance(transform.position, patrolPoints[patrolSpot].position) < 1.8f)
        {
            if (waitTime <= 0)
            {
                stateEnemy = EnemyState.PATROLLING;
                patrolSpot++;
                waitTime = startWaitTime;
            }
            else
            {
                stateEnemy = EnemyState.IDLE;
                waitTime -= Time.deltaTime;
            }
            if (patrolSpot >= patrolPoints.Length)
            {
                patrolSpot = 0;
            }
        }
    }

    public void EnemyFollowPlayer()
    {
        _distanceBetWeen = Vector3.Distance(PlayerController.instance.transform.position, transform.position);

        if (_distanceBetWeen <= rangeFind)
        {
            stateEnemy = EnemyState.FOLLOWING_PLAYER;
            enemyAgent.destination = PlayerController.instance.transform.position;
        }
        if (_distanceBetWeen <= enemyAgent.stoppingDistance)
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
