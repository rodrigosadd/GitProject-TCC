using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy
{
    public bool seeRangeFind;

    void Start()
    {

    }

    void Update()
    {
        MoveToPatrolPoint();
        EnemyFollowPlayer();
    }

    void OnDrawGizmos()
    {
        if (seeRangeFind)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangeFind);
        }
    }
}
