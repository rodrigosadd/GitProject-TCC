using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Teleport : MonoBehaviour
{
     public Camera3rdPerson camera3RdPerson;
     public Transform targetCameraPlayer;
     public Transform targetCameraExitPortal;
     public GameObject entrancePortalDeactive;
     public GameObject exitPortalDeactive;
     public Transform entrancePortal;
     public Transform exitPortal;
     public float rangeTeleport;
     public float timeEntryTeleport;
     public float timeExitTeleport;
     public float cameraVelocity;
     public bool seeRangeTeleport;
     [EventRef]
     public string teleportSound;
     private float _distanceBetween;
     private float _countdownEntryTeleport;
     private float _countdownMoveCamera;
     private float _countdownExitTeleport;
     private bool _canTeleport;
     private bool _canMoveCamera = true;
     private bool _canMove = true;

     void Start()
     {
          SetTargetCamPosition();
          entrancePortalDeactive.transform.position = entrancePortal.position;
          exitPortalDeactive.transform.position = exitPortal.position;
     }

     void Update()
     {
          CheckPlayerCanToSeeTeleport();

          if(PlayerController.instance.levelMechanics.canSeeTeleport)
          {
               PlayerTeleport();
               CountdownEntryTeleport();
               CountdownMoveCamera();
               CountdownExitTeleport();
          }
     }

     public void CheckPlayerCanToSeeTeleport()
     {
          if(PlayerController.instance.levelMechanics.canSeeTeleport)
          {
               entrancePortal.gameObject.SetActive(true);
               exitPortal.gameObject.SetActive(true);
               entrancePortalDeactive.SetActive(false);
               exitPortalDeactive.SetActive(false);
          }
          else
          {
               entrancePortal.gameObject.SetActive(false);
               exitPortal.gameObject.SetActive(false);
               entrancePortalDeactive.SetActive(true);
               exitPortalDeactive.SetActive(true);
          }
     }

     public void PlayerTeleport()
     {
          _distanceBetween = Vector3.Distance(entrancePortal.position, PlayerController.instance.transform.position);

          if (_distanceBetween <= rangeTeleport)
          {
               _canTeleport = true;
               PlayerAnimationController.instance.SetEntryTeleport();               
               RuntimeManager.PlayOneShot(teleportSound, PlayerController.instance.transform.position);
          }
     }

     public void CountdownEntryTeleport()
     {
          if (_canTeleport)
          {
               if (_countdownEntryTeleport < 1)
               {
                    if(_canMoveCamera)
                    {
                         _canMoveCamera = false;
                    }

                    PlayerConfigsEntryTeleport();
                    PlayerController.instance.levelMechanics.entryTeleport = true;
                    _countdownEntryTeleport += Time.deltaTime / timeEntryTeleport;
               }
               else
               {
                    PlayerController.instance.levelMechanics.entryTeleport = false;
                    _countdownEntryTeleport = 0;
                    _canTeleport = false;
                    _canMove = false;
                    MovePlayerToPortalExit();
                    SetRotation();
               }
          }
     }

     public void CountdownMoveCamera()
     {
          if(!_canMoveCamera)
          {
               if(_countdownMoveCamera < 1)
               {
                    _countdownMoveCamera += Time.deltaTime / 0.35f;
               }
               else
               {                    
                    MoveCamera();                     
               }
          }
     }

     public void CountdownExitTeleport()
     {
          if (!_canMove)
          {
               if (_countdownExitTeleport < 1)
               {
                    PlayerController.instance.levelMechanics.exitTeleport = true;
                    PlayerController.instance.death.canSetAppearShader = true;
                    PlayerController.instance.death.canSetDisappearShader = false;
                    _countdownExitTeleport += Time.deltaTime / timeExitTeleport;
               }
               else
               {
                    PlayerController.instance.levelMechanics.exitTeleport = false;
                    _canMove = true;
                    _countdownExitTeleport = 0;
                    PlayerConfigsExitTeleport();
                    PlayerController.instance.death.canSetAppearShader = false;
                    PlayerController.instance.death.canSetDisappearShader = false;
               }
          }
     }

     public void MovePlayerToPortalExit()
     {
          PlayerController.instance.SetControllerPosition(exitPortal.position + exitPortal.forward);
     }

     public void SetRotation()
     {
          PlayerController.instance.characterGraphic.transform.rotation = exitPortal.rotation;
     }

     public void PlayerConfigsEntryTeleport()
     {
          camera3RdPerson.targetCamera = targetCameraExitPortal;          
          PlayerController.instance.movement.gravity = 0;
          PlayerController.instance.movement.velocity = Vector3.zero;
          PlayerController.instance.movement.maxSpeed = 0; 
          PlayerController.instance.death.canSetDisappearShader = true;
     }

     public void MoveCamera()
     {
          targetCameraExitPortal.position = Vector3.MoveTowards(targetCameraExitPortal.position, exitPortal.position, cameraVelocity * Time.deltaTime);
     }

     public void PlayerConfigsExitTeleport()
     {
          camera3RdPerson.targetCamera = targetCameraPlayer;
          PlayerController.instance.movement.gravity = PlayerController.instance.movement.fixedGravity;
          PlayerController.instance.jump.currentJump = 0;
          PlayerController.instance.jump.doubleJumpCountdown = 0;
          PlayerController.instance.movement.maxSpeed = PlayerController.instance.movement.fixedMaxSpeed;   
          _canMoveCamera = true;       
          _countdownMoveCamera = 0;
          SetTargetCamPosition();
     }

     public void SetTargetCamPosition()
     {
          targetCameraExitPortal.position = entrancePortal.position;
     }

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          if (seeRangeTeleport)
          {
               Gizmos.color = Color.magenta;
               Gizmos.DrawWireSphere(entrancePortal.position, rangeTeleport);
          }
     }
#endif
}
