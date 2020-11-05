using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
     public float timeToMovePiston;
     public float pistonSpeed;
     public float pistonOffset;
     public bool goingDown = true;

     private float currentCountdown;
     private Vector3 initialPositionPiston;

     void Start()
     {
          GetPistonInitialPosition();
     }

     void Update()
     {
          MovePiston();
     }

     public void GetPistonInitialPosition()
     {
          initialPositionPiston = transform.position;
     }

     public void MovePiston()
     {
          RaycastHit _hitInfo;
          if (Physics.Raycast(transform.position, Vector3.down, out _hitInfo, 30f))
          {
               float distanceUntilPoint = Vector3.Distance(transform.position, _hitInfo.point);
               float distanceUntilFirstPosition = Vector3.Distance(transform.position, initialPositionPiston);
               if (goingDown)
               {
                    transform.position = Vector3.MoveTowards(transform.position, _hitInfo.point + new Vector3(0f, pistonOffset, 0f), pistonSpeed * Time.deltaTime);
                    if (distanceUntilPoint <= pistonOffset)
                    {
                         if (currentCountdown < 1)
                         {
                              currentCountdown += Time.deltaTime / timeToMovePiston;
                         }
                         else
                         {
                              goingDown = false;
                              currentCountdown = 0;
                         }
                    }
               }
               else
               {
                    transform.position = Vector3.MoveTowards(transform.position, initialPositionPiston, pistonSpeed * Time.deltaTime);
                    if (distanceUntilFirstPosition <= 0)
                    {
                         goingDown = true;
                    }
               }
          }
     }
}
