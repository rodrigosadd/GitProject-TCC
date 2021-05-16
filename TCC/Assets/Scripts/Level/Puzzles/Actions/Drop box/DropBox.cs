using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
     public Camera3rdPerson camera3RdPerson;
     public Transform targetCam;
     public DropBoxType type;
     public GameObject[] objects;
     public Transform[] targets;
     public Button[] buttons;
     public GameObject objThatWillBeActivated;
     public float timeToReturnPlayerTarget = 2f;
     public bool seeObject;
     public bool activateObject;
     private bool _canSpawn = true;
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
               if (!buttons[i].triggerButton)
               {
                    _isComplete = false;
                    break;
               }
          }

          if(type == DropBoxType.ALWAYS_DROP)
          {               
               if (_isComplete)
               {
                    ActiveAllObjetcs();   
                    SeeObjectDrop(); 
                    ActivateObject();                              
               }
          }

          if(type == DropBoxType.DROP_ONCE)
          {               
               if (_isComplete && _canSpawn)
               {
                    ActiveAllObjetcs();
                    SeeObjectDrop();
                    ActivateObject(); 
                    _canSpawn = false;               
               }
          }
     }

     public void ActiveAllObjetcs()
     {
          for (int i = 0; i < objects.Length; i++)
          {
               objects[i].SetActive(true);
               objects[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
               objects[i].transform.position = targets[i].position;
          }
     }
     
     public void SeeObjectDrop()
     {
          if(seeObject)
          {
               canChangeTargetCam = true;
               camera3RdPerson.targetCamera = targetCam;
               PlayerController.instance.movement.canMove = false;
          }
     }

     public void ActivateObject()
     {
          if(activateObject)
          {
               objThatWillBeActivated.SetActive(true);
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
                    PlayerController.instance.movement.canMove = true;
                    seeObject = false;
               }
          }
     }
}
