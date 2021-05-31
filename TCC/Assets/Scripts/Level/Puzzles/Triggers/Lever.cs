using FMODUnity;
using UnityEngine;

public class Lever : MonoBehaviour
{
     public LeverType type;
     public Transform lever;
     public float maxDistancePushLever;
     public float time;
     public float rotationLever;
     public bool triggerLever;
     [EventRef] public string leverSound;
     private float _countdown;
     private float _countdownDeactivateInteractAnimation;
     private bool _canPlayInteractAnimation;

     void Update()
     {
          PushLever();
          SetLeverRotCloseDoor();
          CountdownCloseDoor();
          CountdownDeactivateInteractAnimation();
     }

     public void PushLever()
     {
          float distanceBetween = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

          if (Input.GetButtonDown("Interact") && 
              distanceBetween < maxDistancePushLever &&
              !triggerLever &&
              !_canPlayInteractAnimation &&
              PlayerController.instance.movement.isGrounded &&
              !PlayerAttackController.instance.attaking)
          {
               triggerLever = true;
               _canPlayInteractAnimation = true;
               PlayerController.instance.levelMechanics.interacting = true;
               SetLeverRotOpenDoor();
          }
     }

     public void SetLeverRotOpenDoor()
     {
          lever.rotation = Quaternion.AngleAxis(rotationLever, transform.right);
          RuntimeManager.PlayOneShot(leverSound, transform.position);
     }

     public void SetLeverRotCloseDoor()
     {
          if (!triggerLever)
          {
               lever.rotation = Quaternion.AngleAxis(rotationLever * -1, transform.right);
          }
     }

     public void CountdownCloseDoor()
     {
          if (type == LeverType.TIMER && triggerLever)
          {
               if (_countdown < 1)
               {
                    _countdown += Time.deltaTime / time;
               }
               else
               {
                    _countdown = 0;
                    triggerLever = false;
               }
          }
     }

     public void CountdownDeactivateInteractAnimation()
     {
          if(_canPlayInteractAnimation)
          {
               if(_countdownDeactivateInteractAnimation < 1)
               {
                    _countdownDeactivateInteractAnimation += Time.deltaTime / 0.3f;
               }
               else
               {
                    _countdownDeactivateInteractAnimation = 0;
                    _canPlayInteractAnimation = false;
                    PlayerController.instance.levelMechanics.interacting = false;
                    PlayerController.instance.movement.currentSpeed = 0f;
               }
          }
     }

// #if UNITY_EDITOR
//      void OnDrawGizmos()
//      {
//           Gizmos.color = Color.blue;
//           Gizmos.DrawWireSphere(transform.position, maxDistancePushLever);
//      }
// #endif
}
