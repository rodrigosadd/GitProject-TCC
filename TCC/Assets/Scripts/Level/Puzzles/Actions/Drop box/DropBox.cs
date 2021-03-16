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
               ActiveAllObjetcs();
               _isComplete = false;
          }
     }

     public void ActiveAllObjetcs()
     {
          for (int i = 0; i < objects.Length; i++)
          {
               if (objects[i].activeSelf)
               {
                    continue;
               }

               objects[i].SetActive(true);
               objects[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
               objects[i].transform.position = targets[i].position;
          }
     }
}
