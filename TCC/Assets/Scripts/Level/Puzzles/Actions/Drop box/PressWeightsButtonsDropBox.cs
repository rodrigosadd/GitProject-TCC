using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressWeightsButtonsDropBox : MonoBehaviour
{
     public GameObject[] objects;
     public Transform[] targets;
     public WeightButton[] buttons;
     private bool _canDrop = true;

     void Update()
     {
          CheckTriggers();
     }

     public void CheckTriggers()
     {
          bool _isComplete = true;

          for (int i = 0; i < buttons.Length; i++)
          {
               if (!buttons[i].rightWeight)
               {
                    _isComplete = false;
                    break;
               }
          }

          if (_isComplete)
          {
               ActiveObjetcs();
               _canDrop = false;
          }
     }

     public void ActiveObjetcs()
     {
          if (_canDrop)
          {
               for (int i = 0; i < objects.Length; i++)
               {
                    objects[i].SetActive(true);
                    objects[i].transform.position = targets[i].position;
               }
          }
     }
}
