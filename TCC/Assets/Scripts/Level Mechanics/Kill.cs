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
                    PlayerController.instance.animator.SetBool("Dying", true);
                    PlayerController.instance.rbody.useGravity = false;
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
                    PlayerController.instance.animator.SetBool("Dying", false);
                    PlayerController.instance.transform.position = currentPoint.position;
                    PlayerController.instance.rbody.useGravity = true;
                    PlayerController.instance.characterCollider.enabled = true;
                    PlayerController.instance.movement.maxSpeed = currentMaxSpeed;
                    countdownKill = 0;
                    alive = true;
               }
          }
     }
}
