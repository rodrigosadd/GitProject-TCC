using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piston : MonoBehaviour
{
     public Collider deathCollider;
     public float timeToMovePiston;
     public float pistonSpeedDown;
     public float pistonSpeedUp;
     public float pistonOffset;
     public bool goingDown = true;

     private float _currentCountdown;
     private Vector3 _initialPositionPiston;

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
          _initialPositionPiston = new Vector3(transform.position.x, transform.position.y, transform.position.z);
     }

     public void MovePiston()
     {
          RaycastHit _hitInfo;
          if (Physics.Raycast(transform.position, Vector3.down, out _hitInfo, 30f))
          {
               float distanceUntilPoint = Vector3.Distance(transform.position, _hitInfo.point);
               float distanceUntilFirstPosition = Vector3.Distance(transform.position, _initialPositionPiston);
               if (goingDown)
               {
                    transform.position = Vector3.MoveTowards(transform.position, _hitInfo.point + new Vector3(0f, pistonOffset, 0f), pistonSpeedDown * Time.deltaTime);
                    deathCollider.enabled = true;
                    if (distanceUntilPoint <= pistonOffset)
                    {
                         if (_currentCountdown < 1)
                         {
                              _currentCountdown += Time.deltaTime / timeToMovePiston;
                         }
                         else
                         {
                              goingDown = false;
                              _currentCountdown = 0;
                         }
                    }
               }
               else
               {
                    transform.position = Vector3.MoveTowards(transform.position, _initialPositionPiston, pistonSpeedUp * Time.deltaTime);
                    deathCollider.enabled = false;
                    if (distanceUntilFirstPosition <= 0)
                    {
                         goingDown = true;
                    }
               }
          }
     }
}
