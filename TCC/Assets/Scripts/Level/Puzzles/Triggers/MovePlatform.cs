using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : Platform
{
     public bool canMove;

     void Update()
     {
          TriggerMovementBetweenSpots();
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
}
