﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
     public Transform button;
     public Transform targetMoveButton;
     public float maxDistancePressButton;
     public bool triggerButton;
     private Vector3 _startPositionButton;
     private float _countdownAnimationButton;

     void Start()
     {
          _startPositionButton = new Vector3(button.position.x, button.position.y, button.position.z);
     }

     void Update()
     {
          CheckPressButton();
          CountdownButtonAnimation();
     }

     public void CheckPressButton()
     {
          float _distanceBetween = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

          if (_distanceBetween < maxDistancePressButton &&
              Input.GetButtonDown("Interact"))
          {
               triggerButton = true;
          }
     }

     public void StartButtonAnimation()
     {
          button.position = Vector3.MoveTowards(button.position, targetMoveButton.position, 1f * Time.deltaTime);
     }

     public void EndButtonAnimation()
     {
          button.position = Vector3.MoveTowards(button.position, _startPositionButton, 1f * Time.deltaTime);
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

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          Gizmos.color = Color.blue;
          Gizmos.DrawWireSphere(transform.position, maxDistancePressButton);
     }
#endif
}