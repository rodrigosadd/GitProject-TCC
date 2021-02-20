using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightsButtonsDoor : OpenDoor
{
     public WeightButton[] weightButtons;

     void Start()
     {
          StartPositionTargets();
     }

     void Update()
     {
          CheckObjectsBroken();
     }

     public void CheckObjectsBroken()
     {
          bool _isComplete = true;

          for (int i = 0; i < weightButtons.Length; i++)
          {
               if (!weightButtons[i].rightWeight)
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
