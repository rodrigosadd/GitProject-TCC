using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnTopButtonPlatform : MonoBehaviour
{
     public MovePlatform[] platforms;
     public StayButton[] stayButtons;

     void Update()
     {
          CheckTriggerButtons();
     }

     public void CheckTriggerButtons()
     {
          bool _isComplete = true;

          for (int i = 0; i < stayButtons.Length; i++)
          {
               if (!stayButtons[i].triggerButton)
               {
                    _isComplete = false;
                    break;
               }
          }

          if (_isComplete)
          {
               MovePlatforms();
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
               platforms[i].canMove = true;
          }
     }

     public void StopMovePlatforms()
     {
          for (int i = 0; i < platforms.Length; i++)
          {
               platforms[i].canMove = false;
          }
     }
}
