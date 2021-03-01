using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : Platform
{
     public PlatformType type;
     public bool canMove;

     void FixedUpdate()
     {
          TriggerMovementBetweenSpots();
          SetVelocityToCurrentBox();
     }

     public void TriggerMovementBetweenSpots()
     {
          if (canMove && type == PlatformType.MOVEMENT_BETWEEN_SPOTS)
          {
               CountdownToMove();
               MovementBetweenSpots();
          }
          else if (canMove && type == PlatformType.MOVE_TO_SPOT)
          {
               MoveToFirstSpot();
          }
          else if (!canMove && type == PlatformType.MOVE_TO_SPOT)
          {
               MoveToSecondSpot();
          }
     }

     public void MoveToFirstSpot()
     {
          rbody.MovePosition(Vector3.MoveTowards(transform.position, spotsToMovePlatform[0].position, speed * Time.deltaTime));
     }

     public void MoveToSecondSpot()
     {

          rbody.MovePosition(Vector3.MoveTowards(transform.position, spotsToMovePlatform[1].position, speed * Time.deltaTime));
     }
}
