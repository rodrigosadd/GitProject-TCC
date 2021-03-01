using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OpenDoor : MonoBehaviour
{
     public Transform doorLeft;
     public Transform doorRight;
     public Transform targetMoveLeft;
     public Transform targetMoveRight;
     public float speedMoveDoor;
     private Vector3 _targetInitialLPos;
     private Vector3 _targetInitialRPos;

     public virtual void StartPositionTargets()
     {
          _targetInitialLPos = new Vector3(doorLeft.position.x, doorLeft.position.y, doorLeft.position.z);
          _targetInitialRPos = new Vector3(doorRight.position.x, doorRight.position.y, doorRight.position.z);
     }

     public virtual void CanOpenDoor()
     {
          doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetMoveLeft.position, speedMoveDoor * Time.deltaTime);
          doorRight.position = Vector3.MoveTowards(doorRight.position, targetMoveRight.position, speedMoveDoor * Time.deltaTime);
     }

     public virtual void CanCloseDoor()
     {
          doorLeft.position = Vector3.MoveTowards(doorLeft.position, _targetInitialLPos, speedMoveDoor * Time.deltaTime);
          doorRight.position = Vector3.MoveTowards(doorRight.position, _targetInitialRPos, speedMoveDoor * Time.deltaTime);
     }
}
