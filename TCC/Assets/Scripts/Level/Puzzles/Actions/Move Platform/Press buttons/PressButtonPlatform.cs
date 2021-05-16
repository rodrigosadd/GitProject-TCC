using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButtonPlatform : MonoBehaviour
{
     public Camera3rdPerson camera3RdPerson;
     public Transform targetCam;
     public MovePlatform[] platforms;
     public Button[] buttons;
     public float timeToReturnPlayerTarget = 2f;
     public bool seeObject;
     private float countdownToReturnPlayerTarget;
     private bool canChangeTargetCam; 

     void Update()
     {
          CheckTriggerButtons();
          CountdownToReturnPlayerTarget();
     }

     public void CheckTriggerButtons()
     {
          bool _isComplete = true;

          for (int i = 0; i < buttons.Length; i++)
          {
               if (!buttons[i].triggerButton)
               {
                    _isComplete = false;
                    break;
               }
          }

          if (_isComplete)
          {
               MovePlatforms();
               SeeObjectDrop();
          }
     }

     public void MovePlatforms()
     {
          for (int i = 0; i < platforms.Length; i++)
          {
               platforms[i].Initialize();
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