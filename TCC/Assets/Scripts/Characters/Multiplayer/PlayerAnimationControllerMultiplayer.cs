using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimationControllerMultiplayer : MonoBehaviourPun
{   
    public static PlayerAnimationControllerMultiplayer instance;
    public ParticleSystem oil;
    public ParticleSystem dust;
    public float timeToJump;
    public bool afterFalling;
    public bool fallingIdle;
    public bool balance;
    public bool alreadyPlayedFallingAction;
    public PlayerControllerMultiplayer multiplayerController;
    private bool _canJumpAfterFalling;
    private bool _inFallingAction;
    private float _countdownAfterFalling;
    private float _countdownFallingAction;
    private bool _firstAttack;
    private bool _secondAttack;
    private bool _finalAttack;
    private PhotonView photonView;
    private Animator animator;

    void Awake()
    {
        instance = this;
        photonView = GetComponent<PhotonView>();
        animator = GetComponent<Animator>();

    }

     void Update()
     {
        if (photonView.IsMine) {
            photonView.RPC("SetIsGrounded", RpcTarget.AllBuffered);
            photonView.RPC("SetIdle", RpcTarget.AllBuffered);
            photonView.RPC("SetRunning", RpcTarget.AllBuffered);
            //SetIsGrounded();
            //SetIdle();
            //SetRunning();
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
     }

#region Attack
     public void CheckAttackAnimationIsFinished()
     {
          _firstAttack = multiplayerController.photon.animator.GetBool("First Attack");
          _secondAttack = multiplayerController.photon.animator.GetBool("Second Attack");
          _finalAttack = multiplayerController.photon.animator.GetBool("Final Attack");
     }

     public void SetFirstAttack()
     {
          multiplayerController.photon.animator.SetBool("First Attack", true);
          multiplayerController.photon.animator.SetBool("Idle", false);
          multiplayerController.photon.animator.SetBool("Running", false);
          multiplayerController.photon.animator.SetBool("Single Jump", false);
          multiplayerController.photon.animator.SetBool("Single Jump Running", false);
          multiplayerController.photon.animator.SetBool("Double Jump", false);
          multiplayerController.photon.animator.SetBool("Pushing", false);
          multiplayerController.photon.animator.SetBool("Pushing Idle", false);
          multiplayerController.photon.animator.SetBool("Drop Box", false);
          multiplayerController.photon.animator.SetBool("Dying", false);
          multiplayerController.photon.animator.SetBool("Balance", false);
          multiplayerController.photon.animator.SetBool("Falling Action", false);
          multiplayerController.photon.animator.SetBool("Falling Idle", false);
          multiplayerController.photon.animator.SetBool("Falling Ground", false);
          multiplayerController.photon.animator.SetBool("Falling Running", false);
          multiplayerController.photon.animator.SetBool("Sliding", false);
          multiplayerController.photon.animator.SetBool("Interacting", false);  
          multiplayerController.photon.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void ResetFirstAttack()
     {
          multiplayerController.photon.animator.SetBool("First Attack", false);
          multiplayerController.photon.animator.SetBool("Idle", false);
          multiplayerController.photon.animator.SetBool("Running", false);
          multiplayerController.photon.animator.SetBool("Single Jump", false);
          multiplayerController.photon.animator.SetBool("Single Jump Running", false);
          multiplayerController.photon.animator.SetBool("Double Jump", false);
          multiplayerController.photon.animator.SetBool("Pushing", false);
          multiplayerController.photon.animator.SetBool("Pushing Idle", false);
          multiplayerController.photon.animator.SetBool("Drop Box", false);
          multiplayerController.photon.animator.SetBool("Dying", false);
          multiplayerController.photon.animator.SetBool("Balance", false);
          multiplayerController.photon.animator.SetBool("Falling Action", false);
          multiplayerController.photon.animator.SetBool("Falling Idle", false);
          multiplayerController.photon.animator.SetBool("Falling Ground", false);
          multiplayerController.photon.animator.SetBool("Falling Running", false);
          multiplayerController.photon.animator.SetBool("Sliding", false);
          multiplayerController.photon.animator.SetBool("Interacting", false); 
          multiplayerController.photon.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void SetSecondAttack()
     {
          multiplayerController.photon.animator.SetBool("Second Attack", true);
          multiplayerController.photon.animator.SetBool("Idle", false);
          multiplayerController.photon.animator.SetBool("Running", false);
          multiplayerController.photon.animator.SetBool("Single Jump", false);
          multiplayerController.photon.animator.SetBool("Single Jump Running", false);
          multiplayerController.photon.animator.SetBool("Double Jump", false);
          multiplayerController.photon.animator.SetBool("Pushing", false);
          multiplayerController.photon.animator.SetBool("Pushing Idle", false);
          multiplayerController.photon.animator.SetBool("Drop Box", false);
          multiplayerController.photon.animator.SetBool("Dying", false);
          multiplayerController.photon.animator.SetBool("Balance", false);
          multiplayerController.photon.animator.SetBool("Falling Action", false);
          multiplayerController.photon.animator.SetBool("Falling Idle", false);
          multiplayerController.photon.animator.SetBool("Falling Ground", false);
          multiplayerController.photon.animator.SetBool("Falling Running", false);
          multiplayerController.photon.animator.SetBool("Sliding", false);
          multiplayerController.photon.animator.SetBool("Interacting", false); 
          multiplayerController.photon.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void ResetSecondAttack()
     {
          multiplayerController.photon.animator.SetBool("Second Attack", false);
          multiplayerController.photon.animator.SetBool("Idle", false);
          multiplayerController.photon.animator.SetBool("Running", false);
          multiplayerController.photon.animator.SetBool("Single Jump", false);
          multiplayerController.photon.animator.SetBool("Single Jump Running", false);
          multiplayerController.photon.animator.SetBool("Double Jump", false);
          multiplayerController.photon.animator.SetBool("Pushing", false);
          multiplayerController.photon.animator.SetBool("Pushing Idle", false);
          multiplayerController.photon.animator.SetBool("Drop Box", false);
          multiplayerController.photon.animator.SetBool("Dying", false);
          multiplayerController.photon.animator.SetBool("Balance", false);
          multiplayerController.photon.animator.SetBool("Falling Action", false);
          multiplayerController.photon.animator.SetBool("Falling Idle", false);
          multiplayerController.photon.animator.SetBool("Falling Ground", false);
          multiplayerController.photon.animator.SetBool("Falling Running", false);
          multiplayerController.photon.animator.SetBool("Sliding", false);
          multiplayerController.photon.animator.SetBool("Interacting", false); 
          multiplayerController.photon.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void SetFinalAttack()
     {
          multiplayerController.photon.animator.SetBool("Final Attack", true);
          multiplayerController.photon.animator.SetBool("Idle", false);
          multiplayerController.photon.animator.SetBool("Running", false);
          multiplayerController.photon.animator.SetBool("Single Jump", false);
          multiplayerController.photon.animator.SetBool("Single Jump Running", false);
          multiplayerController.photon.animator.SetBool("Double Jump", false);
          multiplayerController.photon.animator.SetBool("Pushing", false);
          multiplayerController.photon.animator.SetBool("Pushing Idle", false);
          multiplayerController.photon.animator.SetBool("Drop Box", false);
          multiplayerController.photon.animator.SetBool("Dying", false);
          multiplayerController.photon.animator.SetBool("Balance", false);
          multiplayerController.photon.animator.SetBool("Falling Action", false);
          multiplayerController.photon.animator.SetBool("Falling Idle", false);
          multiplayerController.photon.animator.SetBool("Falling Ground", false);
          multiplayerController.photon.animator.SetBool("Falling Running", false);
          multiplayerController.photon.animator.SetBool("Sliding", false);
          multiplayerController.photon.animator.SetBool("Interacting", false); 
          multiplayerController.photon.animator.SetBool("Power Up", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void ResetAttacks()
     {
          multiplayerController.photon.animator.SetBool("Final Attack", false);
          multiplayerController.photon.animator.SetBool("Second Attack", false);
          multiplayerController.photon.animator.SetBool("First Attack", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }
#endregion

#region Idle
    [PunRPC]
    public void SetIsGrounded()
    {
         animator.SetBool("IsGrounded", multiplayerController.movement.isGrounded);
    }
    [PunRPC]
    public void SetIdle()
     {
          if ((multiplayerController.movement.horizontal == 0 &&
               multiplayerController.movement.vertical == 0) &&
               multiplayerController.movement.currentSpeed == 0f &&
               multiplayerController.movement.isGrounded &&
               !balance &&
               PlayerAttackControllerMultiplayer.instance.currentAttack == 0 &&
               !multiplayerController.levelMechanics.interacting &&
               multiplayerController.movement.canMove &&
               !multiplayerController.death.dead && 
               !multiplayerController.levelMechanics.entryTeleport)
          {
               animator.SetBool("Idle", true);
               animator.SetBool("Running", false);
               animator.SetBool("Single Jump", false);
               animator.SetBool("Single Jump Running", false);
               animator.SetBool("Double Jump", false);
               animator.SetBool("Pushing", false);
               animator.SetBool("Pushing Idle", false);
               animator.SetBool("Drop Box", false);
               animator.SetBool("Dying", false);
               animator.SetBool("Balance", false);
               animator.SetBool("Falling Action", false);
               animator.SetBool("Falling Idle", false);
               animator.SetBool("Falling Ground", false);
               animator.SetBool("Falling Running", false);
               animator.SetBool("Sliding", false);
               animator.SetBool("Interacting", false); 
               animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
    #endregion

#region Running
    [PunRPC]
    public void SetRunning()
     {
          if ((multiplayerController.movement.horizontal != 0 ||
               multiplayerController.movement.vertical != 0) &&
               multiplayerController.movement.currentSpeed > 0f &&
               multiplayerController.movement.isGrounded &&
               !balance &&
               multiplayerController.movement.controller.velocity.y <= 0 &&
               PlayerAttackControllerMultiplayer.instance.currentAttack == 0 &&
               !PlayerAttackControllerMultiplayer.instance.attaking &&
               multiplayerController.movement.canMove &&
               !multiplayerController.death.dead && 
               !multiplayerController.levelMechanics.entryTeleport)
          {
               animator.SetBool("Idle", false);
               animator.SetBool("Running", true);
               animator.SetBool("Single Jump", false);
               animator.SetBool("Single Jump Running", false);
               animator.SetBool("Double Jump", false);
               animator.SetBool("Pushing", false);
               animator.SetBool("Pushing Idle", false);
               animator.SetBool("Drop Box", false);
               animator.SetBool("Dying", false);
               animator.SetBool("Balance", false);
               animator.SetBool("Falling Action", false);
               animator.SetBool("Falling Idle", false);
               animator.SetBool("Falling Ground", false);
               animator.SetBool("Falling Running", false);
               animator.SetBool("Sliding", false);   
               animator.SetBool("Interacting", false);   
               animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false; 
          }
     }
#endregion

#region Jump
     public void SetSingleJump()
     {
          if (multiplayerController.movement.velocity.y > -2 &&
               (multiplayerController.movement.horizontal != 0 ||
               multiplayerController.movement.vertical != 0) &&
               multiplayerController.jump.currentJump == 1 &&
               !multiplayerController.movement.isGrounded &&
               !PlayerAttackControllerMultiplayer.instance.attaking &&
               !multiplayerController.death.dead && 
               !multiplayerController.levelMechanics.entryTeleport)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", false);
               multiplayerController.photon.animator.SetBool("Single Jump Running", true);
               multiplayerController.photon.animator.SetBool("Double Jump", false);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", false);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", false);
               multiplayerController.photon.animator.SetBool("Falling Idle", false);
               multiplayerController.photon.animator.SetBool("Falling Ground", false);
               multiplayerController.photon.animator.SetBool("Falling Running", false);
               multiplayerController.photon.animator.SetBool("Sliding", false);
               multiplayerController.photon.animator.SetBool("Interacting", false); 
               multiplayerController.photon.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }

     public void SetSingleJumpRunning()
     {
          if (multiplayerController.movement.velocity.y > -2 &&
              (multiplayerController.movement.horizontal == 0 &&
              multiplayerController.movement.vertical == 0) &&
              multiplayerController.jump.currentJump == 1 &&
              !multiplayerController.movement.isGrounded &&
              !PlayerAttackControllerMultiplayer.instance.attaking &&
               !multiplayerController.death.dead && 
               !multiplayerController.levelMechanics.entryTeleport)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", true);
               multiplayerController.photon.animator.SetBool("Single Jump Running", false);
               multiplayerController.photon.animator.SetBool("Double Jump", false);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", false);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", false);
               multiplayerController.photon.animator.SetBool("Falling Idle", false);
               multiplayerController.photon.animator.SetBool("Falling Ground", false);
               multiplayerController.photon.animator.SetBool("Falling Running", false);
               multiplayerController.photon.animator.SetBool("Sliding", false);
               multiplayerController.photon.animator.SetBool("Interacting", false); 
               multiplayerController.photon.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }

     public void SetDoubleJump()
     {
          if (multiplayerController.movement.controller.velocity.y > 0 &&
               multiplayerController.jump.currentJump >= 2 &&
              !multiplayerController.movement.isGrounded &&
              !PlayerAttackControllerMultiplayer.instance.attaking &&
               !multiplayerController.death.dead && 
               !multiplayerController.levelMechanics.entryTeleport)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", false);
               multiplayerController.photon.animator.SetBool("Single Jump Running", false);
               multiplayerController.photon.animator.SetBool("Double Jump", true);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", false);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", false);
               multiplayerController.photon.animator.SetBool("Falling Idle", false);
               multiplayerController.photon.animator.SetBool("Falling Ground", false);
               multiplayerController.photon.animator.SetBool("Falling Running", false);
               multiplayerController.photon.animator.SetBool("Sliding", false);
               multiplayerController.photon.animator.SetBool("Interacting", false); 
               multiplayerController.photon.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
#endregion

#region Falling
     public void ResetFallingAnimations()
     {
          multiplayerController.photon.animator.SetBool("Idle", true);
          multiplayerController.photon.animator.SetBool("Falling Action", false);
          multiplayerController.photon.animator.SetBool("Falling Idle", false);
          multiplayerController.photon.animator.SetBool("Falling Ground", false);
          multiplayerController.photon.animator.SetBool("Falling Running", false);
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }

     public void SetFallingAction()
     {
          if(multiplayerController.movement.controller.velocity.y <= -0.1 &&
              !multiplayerController.movement.isGrounded &&
              !PlayerAttackControllerMultiplayer.instance.attaking &&
              multiplayerController.jump.currentJump >= 2 &&
              !alreadyPlayedFallingAction &&
               !multiplayerController.death.dead && 
               !multiplayerController.levelMechanics.entryTeleport)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", false);
               multiplayerController.photon.animator.SetBool("Single Jump Running", false);
               multiplayerController.photon.animator.SetBool("Double Jump", false);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", false);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", true);
               multiplayerController.photon.animator.SetBool("Falling Idle", false);
               multiplayerController.photon.animator.SetBool("Falling Ground", false);
               multiplayerController.photon.animator.SetBool("Falling Running", false);  
               multiplayerController.photon.animator.SetBool("Sliding", false); 
               multiplayerController.photon.animator.SetBool("Interacting", false); 
               multiplayerController.photon.animator.SetBool("Power Up", false);
               _inFallingAction = true;                          
          }
     }

     public void SetFallingIdle()
     {
          if (multiplayerController.movement.controller.velocity.y <= -0.1 &&
              !multiplayerController.movement.isGrounded &&
              !PlayerAttackControllerMultiplayer.instance.attaking &&
              !alreadyPlayedFallingAction &&
              multiplayerController.jump.currentJump <= 1 &&
              !_inFallingAction &&
              !_firstAttack &&
              !_secondAttack &&
              !_finalAttack &&
               !multiplayerController.death.dead && 
               !multiplayerController.levelMechanics.entryTeleport)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", false);
               multiplayerController.photon.animator.SetBool("Single Jump Running", false);
               multiplayerController.photon.animator.SetBool("Double Jump", false);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", false);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", false);
               multiplayerController.photon.animator.SetBool("Falling Idle", true);
               multiplayerController.photon.animator.SetBool("Falling Ground", false);
               multiplayerController.photon.animator.SetBool("Falling Running", false);
               multiplayerController.photon.animator.SetBool("Sliding", false);
               multiplayerController.photon.animator.SetBool("Interacting", false); 
               multiplayerController.photon.animator.SetBool("Final Attack", false);
               multiplayerController.photon.animator.SetBool("Second Attack", false);
               multiplayerController.photon.animator.SetBool("First Attack", false);
               multiplayerController.photon.animator.SetBool("Power Up", false);

               if (!fallingIdle)
               {
                    fallingIdle = true;                    
               }
          }
     }

     public void SetFallingIdleDoubleJump()
     {
          if (multiplayerController.movement.controller.velocity.y <= -0.1 &&
              !multiplayerController.movement.isGrounded &&
              !PlayerAttackControllerMultiplayer.instance.attaking &&
              multiplayerController.jump.currentJump >= 2 &&
              alreadyPlayedFallingAction &&
               !multiplayerController.death.dead && 
               !multiplayerController.levelMechanics.entryTeleport)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", false);
               multiplayerController.photon.animator.SetBool("Single Jump Running", false);
               multiplayerController.photon.animator.SetBool("Double Jump", false);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", false);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", false);
               multiplayerController.photon.animator.SetBool("Falling Idle", true);
               multiplayerController.photon.animator.SetBool("Falling Ground", false);
               multiplayerController.photon.animator.SetBool("Falling Running", false);
               multiplayerController.photon.animator.SetBool("Sliding", false);
               multiplayerController.photon.animator.SetBool("Interacting", false); 
               multiplayerController.photon.animator.SetBool("Power Up", false);
               
               if (!fallingIdle)
               {
                    fallingIdle = true;
               }
          }
     }

     public void SetFallingGround()
     {
          if ((multiplayerController.movement.horizontal == 0 &&
              multiplayerController.movement.vertical == 0) &&
              multiplayerController.movement.isGrounded &&
              !PlayerAttackControllerMultiplayer.instance.attaking &&
              multiplayerController.jump.currentJump >= 2 &&
               !multiplayerController.death.dead && 
               !multiplayerController.levelMechanics.entryTeleport)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", false);
               multiplayerController.photon.animator.SetBool("Single Jump Running", false);
               multiplayerController.photon.animator.SetBool("Double Jump", false);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", false);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", false);
               multiplayerController.photon.animator.SetBool("Falling Idle", false);
               multiplayerController.photon.animator.SetBool("Falling Ground", true);
               multiplayerController.photon.animator.SetBool("Falling Running", false);
               multiplayerController.photon.animator.SetBool("Sliding", false);
               multiplayerController.photon.animator.SetBool("Interacting", false); 
               multiplayerController.photon.animator.SetBool("Power Up", false);
               _canJumpAfterFalling = false;
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
               multiplayerController.jump.fallingDust.Play();
          }
     }

     public void SetFallingRunning()
     {
          if ((multiplayerController.movement.horizontal != 0 ||
               multiplayerController.movement.vertical != 0) &&
               multiplayerController.movement.isGrounded &&
               !PlayerAttackControllerMultiplayer.instance.attaking &&
               multiplayerController.jump.currentJump >= 2 &&
               !multiplayerController.death.dead && 
               !multiplayerController.levelMechanics.entryTeleport)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", false);
               multiplayerController.photon.animator.SetBool("Single Jump Running", false);
               multiplayerController.photon.animator.SetBool("Double Jump", false);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", false);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", false);
               multiplayerController.photon.animator.SetBool("Falling Idle", false);
               multiplayerController.photon.animator.SetBool("Falling Ground", false);
               multiplayerController.photon.animator.SetBool("Falling Running", true);
               multiplayerController.photon.animator.SetBool("Sliding", false);
               multiplayerController.photon.animator.SetBool("Interacting", false); 
               multiplayerController.photon.animator.SetBool("Power Up", false);
               _canJumpAfterFalling = false;
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
               multiplayerController.jump.fallingDust.Play();
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
          multiplayerController.photon.animator.SetBool("Idle", false);
          multiplayerController.photon.animator.SetBool("Running", false);
          multiplayerController.photon.animator.SetBool("Single Jump", false);
          multiplayerController.photon.animator.SetBool("Single Jump Running", false);
          multiplayerController.photon.animator.SetBool("Double Jump", false);
          multiplayerController.photon.animator.SetBool("Pushing", false);
          multiplayerController.photon.animator.SetBool("Pushing Idle", false);
          multiplayerController.photon.animator.SetBool("Drop Box", false);
          multiplayerController.photon.animator.SetBool("Dying", false);
          multiplayerController.photon.animator.SetBool("Balance", true);
          multiplayerController.photon.animator.SetBool("Falling Action", false);
          multiplayerController.photon.animator.SetBool("Falling Idle", false);
          multiplayerController.photon.animator.SetBool("Falling Ground", false);
          multiplayerController.photon.animator.SetBool("Falling Running", false);
          multiplayerController.photon.animator.SetBool("Sliding", false);
          multiplayerController.photon.animator.SetBool("Interacting", false); 
          multiplayerController.photon.animator.SetBool("Power Up", false);
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
          if(multiplayerController.levelMechanics.sliding)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", false);
               multiplayerController.photon.animator.SetBool("Single Jump Running", false);
               multiplayerController.photon.animator.SetBool("Double Jump", false);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", false);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", false);
               multiplayerController.photon.animator.SetBool("Falling Idle", false);
               multiplayerController.photon.animator.SetBool("Falling Ground", false);
               multiplayerController.photon.animator.SetBool("Falling Running", false);
               multiplayerController.photon.animator.SetBool("Sliding", true);
               multiplayerController.photon.animator.SetBool("Interacting", false); 
               multiplayerController.photon.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;                            
          }
     }
