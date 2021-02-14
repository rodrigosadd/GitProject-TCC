using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnTopButtonDoor : OpenDoor
{
     public StayButton[] stayButton;

     void Start()
     {
          StartPositionTargets();
     }

     void Update()
     {
          CheckStayButtons();
     }

     public void CheckStayButtons()
     {
          bool _isComplete = true;

          for (int i = 0; i < stayButton.Length; i++)
          {
               if (!stayButton[i].triggerButton)
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
