using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayOnTopButton : MonoBehaviour
{
     public StayButton[] stayButton;
     public Transform doorLeft;
     public Transform targetMoveLeft;
     public Transform doorRight;
     public Transform targetMoveRight;
     public float speedMoveDoor;
     private Vector3 _targetInitialLPos;
     private Vector3 _targetInitialRPos;
     public bool _isComplete = true;

     void Start()
     {
          _targetInitialLPos = new Vector3(doorLeft.position.x, doorLeft.position.y, doorLeft.position.z);
          _targetInitialRPos = new Vector3(doorRight.position.x, doorRight.position.y, doorRight.position.z);
     }

     void Update()
     {
          CheckStayButtons();
     }

     public void CheckStayButtons()
     {
          _isComplete = true;

          for (int i = 0; i < stayButton.Length; i++)
          {
               if (!stayButton[i].triggerButton)
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
