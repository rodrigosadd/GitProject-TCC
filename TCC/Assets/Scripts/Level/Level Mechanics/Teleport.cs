using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
     public Camera3rdPerson camera3RdPerson;
     public Transform targetCameraPlayer;
     public Transform targetCameraExitPortal;
     public Transform entrancePortal;
     public Transform exitPortal;
     public float rangeTeleport;
     public float timeEntryTeleport;
     public float timeExitTeleport;
     public float cameraVelocity;
     public bool seeRangeTeleport;
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
     }

     void Update()
     {
          PlayerTeleport();
          CountdownEntryTeleport();
          CountdownMoveCamera();
          CountdownExitTeleport();
     }

     public void PlayerTeleport()
     {
          _distanceBetween = Vector3.Distance(entrancePortal.position, PlayerController.instance.transform.position);

          if (_distanceBetween <= rangeTeleport)
          {
               _canTeleport = true;               
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
                    PlayerController.instance.movement.entryTeleport = true;
                    _countdownEntryTeleport += Time.deltaTime / timeEntryTeleport;
               }
               else
               {
                    PlayerController.instance.movement.entryTeleport = false;
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
                    PlayerController.instance.movement.exitTeleport = true;
                    _countdownExitTeleport += Time.deltaTime / timeExitTeleport;
               }
               else
               {
                    PlayerController.instance.movement.exitTeleport = false;
                    _canMove = true;
                    _countdownExitTeleport = 0;
                    PlayerConfigsExitTeleport();
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
