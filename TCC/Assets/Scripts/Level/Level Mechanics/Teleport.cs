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
     public float delayToSetPositionCam;
     public bool seeRangeTeleport;
     [EventRef]
     public string teleportSound;
     public float speedHeadCutoffHeightDisappear;
     public float speedHeadCutoffHeightAppear;
     public float speedBodyCutoffHeightDisappear;
     public float speedBodyCutoffHeightAppear;
     public float speedPickaxeCutoffHeightDisappear;
     public float speedPickaxeCutoffHeightAppear;
     private float _headCutoffHeight;
     private float _bodyCutoffHeight;
     private float _pickaxeCutoffHeight;
     private float _distanceBetween;
     private float _countdownMoveCamera;
     private bool _canTeleport = true;
     private bool _canMoveCamera;
     private bool _canSetAppearShader;
     private bool _canSetDisappearShader;

     void Start()
     {
          SetTargetCamPosition();
          PlayerController.instance.ResetValueDissolveShader();
          entrancePortalDeactive.transform.position = entrancePortal.position;
          exitPortalDeactive.transform.position = exitPortal.position;
     }

     void Update()
     {
          CheckPlayerCanToSeeTeleport();

          if(PlayerController.instance.levelMechanics.canSeeTeleport)
          {
               PlayerTeleport();
               MoveCamera();
               SetDissolveShaderAppear();
               SetDissolveShaderDisappear();
          }
     }

     public void PlayerTeleport()
     {
          _distanceBetween = Vector3.Distance(entrancePortal.position, PlayerController.instance.transform.position);

          if (_distanceBetween <= rangeTeleport && _canTeleport)
          {            
               _canTeleport = false;
               StartCoroutine("StartTeleport");
          }
     }

     IEnumerator StartTeleport()
     {
          RuntimeManager.PlayOneShot(teleportSound, PlayerController.instance.transform.position);
          PlayerConfigsEntryTeleport();
          _canSetDisappearShader = true;
          PlayerController.instance.levelMechanics.entryTeleport = true;
          PlayerController.instance.levelMechanics.exitTeleport = false;
          StartCoroutine("DelayToSetPositionCam");

          yield return new WaitForSeconds(timeEntryTeleport);

          MovePlayerToPortalExit();
          SetRotation();
          _canSetAppearShader = true;
          _canSetDisappearShader = false;

          PlayerController.instance.levelMechanics.entryTeleport = false;
          PlayerController.instance.levelMechanics.exitTeleport = true;

          yield return new WaitForSeconds(timeExitTeleport);
          
          PlayerConfigsExitTeleport();
          PlayerController.instance.ResetValueDissolveShader();
          PlayerController.instance.levelMechanics.exitTeleport = false;
          _canSetAppearShader = false;
          _canSetDisappearShader = false;
          _canTeleport = true;
          _canMoveCamera = false;
     }

     IEnumerator DelayToSetPositionCam()
     {
          yield return new WaitForSeconds(delayToSetPositionCam);
          _canMoveCamera = true;
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

     public void PlayerConfigsExitTeleport()
     {
          camera3RdPerson.targetCamera = targetCameraPlayer;
          PlayerController.instance.movement.gravity = PlayerController.instance.movement.fixedGravity;
          PlayerController.instance.jump.currentJump = 0;
          PlayerController.instance.jump.doubleJumpCountdown = 0;
          PlayerController.instance.movement.maxSpeed = PlayerController.instance.movement.fixedMaxSpeed;         
          SetTargetCamPosition();
     }

     public void MoveCamera()
     {
          if(_canMoveCamera)
          {
               targetCameraExitPortal.position = Vector3.MoveTowards(targetCameraExitPortal.position, exitPortal.position, cameraVelocity * Time.deltaTime);
          }
     }

     public void SetTargetCamPosition()
     {
          targetCameraExitPortal.position = entrancePortal.position;
     }

     public void SetDissolveShaderAppear()
     {
          if(_canSetAppearShader)
          {
               _headCutoffHeight -= Time.deltaTime * speedHeadCutoffHeightAppear ;
               _headCutoffHeight = Mathf.Clamp(_headCutoffHeight, -1f, 5f);
               PlayerController.instance.death.head.SetFloat("_Cutoff_Height", _headCutoffHeight);

               _bodyCutoffHeight -= Time.deltaTime * speedBodyCutoffHeightAppear;
               _bodyCutoffHeight = Mathf.Clamp(_bodyCutoffHeight, -1f, 5f);
               PlayerController.instance.death.body.SetFloat("_Cutoff_Height", _bodyCutoffHeight);

               _pickaxeCutoffHeight -= Time.deltaTime * speedPickaxeCutoffHeightAppear;
               _pickaxeCutoffHeight = Mathf.Clamp(_pickaxeCutoffHeight, -1f, 5f);
               PlayerController.instance.death.pickaxe.SetFloat("_Cutoff_Height", _pickaxeCutoffHeight);
          }
     }

     public void SetDissolveShaderDisappear()
     {  
          if(_canSetDisappearShader)
          {
               _headCutoffHeight += Time.deltaTime * speedHeadCutoffHeightDisappear;
               _headCutoffHeight = Mathf.Clamp(_headCutoffHeight, -1f, 5f);
               PlayerController.instance.death.head.SetFloat("_Cutoff_Height", _headCutoffHeight);

               _bodyCutoffHeight += Time.deltaTime * speedBodyCutoffHeightDisappear;
               _bodyCutoffHeight = Mathf.Clamp(_bodyCutoffHeight, -1f, 5f);
               PlayerController.instance.death.body.SetFloat("_Cutoff_Height", _bodyCutoffHeight);

               _pickaxeCutoffHeight += Time.deltaTime * speedPickaxeCutoffHeightDisappear;
               _pickaxeCutoffHeight = Mathf.Clamp(_pickaxeCutoffHeight, -1f, 5f);
               PlayerController.instance.death.pickaxe.SetFloat("_Cutoff_Height", _pickaxeCutoffHeight);
          }
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
