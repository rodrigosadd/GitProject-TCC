using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressWeightsButtonsPlatform : MonoBehaviour
{
     public Camera3rdPerson camera3RdPerson;
     public Transform targetCam;
     public MovePlatform[] platforms;
     public WeightButton[] weightButtons;
     public float timeToReturnPlayerTarget = 2f;
     public bool seeObject;
     private float countdownToReturnPlayerTarget;
     private bool canChangeTargetCam; 

     void Update()
     {
          CheckPressButtons();
          CountdownToReturnPlayerTarget();
     }

     public void CheckPressButtons()
     {
          bool isComplete = true;

          for (int i = 0; i < weightButtons.Length; i++)
          {
               if (!weightButtons[i].rightWeight)
               {
                    isComplete = false;
                    break;
               }
          }

          if (isComplete)
          {
               MovePlatforms();
               SeeObjectDrop();
          }
          else
          {
               StopMovePlatforms();
          }
     }

     public void MovePlatforms()
     {
          for (int i = 0; i < platforms.Length; i++)
          {
               platforms[i].Initialize();               
          }
     }

     public void StopMovePlatforms()
     {
          for (int i = 0; i < platforms.Length; i++)
          {
               platforms[i].canMove = false;
               platforms[i].hasRestarted = false;
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
