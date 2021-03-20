using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
     public float forceSlide;
     public float velocitySlide;
     public bool sliding;
     private Vector3 _finalImpulse;

     void Update()
     {
          Impulse();
     }

     void OnTriggerEnter(Collider collider)
     {
          if (collider.tag == "Player" && !sliding)
          {
               PlayerController.instance.movement.sliding = true;
               sliding = true;
               PlayerAttackController.instance.ResetAttack();
          }
     }

     public void Impulse()
     {
          if (sliding)
          {
               if (_finalImpulse == Vector3.zero)
               {
                    _finalImpulse = PlayerController.instance.transform.position + (PlayerController.instance.characterGraphic.forward * forceSlide);
               }
               else
               {
                    if (!Physics.Raycast(PlayerController.instance.transform.position, PlayerController.instance.characterGraphic.forward, PlayerController.instance.push.rangePush))
                    {
                         PlayerController.instance.movement.velocity.y = 0;
                         PlayerController.instance.SetControllerPosition(Vector3.MoveTowards(PlayerController.instance.transform.position, _finalImpulse, velocitySlide * Time.deltaTime));

                         if (_finalImpulse == PlayerController.instance.transform.position)
                         {                              
                              _finalImpulse = Vector3.zero;
                              PlayerController.instance.movement.sliding = false;
                              sliding = false;
                         }
                    }
                    else
                    {
                         _finalImpulse = Vector3.zero;
                         PlayerController.instance.movement.sliding = false;
                         sliding = false;
                    }
               }
          }
     }
}