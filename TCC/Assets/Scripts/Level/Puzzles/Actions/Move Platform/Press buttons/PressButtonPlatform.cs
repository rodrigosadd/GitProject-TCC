using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButtonPlatform : MonoBehaviour
{
     public MovePlatform[] platforms;
     public Button[] buttons;

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
               MovePlatforms();
          }
     }

     public void MovePlatforms()
     {
          for (int i = 0; i < platforms.Length; i++)
          {
               platforms[i].canMove = true;
          }
     }
}