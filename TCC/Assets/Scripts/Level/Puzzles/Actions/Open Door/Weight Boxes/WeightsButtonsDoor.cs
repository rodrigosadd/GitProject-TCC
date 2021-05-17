using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightsButtonsDoor : OpenDoor
{
     public Camera3rdPerson camera3RdPerson;
     public Transform targetCam;
     public WeightButton[] weightButtons;
     public float timeToReturnPlayerTarget = 2f;
     public bool seeObject;
     private float countdownToReturnPlayerTarget;
     private bool canChangeTargetCam; 

     void Start()
     {
          StartPositionTargets();
     }

     void Update()
     {
          CheckObjectsBroken();
          CountdownToReturnPlayerTarget();
     }

     public void CheckObjectsBroken()
     {
          bool _isComplete = true;

          for (int i = 0; i < weightButtons.Length; i++)
          {
               if (!weightButtons[i].rightWeight)
               {
                    _isComplete = false;
                    break;
               }
          }

          if (_isComplete)
          {
               CanOpenDoor();
               SeeObjectDrop();
          }
          else
          {
               CanCloseDoor();
          }
     }

     public void SeeObjectDrop()
     {
          if(seeObject)
          {
               canChangeTargetCam = true;
               camera3RdPerson.targetCamera = targetCam;
               camera3RdPerson.ConfigToShowObject();
               PlayerController.instance.movement.canMove = false;
          }
     }

     public void CountdownToReturnPlayerTarget()
     {
          if(canChangeTargetCam)
          {
               if(countdownToReturnPlayerTarget < 1)
               {
                    countdownToReturnPlayerTarget += Time.deltaTime / timeToReturnPlayerTarget;
               }
               else
               {
                    canChangeTargetCam = false;
                    camera3RdPerson.targetCamera = PlayerController.instance.movement.targetCam;
                    camera3RdPerson.ResetConfig();
                    PlayerController.instance.movement.canMove = true;
                    seeObject = false;
               }
          }
     }
}
