using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
     public Rigidbody rbody;
     public PlatformType type;
     public Transform[] spotsToMovePlatform;
     public Transform interactObjectEmpty;
     public int spotToMove;
     public float speed;
     public float waitTimeToMove;
     private float _countdown;
     private bool _canMove = true;

     void Update()
     {
          CountdownToMove();
     }

     void FixedUpdate()
     {
          MovementBetweenSpots();
          MoveToSecondSpot();
     }

     public virtual void MovementBetweenSpots()
     {
          if (type == PlatformType.MOVEMENT_BETWEEN_SPOTS)
          {
               if (_canMove)
               {
                    rbody.MovePosition(Vector3.MoveTowards(transform.position, spotsToMovePlatform[spotToMove].position, speed * Time.deltaTime));
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

     public void MoveToFirstSpot()
     {
          if (type == PlatformType.MOVE_TO_SPOT)
          {
               rbody.MovePosition(Vector3.MoveTowards(transform.position, spotsToMovePlatform[0].position, speed * Time.deltaTime));
          }
     }

     public void MoveToSecondSpot()
     {
          if (type == PlatformType.MOVE_TO_SPOT)
          {
               rbody.MovePosition(Vector3.MoveTowards(transform.position, spotsToMovePlatform[1].position, speed * Time.deltaTime));
          }
     }

     void OnTriggerEnter(Collider other)
     {
          if (other.tag == "Light" ||
              other.tag == "Heavy")
          {
               other.transform.parent = transform;
          }
     }

     void OnTriggerStay(Collider other)
     {
          if (other.tag == "Player")
          {
               PlayerController.instance.movement.controller.Move(rbody.velocity * Time.deltaTime);
          }
     }

     void OnTriggerExit(Collider other)
     {
          if (other.tag == "Light" ||
              other.tag == "Heavy")
          {
               other.transform.parent = interactObjectEmpty;
          }
     }
}
