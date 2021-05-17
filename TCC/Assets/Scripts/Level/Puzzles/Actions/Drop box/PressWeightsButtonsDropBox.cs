using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressWeightsButtonsDropBox : MonoBehaviour
{
     public Camera3rdPerson camera3RdPerson;
     public Transform targetCam;
     public GameObject[] objects;
     public Transform[] targets;
     public WeightButton[] buttons;
     public float timeToReturnPlayerTarget = 2f;
     public bool seeObject;
     private bool _canDrop = true;
     private float countdownToReturnPlayerTarget;
     private bool canChangeTargetCam; 

     void Update()
     {
          CheckTriggers();
          CountdownToReturnPlayerTarget();
     }

     public void CheckTriggers()
     {
          bool _isComplete = true;

          for (int i = 0; i < buttons.Length; i++)
          {
               if (!buttons[i].rightWeight)
               {
                    _isComplete = false;
                    break;
               }
          }

          if (_isComplete)
          {
               ActiveObjetcs();
               SeeObjectDrop();
               _canDrop = false;
          }
     }

     public void ActiveObjetcs()
     {
          if (_canDrop)
          {
               for (int i = 0; i < objects.Length; i++)
               {
                    objects[i].SetActive(true);
                    objects[i].transform.position = targets[i].position;
               }
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