#endregion

#region Teleport
     public void SetEntryTeleport()
     {
          if(multiplayerController.levelMechanics.entryTeleport)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", false);
               multiplayerController.photon.animator.SetBool("Single Jump Running", false);
               multiplayerController.photon.animator.SetBool("Double Jump", false);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", false);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", false);
               multiplayerController.photon.animator.SetBool("Falling Idle", false);
               multiplayerController.photon.animator.SetBool("Falling Ground", false);
               multiplayerController.photon.animator.SetBool("Falling Running", false);
               multiplayerController.photon.animator.SetBool("Sliding", false);
               multiplayerController.photon.animator.SetBool("Entry Teleport", true);
               multiplayerController.photon.animator.SetBool("Exit Teleport", false);
               multiplayerController.photon.animator.SetBool("Interacting", false); 
               multiplayerController.photon.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }

     public void SetExitTeleport()
     {
          if(multiplayerController.levelMechanics.exitTeleport)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", false);
               multiplayerController.photon.animator.SetBool("Single Jump Running", false);
               multiplayerController.photon.animator.SetBool("Double Jump", false);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", false);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", false);
               multiplayerController.photon.animator.SetBool("Falling Idle", false);
               multiplayerController.photon.animator.SetBool("Falling Ground", false);
               multiplayerController.photon.animator.SetBool("Falling Running", false);
               multiplayerController.photon.animator.SetBool("Sliding", false);
               multiplayerController.photon.animator.SetBool("Entry Teleport", false);
               multiplayerController.photon.animator.SetBool("Exit Teleport", true);
               multiplayerController.photon.animator.SetBool("Interacting", false); 
               multiplayerController.photon.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
