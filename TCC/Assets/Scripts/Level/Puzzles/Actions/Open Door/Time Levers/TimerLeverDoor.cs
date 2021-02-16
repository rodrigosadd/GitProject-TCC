using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerLeverDoor : OpenDoor
{
     public Lever[] levers;

     void Start()
     {
          StartPositionTargets();
     }

     void Update()
     {
          CheckLevers();
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
               CanOpenDoor();
          }
          else
          {
               CanCloseDoor();
          }
     }
}