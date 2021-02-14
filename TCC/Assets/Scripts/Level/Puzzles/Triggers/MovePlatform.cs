using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : Platform
{
     public bool canMove;

     void Start()
     {
          StartWaitTime();
     }

     void Update()
     {
          TriggerMovementBetweenSpots();
     }

     public void TriggerMovementBetweenSpots()
     {
          if (canMove)
          {
               MovementBetweenSpots();
          }
     }
}