#endregion

#region Interact
     public void SetInteract()
     {
          if(multiplayerController.levelMechanics.interacting)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", false);
               multiplayerController.photon.animator.SetBool("Single Jump Running", false);
               multiplayerController.photon.animator.SetBool("Double Jump", false);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", false);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", false);
               multiplayerController.photon.animator.SetBool("Falling Idle", false);
               multiplayerController.photon.animator.SetBool("Falling Ground", false);
               multiplayerController.photon.animator.SetBool("Falling Running", false);
               multiplayerController.photon.animator.SetBool("Sliding", false);
               multiplayerController.photon.animator.SetBool("Entry Teleport", false);
               multiplayerController.photon.animator.SetBool("Exit Teleport", false);
               multiplayerController.photon.animator.SetBool("Interacting", true);     
               multiplayerController.photon.animator.SetBool("Power Up", false);          
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
          multiplayerController.photon.animator.SetBool("Idle", false);
          multiplayerController.photon.animator.SetBool("Running", false);
          multiplayerController.photon.animator.SetBool("Single Jump", false);
          multiplayerController.photon.animator.SetBool("Single Jump Running", false);
          multiplayerController.photon.animator.SetBool("Double Jump", false);
          multiplayerController.photon.animator.SetBool("Pushing", false);
          multiplayerController.photon.animator.SetBool("Pushing Idle", false);
          multiplayerController.photon.animator.SetBool("Drop Box", false);
          multiplayerController.photon.animator.SetBool("Dying", false);
          multiplayerController.photon.animator.SetBool("Balance", false);
          multiplayerController.photon.animator.SetBool("Falling Action", false);
          multiplayerController.photon.animator.SetBool("Falling Idle", false);
          multiplayerController.photon.animator.SetBool("Falling Ground", false);
          multiplayerController.photon.animator.SetBool("Falling Running", false);
          multiplayerController.photon.animator.SetBool("Sliding", false);
          multiplayerController.photon.animator.SetBool("Entry Teleport", false);
          multiplayerController.photon.animator.SetBool("Exit Teleport", false);
          multiplayerController.photon.animator.SetBool("Interacting", false);     
          multiplayerController.photon.animator.SetBool("Power Up", true); 
          fallingIdle = false;
          alreadyPlayedFallingAction = false;
     }
