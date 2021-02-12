using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingObjects : MonoBehaviour
{
     public BreakableObject[] breakableObjects;
     public Transform doorLeft;
     public Transform targetMoveLeft;
     public Transform doorRight;
     public Transform targetMoveRight;
     public float speedMoveDoor;
     private Vector3 _targetInitialLPos;
     private Vector3 _targetInitialRPos;

     void Start()
     {
          _targetInitialLPos = new Vector3(doorLeft.position.x, doorLeft.position.y, doorLeft.position.z);
          _targetInitialRPos = new Vector3(doorRight.position.x, doorRight.position.y, doorRight.position.z);
     }

     void Update()
     {
          CheckObjectsBroken();
     }

     public void CheckObjectsBroken()
     {
          bool _isComplete = true;

          for (int i = 0; i < breakableObjects.Length; i++)
          {
               if (!breakableObjects[i].triggerBroken)
               {
                    _isComplete = false;
                    break;
               }
          }

          if (_isComplete)
          {
               OpenDoor();
          }
          else
          {
               CloseDoor();
          }
     }

     public void OpenDoor()
     {
          doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetMoveLeft.position, speedMoveDoor * Time.deltaTime);
          doorRight.position = Vector3.MoveTowards(doorRight.position, targetMoveRight.position, speedMoveDoor * Time.deltaTime);
     }

     public void CloseDoor()
     {
          doorLeft.position = Vector3.MoveTowards(doorLeft.position, _targetInitialLPos, speedMoveDoor * Time.deltaTime);
          doorRight.position = Vector3.MoveTowards(doorRight.position, _targetInitialRPos, speedMoveDoor * Time.deltaTime);
     }
}
