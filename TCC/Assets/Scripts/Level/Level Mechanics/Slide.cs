using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
     public float forceSlide;
     public float timeToMoveAgaing;
     private float _countdown;
     private bool _canSlide;

     void Update()
     {
          CountdownCanMoveAgaing();
     }

     void OnTriggerEnter(Collider collider)
     {
          if (collider.tag == "Player" && !_canSlide)
          {
               PlayerController.instance.movement.rbody.AddForce(PlayerController.instance.characterGraphic.transform.forward * forceSlide, ForceMode.Impulse);
               PlayerController.instance.movement.sliding = true;
               _canSlide = true;
          }
     }

     public void CountdownCanMoveAgaing()
     {
          if (_canSlide)
          {
               if (_countdown < 1)
               {
                    _countdown += Time.deltaTime / timeToMoveAgaing;
               }
               else
               {
                    PlayerController.instance.movement.sliding = false;
                    _canSlide = false;
                    _countdown = 0;
               }
          }
     }
}
