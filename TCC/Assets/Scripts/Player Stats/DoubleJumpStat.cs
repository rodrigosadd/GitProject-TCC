﻿using FMODUnity;
using UnityEngine;

public class DoubleJumpStat : Stats
{
    private bool _itemTaken;
    void Update()
    {
        RotateObject();
        CheckPickedUp();
        ReturnPlayerTarget();
    }

    public void CheckPickedUp()
    {
        float _distanceBetween = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

        if(_distanceBetween < maxDistancePickedUp && 
            Input.GetButtonDown("Interact") &&
            PlayerController.instance.movement.canMove &&
            !PlayerAttackController.instance.attaking &&
            PlayerAttackController.instance.currentAttack == 0 &&
            PlayerController.instance.jump.currentJump <= 0 &&
            !_itemTaken)
        {
            RuntimeManager.PlayOneShot(collectSound, transform.position);
            GameManager.instance.playerStatsData.maxJump = 2;
            GameManager.instance.playerStatsData.ApplySettings();
            GameManager.instance.savePlayerStats.Save();
            PickingUpItem();
        }
    } 

// #if UNITY_EDITOR
//      void OnDrawGizmos()
//      {
//           Gizmos.color = Color.yellow;
//           Gizmos.DrawWireSphere(transform.position, maxDistancePickedUp);
//      }
// #endif
}
