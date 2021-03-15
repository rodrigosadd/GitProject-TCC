using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropBox : MonoBehaviour
{
     public GameObject[] objects;
     public Transform[] targets;
     public Button[] buttons;
     private bool _canSpawn = true;

     void Update()
     {
          CheckTriggers();
     }

     public void CheckTriggers()
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
               ActiveObjetcsAlways();
               _isComplete = false;
          }
     }

     public void ActiveObjetcsAlways()
     {
          for (int i = 0; i < objects.Length; i++)
          {
               if (objects[i].activeSelf)
               {
                    return;
               }

               objects[i].SetActive(true);
               objects[i].transform.position = targets[i].position;
          }
     }
}
