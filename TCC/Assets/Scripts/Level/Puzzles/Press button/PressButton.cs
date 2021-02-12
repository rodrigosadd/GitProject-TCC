using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressButton : MonoBehaviour
{
     public Button[] buttons;
     public Transform doorLeft;
     public Transform targetMoveLeft;
     public Transform doorRight;
     public Transform targetMoveRight;
     public float speedMoveDoor;

     void Update()
     {
          CheckTriggerButtons();
     }

     public void CheckTriggerButtons()
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
               OpenDoor();
          }
     }

     public void OpenDoor()
     {
          doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetMoveLeft.position, speedMoveDoor * Time.deltaTime);
          doorRight.position = Vector3.MoveTowards(doorRight.position, targetMoveRight.position, speedMoveDoor * Time.deltaTime);
     }
}