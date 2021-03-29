using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Button : MonoBehaviour
{
     public ButtonType type;
     public Transform button;
     public Transform targetMoveButton;
     public float maxDistancePressButton;
     [EventRef]
     public string clickSound;
     public bool triggerButton;
     private Vector3 _startPositionButton;
     private float _countdownAnimationButton;
     private float _countdownDeactivateInteractAnimation;
     private bool _canPlayInteractAnimation;

     void Start()
     {
          _startPositionButton = new Vector3(button.position.x, button.position.y, button.position.z);
     }

     void Update()
     {
          CheckPressButton();
          CountdownButtonAnimation();
          CountdownDeactivateInteractAnimation();
     }

     public void CheckPressButton()
     {
          float _distanceBetween = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

          if (_distanceBetween < maxDistancePressButton &&
              !triggerButton &&
              !_canPlayInteractAnimation &&
              PlayerController.instance.movement.isGrounded &&
              !PlayerAttackController.instance.attaking &&
              Input.GetButtonDown("Interact"))
          {
               triggerButton = true;
               _canPlayInteractAnimation = true;
               PlayerController.instance.levelMechanics.interacting = true;
               RuntimeManager.PlayOneShot(clickSound, transform.position);
          }
     }

     public void ResetButton()
     {
          if (type == ButtonType.PRESS_ONCE)
          {
               return;
          }

          if (type == ButtonType.ALWAYS_PRESS)
          {
               if (button.position == _startPositionButton)
               {
                    triggerButton = false;
                    _countdownAnimationButton = 0;
               }
          }
     }

     public void StartButtonAnimation()
     {
          button.position = Vector3.MoveTowards(button.position, targetMoveButton.position, 1f * Time.deltaTime);
     }

     public void EndButtonAnimation()
     {
          if (type == ButtonType.ALWAYS_PRESS)
          {
               button.position = Vector3.MoveTowards(button.position, _startPositionButton, 1f * Time.deltaTime);
          }

          ResetButton();
     }

     public void CountdownButtonAnimation()
     {
          if (triggerButton)
          {
               if (_countdownAnimationButton < 1f)
               {
                    StartButtonAnimation();
                    _countdownAnimationButton += Time.deltaTime / 1f;
               }
               else
               {
                    EndButtonAnimation();
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

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          Gizmos.color = Color.blue;
          Gizmos.DrawWireSphere(transform.position, maxDistancePressButton);
     }
#endif
}
