using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
     public float forceSlide;
     public float velocitySlide;
     public float TimeToReset;
     public bool sliding;
     private Vector3 _finalImpulse;
     private float _countdownFinishSliding;

     void Update()
     {
          Impulse();
          ResetSliding();
     }

     void OnTriggerEnter(Collider collider)
     {
          if (collider.tag == "Player" && !sliding)
          {
               PlayerController.instance.levelMechanics.sliding = true;
               sliding = true;
               _countdownFinishSliding = 0;
               PlayerAttackController.instance.ResetAttack();
               PlayerAnimationController.instance.oil.Play();
               PlayerAnimationController.instance.dust.Stop();
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
                    if (!Physics.Raycast(PlayerController.instance.characterGraphic.position, PlayerController.instance.characterGraphic.forward, 1.18f))
                    {
                         PlayerController.instance.movement.velocity.y = 0;
                         PlayerController.instance.SetControllerPosition(Vector3.MoveTowards(PlayerController.instance.transform.position, _finalImpulse, velocitySlide * Time.deltaTime));

                         if (_finalImpulse == PlayerController.instance.transform.position)
                         {                              
                              _finalImpulse = Vector3.zero;
                              PlayerController.instance.levelMechanics.sliding = false;
                              PlayerController.instance.movement.maxSpeed = PlayerController.instance.movement.fixedMaxSpeed;
                              sliding = false;
                              PlayerAnimationController.instance.oil.Stop();
                         }
                    }
                    else
                    {
                         _finalImpulse = Vector3.zero;
                         PlayerController.instance.levelMechanics.sliding = false;
                         PlayerController.instance.movement.maxSpeed = PlayerController.instance.movement.fixedMaxSpeed;
                         sliding = false;
                         PlayerAnimationController.instance.oil.Stop();
                    }
               }
          }
     }

     void ResetSliding()
     {
          if(sliding)
          {
               if(_countdownFinishSliding < 1)
               {
                    _countdownFinishSliding += Time.deltaTime / TimeToReset;
               }
               else
               {
                    _countdownFinishSliding = 0;
                    _finalImpulse = Vector3.zero;
                    PlayerController.instance.levelMechanics.sliding = false;
                    PlayerController.instance.movement.maxSpeed = PlayerController.instance.movement.fixedMaxSpeed;
                    sliding = false;
                    PlayerAnimationController.instance.oil.Stop();
               }
          }
     }
}