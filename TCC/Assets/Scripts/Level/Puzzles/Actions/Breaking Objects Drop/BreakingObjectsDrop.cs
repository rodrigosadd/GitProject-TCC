using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakingObjectsDrop : MonoBehaviour
{
     public BreakableObject[] breakableObjects;
     public BreakableObject[] breakableObjectsDrop;

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
             ActiveGravity();
          }
     }

     void ActiveGravity()
     {
         for(int i = 0; i < breakableObjectsDrop.Length; i++)
         {
                breakableObjectsDrop[i].rbody.isKinematic = false;
                breakableObjectsDrop[i].rbody.useGravity = true;
         }
     }
}
