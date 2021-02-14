using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayButton : MonoBehaviour
{
     public Transform button;
     public Transform targetMoveButton;
     public float maxDistanceStayOnButton;
     public bool triggerButton;
     private Vector3 _startPositionButton;

     void Start()
     {
          _startPositionButton = new Vector3(button.position.x, button.position.y, button.position.z);
     }

     void Update()
     {
          PlayerStayOnButton();
     }

     public void PlayerStayOnButton()
     {
          float distanceBetween = Vector3.Distance(transform.position + transform.up, PlayerController.instance.transform.position);

          if (distanceBetween < maxDistanceStayOnButton)
          {
               StartButtonAnimation();
               triggerButton = true;
          }
          else
          {
               PressedButtonAnimation();
               triggerButton = false;
          }
     }

     public void StartButtonAnimation()
     {
          button.position = Vector3.MoveTowards(button.position, targetMoveButton.position, 1f * Time.deltaTime);
     }

     public void PressedButtonAnimation()
     {
          button.position = Vector3.MoveTowards(button.position, _startPositionButton, 1f * Time.deltaTime);
     }

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          Gizmos.color = Color.blue;
          Gizmos.DrawWireSphere(transform.position + transform.up, maxDistanceStayOnButton);
     }
#endif
}
