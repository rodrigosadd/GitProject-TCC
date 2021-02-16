using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
     public static PlayerAnimationController instance;
     public float timeToJump;
     public bool afterFalling;
     public bool balance;
     private bool _canJumpAfterFalling;
     private float _countdownAfterFalling;

     void Start()
     {
          instance = this;
     }

     void Update()
     {
          SetIsGrounded();
          SetIdle();
          SetRunning();
          SetSingleJump();
          SetSingleJumpRunning();
          SetDoubleJump();
          SetPush();
          SetPushIdle();
          SetFallingIdle();
          SetFallingGround();
          SetFallingRunning();
          SetDead();
          CanJumpAfterFalling();
     }

     public void SetFirstAttack()
     {
          PlayerController.instance.animator.SetBool("First Attack", true);
          PlayerController.instance.animator.SetBool("Idle", false);
          PlayerController.instance.animator.SetBool("Running", false);
          PlayerController.instance.animator.SetBool("Single Jump", false);
          PlayerController.instance.animator.SetBool("Single Jump Running", false);
          PlayerController.instance.animator.SetBool("Double Jump", false);
          PlayerController.instance.animator.SetBool("Pushing", false);
          PlayerController.instance.animator.SetBool("Pushing Idle", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
     }

     public void ResetFirstAttack()
     {
          PlayerController.instance.animator.SetBool("First Attack", false);
          PlayerController.instance.animator.SetBool("Idle", false);
          PlayerController.instance.animator.SetBool("Running", false);
          PlayerController.instance.animator.SetBool("Single Jump", false);
          PlayerController.instance.animator.SetBool("Single Jump Running", false);
          PlayerController.instance.animator.SetBool("Double Jump", false);
          PlayerController.instance.animator.SetBool("Pushing", false);
          PlayerController.instance.animator.SetBool("Pushing Idle", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
     }

     public void SetSecondAttack()
     {
          PlayerController.instance.animator.SetBool("Second Attack", true);
          PlayerController.instance.animator.SetBool("Idle", false);
          PlayerController.instance.animator.SetBool("Running", false);
          PlayerController.instance.animator.SetBool("Single Jump", false);
          PlayerController.instance.animator.SetBool("Single Jump Running", false);
          PlayerController.instance.animator.SetBool("Double Jump", false);
          PlayerController.instance.animator.SetBool("Pushing", false);
          PlayerController.instance.animator.SetBool("Pushing Idle", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
     }

     public void ResetSecondAttack()
     {
          PlayerController.instance.animator.SetBool("Second Attack", false);
          PlayerController.instance.animator.SetBool("Idle", false);
          PlayerController.instance.animator.SetBool("Running", false);
          PlayerController.instance.animator.SetBool("Single Jump", false);
          PlayerController.instance.animator.SetBool("Single Jump Running", false);
          PlayerController.instance.animator.SetBool("Double Jump", false);
          PlayerController.instance.animator.SetBool("Pushing", false);
          PlayerController.instance.animator.SetBool("Pushing Idle", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
     }

     public void SetFinalAttack()
     {
          PlayerController.instance.animator.SetBool("Final Attack", true);
          PlayerController.instance.animator.SetBool("Idle", false);
          PlayerController.instance.animator.SetBool("Running", false);
          PlayerController.instance.animator.SetBool("Single Jump", false);
          PlayerController.instance.animator.SetBool("Single Jump Running", false);
          PlayerController.instance.animator.SetBool("Double Jump", false);
          PlayerController.instance.animator.SetBool("Pushing", false);
          PlayerController.instance.animator.SetBool("Pushing Idle", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
     }

     public void ResetAttacks()
     {
          PlayerController.instance.animator.SetBool("Final Attack", false);
          PlayerController.instance.animator.SetBool("Second Attack", false);
          PlayerController.instance.animator.SetBool("First Attack", false);
     }

     public void SetIsGrounded()
     {
          PlayerController.instance.animator.SetBool("IsGrounded", PlayerController.instance.IsGrounded());
     }

     public void SetIdle()
     {
          if ((PlayerController.instance.movement.horizontal == 0 &&
               PlayerController.instance.movement.vertical == 0) &&
               PlayerController.instance.IsGrounded() &&
               PlayerController.instance.push.pushingObj == false &&
               PlayerAttackController.instance.currentAttack == 0)
          {
               PlayerController.instance.animator.SetBool("Idle", true);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
          }
     }

     public void SetRunning()
     {
          if ((PlayerController.instance.movement.horizontal != 0 ||
               PlayerController.instance.movement.vertical != 0) &&
               PlayerController.instance.IsGrounded() &&
               PlayerController.instance.movement.rbody.velocity.y <= 0 &&
               PlayerController.instance.push.pushingObj == false &&
               PlayerAttackController.instance.currentAttack == 0)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", true);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
          }
     }

     public void SetSingleJump()
     {
          if (PlayerController.instance.movement.rbody.velocity.y > 0 &&
               (PlayerController.instance.movement.horizontal != 0 ||
               PlayerController.instance.movement.vertical != 0) &&
               PlayerController.instance.jump.currentJump == 1 &&
               !PlayerController.instance.IsGrounded() &&
               !PlayerAttackController.instance.attaking)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", true);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
          }
     }

     public void SetSingleJumpRunning()
     {
          if (PlayerController.instance.movement.rbody.velocity.y > 0 &&
              (PlayerController.instance.movement.horizontal == 0 &&
              PlayerController.instance.movement.vertical == 0) &&
              PlayerController.instance.jump.currentJump == 1 &&
              !PlayerController.instance.IsGrounded() &&
              !PlayerAttackController.instance.attaking)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", true);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
          }
     }

     public void SetDoubleJump()
     {
          if (PlayerController.instance.movement.rbody.velocity.y > 0 &&
               PlayerController.instance.jump.currentJump == PlayerController.instance.jump.maxJump &&
              !PlayerController.instance.IsGrounded() &&
              !PlayerAttackController.instance.attaking)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", true);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
          }
     }

     public void SetPush()
     {
          if ((PlayerController.instance.movement.horizontal != 0 ||
               PlayerController.instance.movement.vertical != 0) &&
               PlayerController.instance.push.pushingObj == true &&
               !PlayerAttackController.instance.attaking)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", true);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
          }
     }

     public void SetPushIdle()
     {
          if ((PlayerController.instance.movement.horizontal == 0 &&
               PlayerController.instance.movement.vertical == 0) &&
               PlayerController.instance.push.pushingObj == true &&
               !PlayerAttackController.instance.attaking)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", true);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
          }
     }

     public void SetFallingIdle()
     {
          if (PlayerController.instance.movement.rbody.velocity.y <= -2f &&
              !PlayerController.instance.IsGrounded() &&
              !PlayerAttackController.instance.attaking)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Idle", true);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
          }
     }

     public void SetFallingGround()
     {
          if ((PlayerController.instance.movement.horizontal == 0 &&
              PlayerController.instance.movement.vertical == 0) &&
              PlayerController.instance.IsGrounded() &&
              !PlayerAttackController.instance.attaking &&
              PlayerController.instance.jump.currentJump == PlayerController.instance.jump.maxJump)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", true);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               _canJumpAfterFalling = false;
               PlayerController.instance.jump.fallingDust.Play();
          }
     }

     public void SetFallingRunning()
     {
          if ((PlayerController.instance.movement.horizontal != 0 ||
               PlayerController.instance.movement.vertical != 0) &&
               PlayerController.instance.IsGrounded() &&
               !PlayerAttackController.instance.attaking &&
               PlayerController.instance.jump.currentJump == PlayerController.instance.jump.maxJump)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", true);
               _canJumpAfterFalling = false;
               PlayerController.instance.jump.fallingDust.Play();
          }
     }

     public void CanJumpAfterFalling()
     {
          if (!_canJumpAfterFalling)
          {
               if (_countdownAfterFalling < 1)
               {
                    _countdownAfterFalling += Time.deltaTime / timeToJump;
                    afterFalling = true;
               }
               else
               {
                    afterFalling = false;
                    _canJumpAfterFalling = true;
                    _countdownAfterFalling = 0;
               }
          }
     }

     public void SetDead()
     {
          if (PlayerController.instance.death.dead)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Dying", true);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
          }
     }

     public void SetBalance()
     {
          PlayerController.instance.animator.SetBool("Idle", false);
          PlayerController.instance.animator.SetBool("Running", false);
          PlayerController.instance.animator.SetBool("Single Jump", false);
          PlayerController.instance.animator.SetBool("Single Jump Running", false);
          PlayerController.instance.animator.SetBool("Double Jump", false);
          PlayerController.instance.animator.SetBool("Pushing", false);
          PlayerController.instance.animator.SetBool("Pushing Idle", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", true);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
     }
}