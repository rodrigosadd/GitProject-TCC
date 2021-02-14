using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerLeverPlatform : MonoBehaviour
{
     public MovePlatform[] platforms;
     public Lever[] levers;
     public float timeToStopMovePlatform;
     private float _countdownToMovePlatforms;
     private bool _canToMovePlatforms;

     void Update()
     {
          CheckTriggerButtons();
          StopMovePlatforms();
     }

     public void CheckTriggerButtons()
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
               _canToMovePlatforms = false;
               MovePlatforms();
               CountdownToStopMovePlatforms();
          }
     }

     public void CountdownToStopMovePlatforms()
     {
          if (!_canToMovePlatforms)
          {
               if (_countdownToMovePlatforms < 1)
               {
                    _countdownToMovePlatforms += Time.deltaTime / timeToStopMovePlatform;
               }
               else
               {
                    _countdownToMovePlatforms = 0;
                    _canToMovePlatforms = true;

                    for (int i = 0; i < levers.Length; i++)
                    {
                         levers[i].triggerLever = false;
                    }
               }
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
          if (_canToMovePlatforms)
          {
               for (int i = 0; i < platforms.Length; i++)
               {
                    platforms[i].canMove = false;
               }
          }
     }
}
