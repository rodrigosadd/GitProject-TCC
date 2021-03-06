﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
     public static PlayerAnimationController instance;
     public ParticleSystem oil;
     public ParticleSystem dust;
     public float timeToJump;
     public bool afterFalling;
     public bool fallingIdle;
     public bool balance;
     public bool alreadyPlayedFallingAction;
     private bool _canJumpAfterFalling;
     private bool _inFallingAction;
     private float _countdownAfterFalling;
     private float _countdownFallingAction;
     private bool _firstAttack;
     private bool _secondAttack;
     private bool _finalAttack;

     void Awake()
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
          SetDropBox();
          SetFallingAction();
          InFallingAction();
          SetFallingIdle();
          SetFallingIdleDoubleJump();
          SetFallingGround();
          SetFallingRunning();
          CanJumpAfterFalling();
          SetSliding();
          SetDead();
          SetEntryTeleport();
          SetExitTeleport();
          SetInteract();
          CheckAttackAnimationIsFinished();
     }

#region Attack
     public void CheckAttackAnimationIsFinished()
     {
          _firstAttack = PlayerController.instance.animator.GetBool("First Attack");
          _secondAttack = PlayerController.instance.animator.GetBool("Second Attack");
          _finalAttack = PlayerController.instance.animator.GetBool("Final Attack");
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
          PlayerController.instance.animator.SetBool("Drop Box", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", false);
          PlayerController.instance.animator.SetBool("Falling Action", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
          PlayerController.instance.animator.SetBool("Sliding", false);
          PlayerController.instance.animator.SetBool("Interacting", false);  
          PlayerController.instance.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
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
          PlayerController.instance.animator.SetBool("Drop Box", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", false);
          PlayerController.instance.animator.SetBool("Falling Action", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
          PlayerController.instance.animator.SetBool("Sliding", false);
          PlayerController.instance.animator.SetBool("Interacting", false); 
          PlayerController.instance.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
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
          PlayerController.instance.animator.SetBool("Drop Box", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", false);
          PlayerController.instance.animator.SetBool("Falling Action", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
          PlayerController.instance.animator.SetBool("Sliding", false);
          PlayerController.instance.animator.SetBool("Interacting", false); 
          PlayerController.instance.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
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
          PlayerController.instance.animator.SetBool("Drop Box", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", false);
          PlayerController.instance.animator.SetBool("Falling Action", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
          PlayerController.instance.animator.SetBool("Sliding", false);
          PlayerController.instance.animator.SetBool("Interacting", false); 
          PlayerController.instance.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
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
          PlayerController.instance.animator.SetBool("Drop Box", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", false);
          PlayerController.instance.animator.SetBool("Falling Action", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
          PlayerController.instance.animator.SetBool("Sliding", false);
          PlayerController.instance.animator.SetBool("Interacting", false); 
          PlayerController.instance.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void ResetAttacks()
     {
          PlayerController.instance.animator.SetBool("Final Attack", false);
          PlayerController.instance.animator.SetBool("Second Attack", false);
          PlayerController.instance.animator.SetBool("First Attack", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }
#endregion

#region Idle
     public void SetIsGrounded()
     {
          PlayerController.instance.animator.SetBool("IsGrounded", PlayerController.instance.movement.isGrounded);
     }

     public void SetIdle()
     {
          if ((PlayerController.instance.movement.horizontal == 0 &&
               PlayerController.instance.movement.vertical == 0) &&
               PlayerController.instance.movement.currentSpeed == 0f &&
               PlayerController.instance.movement.isGrounded &&
               !balance &&
               !PlayerController.instance.push.pushingObj &&
               PlayerAttackController.instance.currentAttack == 0 &&
               !PlayerController.instance.levelMechanics.interacting &&
               PlayerController.instance.movement.canMove &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", true);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
#endregion
     
#region Running
     public void SetRunning()
     {
          if ((PlayerController.instance.movement.horizontal != 0 ||
               PlayerController.instance.movement.vertical != 0) &&
               PlayerController.instance.movement.currentSpeed > 0f &&
               PlayerController.instance.movement.isGrounded &&
               !balance &&
               PlayerController.instance.movement.controller.velocity.y <= 0 &&
               !PlayerController.instance.push.pushingObj &&
               !PlayerController.instance.push.droppingObj &&
               !PlayerController.instance.push.setPositionDropObject &&
               PlayerAttackController.instance.currentAttack == 0 &&
               !PlayerAttackController.instance.attaking &&
               PlayerController.instance.movement.canMove &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", true);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);   
               PlayerController.instance.animator.SetBool("Interacting", false);   
               PlayerController.instance.animator.SetBool("Power Up", false);          
               fallingIdle = false;
               alreadyPlayedFallingAction = false; 
          }
     }
#endregion

#region Jump
     public void SetSingleJump()
     {
          if (PlayerController.instance.movement.velocity.y > -2 &&
               (PlayerController.instance.movement.horizontal != 0 ||
               PlayerController.instance.movement.vertical != 0) &&
               PlayerController.instance.jump.currentJump == 1 &&
               !PlayerController.instance.movement.isGrounded &&
               !PlayerAttackController.instance.attaking &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", true);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }

     public void SetSingleJumpRunning()
     {
          if (PlayerController.instance.movement.velocity.y > -2 &&
              (PlayerController.instance.movement.horizontal == 0 &&
              PlayerController.instance.movement.vertical == 0) &&
              PlayerController.instance.jump.currentJump == 1 &&
              !PlayerController.instance.movement.isGrounded &&
              !PlayerAttackController.instance.attaking &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", true);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }

     public void SetDoubleJump()
     {
          if (PlayerController.instance.movement.controller.velocity.y > 0 &&
               PlayerController.instance.jump.currentJump >= 2 &&
              !PlayerController.instance.movement.isGrounded &&
              !PlayerAttackController.instance.attaking &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", true);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
#endregion

#region Push
     public void SetPush()
     {
          if ((PlayerController.instance.movement.horizontal != 0 ||
               PlayerController.instance.movement.vertical != 0) &&
               PlayerController.instance.push.pushingObj == true &&
               !PlayerAttackController.instance.attaking &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", true);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }

     public void SetPushIdle()
     {
          if ((PlayerController.instance.movement.horizontal == 0 &&
               PlayerController.instance.movement.vertical == 0) &&
               PlayerController.instance.push.pushingObj == true &&
               !PlayerAttackController.instance.attaking &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", true);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }

     public void SetDropBox()
     {
          if(PlayerController.instance.push.droppingObj &&
             !PlayerController.instance.push.pushingObj &&
             !PlayerAttackController.instance.attaking &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", true);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
#endregion

#region Falling
     public void ResetFallingAnimations()
     {
          PlayerController.instance.animator.SetBool("Idle", true);
          PlayerController.instance.animator.SetBool("Falling Action", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void SetFallingAction()
     {
          if(PlayerController.instance.movement.controller.velocity.y <= -0.1 &&
              !PlayerController.instance.movement.isGrounded &&
              !PlayerAttackController.instance.attaking &&
              PlayerController.instance.jump.currentJump >= 2 &&
              !alreadyPlayedFallingAction &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", true);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);  
               PlayerController.instance.animator.SetBool("Sliding", false); 
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               _inFallingAction = true;                          
          }
     }

     public void SetFallingIdle()
     {
          if (PlayerController.instance.movement.controller.velocity.y <= -0.1 &&
              !PlayerController.instance.movement.isGrounded &&
              !PlayerAttackController.instance.attaking &&
              !alreadyPlayedFallingAction &&
              PlayerController.instance.jump.currentJump <= 1 &&
              !_inFallingAction &&
              !_firstAttack &&
              !_secondAttack &&
              !_finalAttack &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", true);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Final Attack", false);
               PlayerController.instance.animator.SetBool("Second Attack", false);
               PlayerController.instance.animator.SetBool("First Attack", false);
               PlayerController.instance.animator.SetBool("Power Up", false);

               if (!fallingIdle)
               {
                    fallingIdle = true;                    
               }
          }
     }

     public void SetFallingIdleDoubleJump()
     {
          if (PlayerController.instance.movement.controller.velocity.y <= -0.1 &&
              !PlayerController.instance.movement.isGrounded &&
              !PlayerAttackController.instance.attaking &&
              PlayerController.instance.jump.currentJump >= 2 &&
              alreadyPlayedFallingAction &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", true);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               
               if (!fallingIdle)
               {
                    fallingIdle = true;
               }
          }
     }

     public void SetFallingGround()
     {
          if ((PlayerController.instance.movement.horizontal == 0 &&
              PlayerController.instance.movement.vertical == 0) &&
              PlayerController.instance.movement.isGrounded &&
              !PlayerAttackController.instance.attaking &&
              PlayerController.instance.jump.currentJump >= 2 &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", true);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               _canJumpAfterFalling = false;
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
               PlayerController.instance.jump.fallingDust.Play();
          }
     }

     public void SetFallingRunning()
     {
          if ((PlayerController.instance.movement.horizontal != 0 ||
               PlayerController.instance.movement.vertical != 0) &&
               PlayerController.instance.movement.isGrounded &&
               !PlayerAttackController.instance.attaking &&
               PlayerController.instance.jump.currentJump >= 2 &&
               !PlayerController.instance.death.dead && 
               !PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", true);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               _canJumpAfterFalling = false;
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
               PlayerController.instance.jump.fallingDust.Play();
          }
     }

     public void InFallingAction()
     {
          if (_inFallingAction)
          {
               if (_countdownFallingAction < 1)
               {
                    _countdownFallingAction += Time.deltaTime / 0.5f;                    
               }
               else
               {                    
                    _countdownFallingAction = 0;
                    alreadyPlayedFallingAction = true;
                    _inFallingAction = false;
               }
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
#endregion

#region Balance
     public void SetBalance()
     {
          PlayerController.instance.animator.SetBool("Idle", false);
          PlayerController.instance.animator.SetBool("Running", false);
          PlayerController.instance.animator.SetBool("Single Jump", false);
          PlayerController.instance.animator.SetBool("Single Jump Running", false);
          PlayerController.instance.animator.SetBool("Double Jump", false);
          PlayerController.instance.animator.SetBool("Pushing", false);
          PlayerController.instance.animator.SetBool("Pushing Idle", false);
          PlayerController.instance.animator.SetBool("Drop Box", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", true);
          PlayerController.instance.animator.SetBool("Falling Action", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
          PlayerController.instance.animator.SetBool("Sliding", false);
          PlayerController.instance.animator.SetBool("Interacting", false); 
          PlayerController.instance.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;

          if (!balance)
          {
               balance = true;
          }
     }
#endregion

#region Sliding
     public void SetSliding()
     {
          if(PlayerController.instance.levelMechanics.sliding)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", true);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;                            
          }
     }
#endregion

#region Teleport
     public void SetEntryTeleport()
     {
          if(PlayerController.instance.levelMechanics.entryTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Entry Teleport", true);
               PlayerController.instance.animator.SetBool("Exit Teleport", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }

     public void SetExitTeleport()
     {
          if(PlayerController.instance.levelMechanics.exitTeleport)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Entry Teleport", false);
               PlayerController.instance.animator.SetBool("Exit Teleport", true);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
#endregion

#region Interact
     public void SetInteract()
     {
          if(PlayerController.instance.levelMechanics.interacting)
          {
               PlayerController.instance.animator.SetBool("Idle", false);
               PlayerController.instance.animator.SetBool("Running", false);
               PlayerController.instance.animator.SetBool("Single Jump", false);
               PlayerController.instance.animator.SetBool("Single Jump Running", false);
               PlayerController.instance.animator.SetBool("Double Jump", false);
               PlayerController.instance.animator.SetBool("Pushing", false);
               PlayerController.instance.animator.SetBool("Pushing Idle", false);
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", false);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Entry Teleport", false);
               PlayerController.instance.animator.SetBool("Exit Teleport", false);
               PlayerController.instance.animator.SetBool("Interacting", true);     
               PlayerController.instance.animator.SetBool("Power Up", false);          
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
               
          }
     }

     public void PlayParticleDust()
     {
          dust.Play();
     }

     public void StopParticleDust()
     {
          dust.Stop();
     }
#endregion

#region Power Up
     public void SetPowerUp()
     {
          PlayerController.instance.animator.SetBool("Idle", false);
          PlayerController.instance.animator.SetBool("Running", false);
          PlayerController.instance.animator.SetBool("Single Jump", false);
          PlayerController.instance.animator.SetBool("Single Jump Running", false);
          PlayerController.instance.animator.SetBool("Double Jump", false);
          PlayerController.instance.animator.SetBool("Pushing", false);
          PlayerController.instance.animator.SetBool("Pushing Idle", false);
          PlayerController.instance.animator.SetBool("Drop Box", false);
          PlayerController.instance.animator.SetBool("Dying", false);
          PlayerController.instance.animator.SetBool("Balance", false);
          PlayerController.instance.animator.SetBool("Falling Action", false);
          PlayerController.instance.animator.SetBool("Falling Idle", false);
          PlayerController.instance.animator.SetBool("Falling Ground", false);
          PlayerController.instance.animator.SetBool("Falling Running", false);
          PlayerController.instance.animator.SetBool("Sliding", false);
          PlayerController.instance.animator.SetBool("Entry Teleport", false);
          PlayerController.instance.animator.SetBool("Exit Teleport", false);
          PlayerController.instance.animator.SetBool("Interacting", false);     
          PlayerController.instance.animator.SetBool("Power Up", true); 
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }
#endregion

#region Dead
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
               PlayerController.instance.animator.SetBool("Drop Box", false);
               PlayerController.instance.animator.SetBool("Dying", true);
               PlayerController.instance.animator.SetBool("Balance", false);
               PlayerController.instance.animator.SetBool("Falling Action", false);
               PlayerController.instance.animator.SetBool("Falling Idle", false);
               PlayerController.instance.animator.SetBool("Falling Ground", false);
               PlayerController.instance.animator.SetBool("Falling Running", false);
               PlayerController.instance.animator.SetBool("Sliding", false);
               PlayerController.instance.animator.SetBool("Interacting", false); 
               PlayerController.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
#endregion
}