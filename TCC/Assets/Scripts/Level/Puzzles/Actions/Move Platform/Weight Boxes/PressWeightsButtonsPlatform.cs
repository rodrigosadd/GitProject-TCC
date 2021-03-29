using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressWeightsButtonsPlatform : MonoBehaviour
{
     public MovePlatform[] platforms;
     public WeightButton[] weightButtons;

     void Update()
     {
          CheckPressButtons();
     }

     public void CheckPressButtons()
     {
          bool isComplete = true;

          for (int i = 0; i < weightButtons.Length; i++)
          {
               if (!weightButtons[i].rightWeight)
               {
                    isComplete = false;
                    break;
               }
          }

          if (isComplete)
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
               platforms[i].Initialize();               
          }
     }

     public void StopMovePlatforms()
     {
          for (int i = 0; i < platforms.Length; i++)
          {
               platforms[i].canMove = false;
               platforms[i].hasRestarted = false;
          }
     }
}
