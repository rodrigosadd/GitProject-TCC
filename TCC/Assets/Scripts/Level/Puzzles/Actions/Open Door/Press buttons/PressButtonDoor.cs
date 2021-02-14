using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButtonDoor : OpenDoor
{
     public Button[] buttons;

     void Start()
     {
          StartPositionTargets();
     }

     void Update()
     {
          CheckTriggerButtons();
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
               CanOpenDoor();
          }
     }
}