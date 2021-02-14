using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingObjectsPlatform : MonoBehaviour
{
     public MovePlatform[] platforms;
     public BreakableObject[] breakableObjects;

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