using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class OpenDoor : MonoBehaviour
{
     public Transform doorLeft;
     public Transform targetMoveLeft;
     public Transform doorRight;
     public Transform targetMoveRight;
     public Vector3 targetInitialLPos;
     public Vector3 targetInitialRPos;
     public float speedMoveDoor;

     public virtual void StartPositionTargets()
     {
          targetInitialLPos = new Vector3(doorLeft.position.x, doorLeft.position.y, doorLeft.position.z);
          targetInitialRPos = new Vector3(doorRight.position.x, doorRight.position.y, doorRight.position.z);
     }

     public virtual void CanOpenDoor()
     {
          doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetMoveLeft.position, speedMoveDoor * Time.deltaTime);
          doorRight.position = Vector3.MoveTowards(doorRight.position, targetMoveRight.position, speedMoveDoor * Time.deltaTime);
     }

     public virtual void CanCloseDoor()
     {
          doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetInitialLPos, speedMoveDoor * Time.deltaTime);
          doorRight.position = Vector3.MoveTowards(doorRight.position, targetInitialRPos, speedMoveDoor * Time.deltaTime);
     }
}
