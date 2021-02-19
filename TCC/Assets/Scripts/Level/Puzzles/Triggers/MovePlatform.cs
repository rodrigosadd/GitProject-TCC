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
          if (canMove)
          {
               CountdownToMove();
               MovementBetweenSpots();
               MoveToSpot();
          }
     }
}
