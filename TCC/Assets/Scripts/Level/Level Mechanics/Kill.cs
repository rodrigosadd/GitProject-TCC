using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
     public Transform currentPoint;
     private float countdownKill;
     private float currentMaxSpeed;
     private bool alive = true;

     void Update()
     {
          CheckDeath();
     }

     void OnTriggerEnter(Collider collider)
     {
          if (collider.transform.tag == "Player")
          {
               if (alive)
               {
                    RaycastHit _hitInfo;

                    if (Physics.Raycast(PlayerController.instance.transform.position, Vector3.down, out _hitInfo, 10f))
                    {
                         PlayerController.instance.transform.position = _hitInfo.point + new Vector3(0f, PlayerController.instance.death.offsetDead, 0f);
                         PlayerController.instance.movement.stateCharacter = CharacterState.DEAD;
                         PlayerController.instance.movement.rbody.constraints = RigidbodyConstraints.FreezePosition;
                         PlayerController.instance.characterCollider.enabled = false;
                         currentMaxSpeed = PlayerController.instance.movement.maxSpeed;
                         PlayerController.instance.movement.maxSpeed = 0;
                         alive = false;
                    }
               }
          }
     }

     void CheckDeath()
     {
          if (!alive)
          {
               if (countdownKill < 1)
               {
                    countdownKill += Time.deltaTime / 2f;
               }
               else
               {
                    PlayerController.instance.movement.stateCharacter = CharacterState.IDLE;
                    PlayerController.instance.transform.position = currentPoint.position;
                    PlayerController.instance.movement.rbody.constraints = RigidbodyConstraints.FreezeRotation;
                    PlayerController.instance.characterCollider.enabled = true;
                    PlayerController.instance.movement.maxSpeed = currentMaxSpeed;
                    countdownKill = 0;
                    alive = true;
               }
          }
     }
}
