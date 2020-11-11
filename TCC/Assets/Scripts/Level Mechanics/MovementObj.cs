using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementObj : MonoBehaviour
{
     public Transform[] spotsToMovePlatform;
     public int spotToMove = 0;
     public float speed = 2f;
     public float startWaitTime = 2f;
     public float waitTimeToMove = 0f;

     void Start()
     {
          waitTimeToMove = startWaitTime;
     }

     void Update()
     {
          MovementBetweenSpots();
     }

     public void MovementBetweenSpots()
     {
          transform.position = Vector3.MoveTowards(transform.position, spotsToMovePlatform[spotToMove].position, speed * Time.deltaTime);

          if (Vector3.Distance(transform.position, spotsToMovePlatform[spotToMove].position) < 1.8f)
          {
               if (waitTimeToMove <= 0)
               {
                    spotToMove++;
                    waitTimeToMove = startWaitTime;
               }
               else
               {
                    waitTimeToMove -= Time.deltaTime;
               }
               if (spotToMove >= spotsToMovePlatform.Length)
               {
                    spotToMove = 0;
               }
          }
     }
}
