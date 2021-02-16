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
     private float _countdownExitTeleport;
     public bool _CanTeleport;
     public bool _CanMove;

     void Start()
     {
          SetTargetCamPosition();
     }

     void Update()
     {
          PlayerTeleport();
          CountdownEntryTeleport();
          CountdownExitTeleport();
     }

     public void PlayerTeleport()
     {
          _distanceBetween = Vector3.Distance(entrancePortal.position, PlayerController.instance.transform.position);

          if (_distanceBetween <= rangeTeleport)
          {
               _CanTeleport = true;
          }
     }

     public void CountdownEntryTeleport()
     {
          if (_CanTeleport)
          {
               if (_countdownEntryTeleport < 1)
               {
                    PlayerConfigsEntryTeleport();
                    _countdownEntryTeleport += Time.deltaTime / timeEntryTeleport;
               }
               else
               {
                    _countdownEntryTeleport = 0;
                    _CanTeleport = false;
                    _CanMove = false;
                    MovePlayerToPortalExit();
                    SetRotation();
               }
          }
     }

     public void CountdownExitTeleport()
     {
          if (!_CanMove)
          {
               if (_countdownExitTeleport < 1)
               {
                    _countdownExitTeleport += Time.deltaTime / timeExitTeleport;
               }
               else
               {
                    _CanMove = true;
                    _countdownExitTeleport = 0;
                    PlayerConfigsExitTeleport();
               }
          }
     }

     public void MovePlayerToPortalExit()
     {
          PlayerController.instance.transform.position = exitPortal.position + exitPortal.forward;
     }

     public void SetRotation()
     {
          PlayerController.instance.characterGraphic.transform.rotation = Quaternion.FromToRotation(PlayerController.instance.characterGraphic.transform.forward, exitPortal.forward);
     }

     public void PlayerConfigsEntryTeleport()
     {
          camera3RdPerson.targetCamera = targetCameraExitPortal;
          targetCameraExitPortal.position = Vector3.MoveTowards(targetCameraExitPortal.position, exitPortal.position, cameraVelocity * Time.deltaTime);
          PlayerController.instance.movement.rbody.constraints = RigidbodyConstraints.FreezePosition;
          PlayerController.instance.movement.maxSpeed = 0;
          PlayerController.instance.movement.teleporting = true;
     }

     public void PlayerConfigsExitTeleport()
     {
          camera3RdPerson.targetCamera = targetCameraPlayer;
          PlayerController.instance.movement.rbody.constraints = RigidbodyConstraints.FreezeRotation;
          PlayerController.instance.jump.currentJump = 0;
          PlayerController.instance.jump.doubleJumpCountdown = 0;
          PlayerController.instance.movement.maxSpeed = PlayerController.instance.movement.fixedMaxSpeed;
          PlayerController.instance.movement.teleporting = false;
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
