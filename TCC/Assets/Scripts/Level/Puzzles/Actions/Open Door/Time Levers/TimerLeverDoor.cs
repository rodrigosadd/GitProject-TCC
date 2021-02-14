using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerLeverDoor : OpenDoor
{
     public Lever[] levers;
     public float timeToCloseDoor;
     private float _countdownToCloseDoor;
     private bool _canCloseTheDoor;

     void Start()
     {
          StartPositionTargets();
     }

     void Update()
     {
          CheckLevers();
          CanCloseDoor();
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
               CanOpenDoor();
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

     public override void CanCloseDoor()
     {
          if (_canCloseTheDoor)
          {
               doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetInitialLPos, speedMoveDoor * Time.deltaTime);
               doorRight.position = Vector3.MoveTowards(doorRight.position, targetInitialRPos, speedMoveDoor * Time.deltaTime);
          }
     }
}