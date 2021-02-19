using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
     public PlatformType type;
     public Transform[] spotsToMovePlatform;
     public Transform playerEmpty;
     public Transform interactObjectEmpty;
     public int spotToMove;
     public float speed;
     public float waitTimeToMove;
     private float _countdown;
     private bool _canMove = true;

     void Update()
     {
          CountdownToMove();
          MovementBetweenSpots();
          MoveToSpot();
     }

     public virtual void MovementBetweenSpots()
     {
          if (type == PlatformType.MOVEMENT_BETWEEN_SPOTS)
          {
               if (_canMove)
               {
                    transform.position = Vector3.MoveTowards(transform.position, spotsToMovePlatform[spotToMove].position, speed * Time.deltaTime);
               }

               if (transform.position == spotsToMovePlatform[spotToMove].position)
               {
                    if (_countdown == 0)
                    {
                         _canMove = false;
                         spotToMove++;
                    }
                    if (spotToMove >= spotsToMovePlatform.Length)
                    {
                         spotToMove = 0;
                    }
               }
          }
     }

     public void CountdownToMove()
     {
          if (type == PlatformType.MOVEMENT_BETWEEN_SPOTS)
          {
               if (!_canMove)
               {
                    if (_countdown < 1)
                    {
                         _countdown += Time.deltaTime / waitTimeToMove;
                         _canMove = false;
                    }
                    else
                    {
                         _countdown = 0;
                         _canMove = true;
                    }
               }
          }
     }

     public void MoveToSpot()
     {
          if (type == PlatformType.MOVE_TO_SPOT)
          {
               transform.position = Vector3.MoveTowards(transform.position, spotsToMovePlatform[0].position, speed * Time.deltaTime);
          }
     }

     void OnTriggerEnter(Collider other)
     {
          if (other.tag == "Player")
          {
               PlayerController.instance.transform.parent = transform;
          }
          if (other.tag == "Light" ||
              other.tag == "Heavy")
          {
               other.transform.parent = transform;
          }
     }

     void OnTriggerExit(Collider other)
     {
          if (other.tag == "Player")
          {
               PlayerController.instance.transform.parent = playerEmpty;
          }
          if (other.tag == "Light" ||
              other.tag == "Heavy")
          {
               other.transform.parent = interactObjectEmpty;
          }
     }
}
