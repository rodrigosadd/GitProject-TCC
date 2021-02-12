using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerLever : MonoBehaviour
{
     public Lever[] levers;
     public Transform doorLeft;
     public Transform targetMoveLeft;
     public Transform doorRight;
     public Transform targetMoveRight;
     public float speedMoveDoor;
     public float timeToCloseDoor;
     private Vector3 _targetInitialLPos;
     private Vector3 _targetInitialRPos;
     private float _countdownToCloseDoor;
     private bool _canCloseTheDoor;

     void Start()
     {
          _targetInitialLPos = new Vector3(doorLeft.position.x, doorLeft.position.y, doorLeft.position.z);
          _targetInitialRPos = new Vector3(doorRight.position.x, doorRight.position.y, doorRight.position.z);
     }

     void Update()
     {
          CheckLevers();
          CloseDoor();
     }

     public void CheckLevers()
     {
          bool _isComplete = true;

          for (int i = 0; i < levers.Length; i++)
          {
               if (!levers[i].triggerLever)
               {
                    _isComplete = false;
                    break;
               }
          }

          if (_isComplete)
          {
               _canCloseTheDoor = false;
               OpenDoor();
               CountdownCloseDoor();
          }
     }

     public void CountdownCloseDoor()
     {
          if (!_canCloseTheDoor)
          {
               if (_countdownToCloseDoor < 1)
               {
                    _countdownToCloseDoor += Time.deltaTime / timeToCloseDoor;
               }
               else
               {
                    _countdownToCloseDoor = 0;
                    _canCloseTheDoor = true;

                    for (int i = 0; i < levers.Length; i++)
                    {
                         levers[i].triggerLever = false;
                    }
               }
          }
     }

     public void OpenDoor()
     {
          doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetMoveLeft.position, speedMoveDoor * Time.deltaTime);
          doorRight.position = Vector3.MoveTowards(doorRight.position, targetMoveRight.position, speedMoveDoor * Time.deltaTime);
     }

     public void CloseDoor()
     {
          if (_canCloseTheDoor)
          {
               doorLeft.position = Vector3.MoveTowards(doorLeft.position, _targetInitialLPos, speedMoveDoor * Time.deltaTime);
               doorRight.position = Vector3.MoveTowards(doorRight.position, _targetInitialRPos, speedMoveDoor * Time.deltaTime);
          }
     }
}