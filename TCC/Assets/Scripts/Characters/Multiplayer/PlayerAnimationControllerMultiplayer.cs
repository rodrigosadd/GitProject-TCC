using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationControllerMultiplayer : MonoBehaviour
{   
    public static PlayerAnimationControllerMultiplayer instance;
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
          _firstAttack = PlayerControllerMultiplayer.instance.animator.GetBool("First Attack");
          _secondAttack = PlayerControllerMultiplayer.instance.animator.GetBool("Second Attack");
          _finalAttack = PlayerControllerMultiplayer.instance.animator.GetBool("Final Attack");
     }

     public void SetFirstAttack()
     {
          PlayerControllerMultiplayer.instance.animator.SetBool("First Attack", true);
          PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false);  
          PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void ResetFirstAttack()
     {
          PlayerControllerMultiplayer.instance.animator.SetBool("First Attack", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
          PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void SetSecondAttack()
     {
          PlayerControllerMultiplayer.instance.animator.SetBool("Second Attack", true);
          PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
          PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void ResetSecondAttack()
     {
          PlayerControllerMultiplayer.instance.animator.SetBool("Second Attack", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
          PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void SetFinalAttack()
     {
          PlayerControllerMultiplayer.instance.animator.SetBool("Final Attack", true);
          PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
          PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void ResetAttacks()
     {
          PlayerControllerMultiplayer.instance.animator.SetBool("Final Attack", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Second Attack", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("First Attack", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }
#endregion

#region Idle
     public void SetIsGrounded()
     {
          PlayerControllerMultiplayer.instance.animator.SetBool("IsGrounded", PlayerControllerMultiplayer.instance.movement.isGrounded);
     }

     public void SetIdle()
     {
          if ((PlayerControllerMultiplayer.instance.movement.horizontal == 0 &&
               PlayerControllerMultiplayer.instance.movement.vertical == 0) &&
               PlayerControllerMultiplayer.instance.movement.currentSpeed == 0f &&
               PlayerControllerMultiplayer.instance.movement.isGrounded &&
               !balance &&
               PlayerAttackControllerMultiplayer.instance.currentAttack == 0 &&
               !PlayerControllerMultiplayer.instance.levelMechanics.interacting &&
               PlayerControllerMultiplayer.instance.movement.canMove &&
               !PlayerControllerMultiplayer.instance.death.dead && 
               !PlayerControllerMultiplayer.instance.levelMechanics.entryTeleport)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
#endregion
     
#region Running
     public void SetRunning()
     {
          if ((PlayerControllerMultiplayer.instance.movement.horizontal != 0 ||
               PlayerControllerMultiplayer.instance.movement.vertical != 0) &&
               PlayerControllerMultiplayer.instance.movement.currentSpeed > 0f &&
               PlayerControllerMultiplayer.instance.movement.isGrounded &&
               !balance &&
               PlayerControllerMultiplayer.instance.movement.controller.velocity.y <= 0 &&
               PlayerAttackControllerMultiplayer.instance.currentAttack == 0 &&
               !PlayerAttackControllerMultiplayer.instance.attaking &&
               PlayerControllerMultiplayer.instance.movement.canMove &&
               !PlayerControllerMultiplayer.instance.death.dead && 
               !PlayerControllerMultiplayer.instance.levelMechanics.entryTeleport)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);   
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false);   
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);          
               fallingIdle = false;
               alreadyPlayedFallingAction = false; 
          }
     }
#endregion

#region Jump
     public void SetSingleJump()
     {
          if (PlayerControllerMultiplayer.instance.movement.velocity.y > -2 &&
               (PlayerControllerMultiplayer.instance.movement.horizontal != 0 ||
               PlayerControllerMultiplayer.instance.movement.vertical != 0) &&
               PlayerControllerMultiplayer.instance.jump.currentJump == 1 &&
               !PlayerControllerMultiplayer.instance.movement.isGrounded &&
               !PlayerAttackControllerMultiplayer.instance.attaking &&
               !PlayerControllerMultiplayer.instance.death.dead && 
               !PlayerControllerMultiplayer.instance.levelMechanics.entryTeleport)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }

     public void SetSingleJumpRunning()
     {
          if (PlayerControllerMultiplayer.instance.movement.velocity.y > -2 &&
              (PlayerControllerMultiplayer.instance.movement.horizontal == 0 &&
              PlayerControllerMultiplayer.instance.movement.vertical == 0) &&
              PlayerControllerMultiplayer.instance.jump.currentJump == 1 &&
              !PlayerControllerMultiplayer.instance.movement.isGrounded &&
              !PlayerAttackControllerMultiplayer.instance.attaking &&
               !PlayerControllerMultiplayer.instance.death.dead && 
               !PlayerControllerMultiplayer.instance.levelMechanics.entryTeleport)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }

     public void SetDoubleJump()
     {
          if (PlayerControllerMultiplayer.instance.movement.controller.velocity.y > 0 &&
               PlayerControllerMultiplayer.instance.jump.currentJump >= 2 &&
              !PlayerControllerMultiplayer.instance.movement.isGrounded &&
              !PlayerAttackControllerMultiplayer.instance.attaking &&
               !PlayerControllerMultiplayer.instance.death.dead && 
               !PlayerControllerMultiplayer.instance.levelMechanics.entryTeleport)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
#endregion

#region Falling
     public void ResetFallingAnimations()
     {
          PlayerControllerMultiplayer.instance.animator.SetBool("Idle", true);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void SetFallingAction()
     {
          if(PlayerControllerMultiplayer.instance.movement.controller.velocity.y <= -0.1 &&
              !PlayerControllerMultiplayer.instance.movement.isGrounded &&
              !PlayerAttackControllerMultiplayer.instance.attaking &&
              PlayerControllerMultiplayer.instance.jump.currentJump >= 2 &&
              !alreadyPlayedFallingAction &&
               !PlayerControllerMultiplayer.instance.death.dead && 
               !PlayerControllerMultiplayer.instance.levelMechanics.entryTeleport)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);  
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
               _inFallingAction = true;                          
          }
     }

     public void SetFallingIdle()
     {
          if (PlayerControllerMultiplayer.instance.movement.controller.velocity.y <= -0.1 &&
              !PlayerControllerMultiplayer.instance.movement.isGrounded &&
              !PlayerAttackControllerMultiplayer.instance.attaking &&
              !alreadyPlayedFallingAction &&
              PlayerControllerMultiplayer.instance.jump.currentJump <= 1 &&
              !_inFallingAction &&
              !_firstAttack &&
              !_secondAttack &&
              !_finalAttack &&
               !PlayerControllerMultiplayer.instance.death.dead && 
               !PlayerControllerMultiplayer.instance.levelMechanics.entryTeleport)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Final Attack", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Second Attack", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("First Attack", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);

               if (!fallingIdle)
               {
                    fallingIdle = true;                    
               }
          }
     }

     public void SetFallingIdleDoubleJump()
     {
          if (PlayerControllerMultiplayer.instance.movement.controller.velocity.y <= -0.1 &&
              !PlayerControllerMultiplayer.instance.movement.isGrounded &&
              !PlayerAttackControllerMultiplayer.instance.attaking &&
              PlayerControllerMultiplayer.instance.jump.currentJump >= 2 &&
              alreadyPlayedFallingAction &&
               !PlayerControllerMultiplayer.instance.death.dead && 
               !PlayerControllerMultiplayer.instance.levelMechanics.entryTeleport)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
               
               if (!fallingIdle)
               {
                    fallingIdle = true;
               }
          }
     }

     public void SetFallingGround()
     {
          if ((PlayerControllerMultiplayer.instance.movement.horizontal == 0 &&
              PlayerControllerMultiplayer.instance.movement.vertical == 0) &&
              PlayerControllerMultiplayer.instance.movement.isGrounded &&
              !PlayerAttackControllerMultiplayer.instance.attaking &&
              PlayerControllerMultiplayer.instance.jump.currentJump >= 2 &&
               !PlayerControllerMultiplayer.instance.death.dead && 
               !PlayerControllerMultiplayer.instance.levelMechanics.entryTeleport)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
               _canJumpAfterFalling = false;
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
               PlayerControllerMultiplayer.instance.jump.fallingDust.Play();
          }
     }

     public void SetFallingRunning()
     {
          if ((PlayerControllerMultiplayer.instance.movement.horizontal != 0 ||
               PlayerControllerMultiplayer.instance.movement.vertical != 0) &&
               PlayerControllerMultiplayer.instance.movement.isGrounded &&
               !PlayerAttackControllerMultiplayer.instance.attaking &&
               PlayerControllerMultiplayer.instance.jump.currentJump >= 2 &&
               !PlayerControllerMultiplayer.instance.death.dead && 
               !PlayerControllerMultiplayer.instance.levelMechanics.entryTeleport)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
               _canJumpAfterFalling = false;
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
               PlayerControllerMultiplayer.instance.jump.fallingDust.Play();
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
          PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Balance", true);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
          PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
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
          if(PlayerControllerMultiplayer.instance.levelMechanics.sliding)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;                            
          }
     }
#endregion

#region Teleport
     public void SetEntryTeleport()
     {
          if(PlayerControllerMultiplayer.instance.levelMechanics.entryTeleport)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Entry Teleport", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Exit Teleport", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }

     public void SetExitTeleport()
     {
          if(PlayerControllerMultiplayer.instance.levelMechanics.exitTeleport)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Entry Teleport", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Exit Teleport", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
#endregion

#region Interact
     public void SetInteract()
     {
          if(PlayerControllerMultiplayer.instance.levelMechanics.interacting)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Entry Teleport", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Exit Teleport", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", true);     
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);          
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
          PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Dying", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Entry Teleport", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Exit Teleport", false);
          PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false);     
          PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", true); 
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }
#endregion

#region Dead
     public void SetDead()
     {
          if (PlayerControllerMultiplayer.instance.death.dead)
          {
               PlayerControllerMultiplayer.instance.animator.SetBool("Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Single Jump Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Double Jump", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Pushing Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Drop Box", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Dying", true);
               PlayerControllerMultiplayer.instance.animator.SetBool("Balance", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Action", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Idle", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Ground", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Falling Running", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Sliding", false);
               PlayerControllerMultiplayer.instance.animator.SetBool("Interacting", false); 
               PlayerControllerMultiplayer.instance.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
#endregion
}
