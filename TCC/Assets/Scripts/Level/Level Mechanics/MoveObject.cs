using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
     public Transform[] spotsToMovePlatform;
     public int spotToMove;
     public float speed;
     public float waitTimeToMove;
     private float _countdown;
     private bool _canMove = true;

     void Update()
     {
          CountdownToMove();
          MovementBetweenSpots();
     }

     public void MovementBetweenSpots()
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

     public void CountdownToMove()
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
