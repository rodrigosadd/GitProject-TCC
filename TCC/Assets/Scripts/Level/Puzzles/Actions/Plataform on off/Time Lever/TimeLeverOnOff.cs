using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeLeverOnOff : MonoBehaviour
{
     public Lever[] levers;
     public GameObject[] floors;

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
               CheckActiveFloor();
          }
          else
          {
               CheckDeactiveFloor();
          }
     }

     public void CheckActiveFloor()
     {
          for (int i = 0; i < floors.Length; i++)
          {
               floors[i].SetActive(true);
          }
     }

     public void CheckDeactiveFloor()
     {
          for (int i = 0; i < floors.Length; i++)
          {
               floors[i].SetActive(false);
          }
     }
}