#endregion

#region Dead
     public void SetDead()
     {
          if (multiplayerController.death.dead)
          {
               multiplayerController.photon.animator.SetBool("Idle", false);
               multiplayerController.photon.animator.SetBool("Running", false);
               multiplayerController.photon.animator.SetBool("Single Jump", false);
               multiplayerController.photon.animator.SetBool("Single Jump Running", false);
               multiplayerController.photon.animator.SetBool("Double Jump", false);
               multiplayerController.photon.animator.SetBool("Pushing", false);
               multiplayerController.photon.animator.SetBool("Pushing Idle", false);
               multiplayerController.photon.animator.SetBool("Drop Box", false);
               multiplayerController.photon.animator.SetBool("Dying", true);
               multiplayerController.photon.animator.SetBool("Balance", false);
               multiplayerController.photon.animator.SetBool("Falling Action", false);
               multiplayerController.photon.animator.SetBool("Falling Idle", false);
               multiplayerController.photon.animator.SetBool("Falling Ground", false);
               multiplayerController.photon.animator.SetBool("Falling Running", false);
               multiplayerController.photon.animator.SetBool("Sliding", false);
               multiplayerController.photon.animator.SetBool("Interacting", false); 
               multiplayerController.photon.animator.SetBool("Power Up", false);
               fallingIdle = false;
               alreadyPlayedFallingAction = false;
          }
     }
#endregion
}
