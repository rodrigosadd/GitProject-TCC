using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class MovePlatform : Platform
{
     public PlatformType type;
      public Transform[] spotsToMovePlatform;
     public int spotToMove;
     public float speed;
     public float countdownToMove;
     public float waitTimeToMove;
     public bool canMove;
     public bool hasRestarted;
     public bool hasDelayToMove;
     [EventRef]
     public string moveSound;
     
     void FixedUpdate()
     {
          TriggerMovementBetweenSpots();
     }
     
     public void Initialize()
     {
          if(!hasRestarted)
          {
               canMove = true;
               hasRestarted = true;


               if(type == PlatformType.MOVEMENT_BETWEEN_SPOTS)
               {
                    canMove = false;
               }
          }
          
          if(type == PlatformType.MOVE_RESET)
          {
               transform.position = spotsToMovePlatform[0].position;      
               canMove = false;         
          }
     }

     public void TriggerMovementBetweenSpots()
     {
          if (type == PlatformType.MOVEMENT_BETWEEN_SPOTS)
          {
               CountdownToMove();
               MovementBetweenSpots();
          }
          
          if (canMove && type == PlatformType.MOVE_TO_SPOT)
          {
               MoveToFirstSpot();
          }
          else if (!canMove && type == PlatformType.MOVE_TO_SPOT)
          {
               MoveToSecondSpot();
          } 

          if(type == PlatformType.MOVE_RESET)
          {
               ResetAndMoveToSpot();
               CountdownToMove();
          }
     }

     public void MovementBetweenSpots()
     {
          if (canMove)
          {
               RuntimeManager.PlayOneShot(moveSound, transform.position);
               rbody.MovePosition(Vector3.MoveTowards(transform.position, spotsToMovePlatform[spotToMove].position, speed * Time.fixedDeltaTime));
          }

          if (transform.position == spotsToMovePlatform[spotToMove].position)
          {
               countdownToMove = 0;
               canMove = false;
               spotToMove++;

               if (spotToMove >= spotsToMovePlatform.Length)
               {
                    spotToMove = 0;
               }
          }
     }

     public void CountdownToMove()
     {
          if(hasRestarted)
          {
               if (!canMove && hasDelayToMove)
               {
                    if (countdownToMove < 1f)
                    {                        
                         countdownToMove += Time.deltaTime / waitTimeToMove;                         
                         canMove = false;
                    }
                    else
                    {
                         countdownToMove = 0;
                         canMove = true;
                    }
               }
               else if(!canMove && !hasDelayToMove)
               {
                    canMove = true;
               }
          }
     }

     public void MoveToFirstSpot()
     {
          rbody.MovePosition(Vector3.MoveTowards(transform.position, spotsToMovePlatform[0].position, speed * Time.fixedDeltaTime));
     }

     public void MoveToSecondSpot()
     {
          rbody.MovePosition(Vector3.MoveTowards(transform.position, spotsToMovePlatform[1].position, speed * Time.fixedDeltaTime));
     }

     public void ResetAndMoveToSpot()
     {
          if(canMove)
          {
               MoveToSecondSpot();
               
               if(transform.position == spotsToMovePlatform[1].position)
               {
                    canMove = false;
                    hasRestarted = false;
               }
          }
     }
}