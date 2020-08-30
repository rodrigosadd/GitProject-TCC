using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy
{
    public bool seeRangeFind;

    public static EnemyController instance;

    void Start()
    {
        instance = this;
    }

    void Update()
    {
        MoveToPatrolPoint();
        EnemyFollowPlayer();
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
