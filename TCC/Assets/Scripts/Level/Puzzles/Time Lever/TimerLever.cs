using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerLever : MonoBehaviour
{
     public Transform doorLeft;
     public Transform targetInitialLPos;
     public Transform targetMoveLeft;
     public Transform doorRight;
     public Transform targetInitialRPos;
     public Transform targetMoveRight;
     public Transform lever;
     public float maxDistancePushLever;
     public float speedMoveDoor;
     public float timeToCloseDoor;
     public float rotationLever;
     private bool _startTimeToClose;
     private float _countdownToCloseDoor;
     private bool _canCloseDoor;

     void Update()
     {
          PushLever();
          CountdownCloseDoor();
          CloseDoor();
     }

     public void PushLever()
     {
          float distanceBetween = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

          if (Input.GetKey(KeyCode.E) && distanceBetween < maxDistancePushLever)
          {
               _startTimeToClose = true;
               _canCloseDoor = false;
               SetLeverRotOpenDoor();
          }
     }

     public void CountdownCloseDoor()
     {
          if (_startTimeToClose)
          {
               if (_countdownToCloseDoor < 1)
               {
                    OpenDoor();
                    _countdownToCloseDoor += Time.deltaTime / timeToCloseDoor;
               }
               else
               {
                    _countdownToCloseDoor = 0;
                    _startTimeToClose = false;
                    _canCloseDoor = true;
                    SetLeverRotCloseDoor();
               }
          }
     }

     public void OpenDoor()
     {
          doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetMoveLeft.position, speedMoveDoor * Time.deltaTime);
          doorRight.position = Vector3.MoveTowards(doorRight.position, targetMoveRight.position, speedMoveDoor * Time.deltaTime);
     }

     public void CloseDoor()
     {
          if (_canCloseDoor)
          {
               doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetInitialLPos.position, speedMoveDoor * Time.deltaTime);
               doorRight.position = Vector3.MoveTowards(doorRight.position, targetInitialRPos.position, speedMoveDoor * Time.deltaTime);
          }
     }

     public void SetLeverRotOpenDoor()
     {
          lever.rotation = Quaternion.AngleAxis(rotationLever, Vector3.right);
     }

     public void SetLeverRotCloseDoor()
     {
          lever.rotation = Quaternion.AngleAxis(rotationLever * -1, Vector3.right);
     }

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          Gizmos.color = Color.blue;
          Gizmos.DrawWireSphere(transform.position, maxDistancePushLever);
     }
#endif
}