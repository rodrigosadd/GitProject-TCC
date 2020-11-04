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
                    PlayerController.instance.movement.stateCharacter = CharacterState.DEAD;
                    PlayerController.instance.movement.rbody.useGravity = false;
                    PlayerController.instance.characterCollider.enabled = false;
                    currentMaxSpeed = PlayerController.instance.movement.maxSpeed;
                    PlayerController.instance.movement.maxSpeed = 0;
                    alive = false;
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
                    PlayerController.instance.movement.rbody.useGravity = true;
                    PlayerController.instance.characterCollider.enabled = true;
                    PlayerController.instance.movement.maxSpeed = currentMaxSpeed;
                    countdownKill = 0;
                    alive = true;
               }
          }
     }
}
