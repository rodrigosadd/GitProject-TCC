using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
     public LeverType type;
     public Transform lever;
     public float maxDistancePushLever;
     public float time;
     public float rotationLever;
     public bool triggerLever;
     private float _countdown;

     void Update()
     {
          PushLever();
          SetLeverRotCloseDoor();
          CountdownCloseDoor();
     }

     public void PushLever()
     {
          float distanceBetween = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

          if (Input.GetButtonDown("Interact") && distanceBetween < maxDistancePushLever)
          {
               triggerLever = true;
               SetLeverRotOpenDoor();
          }
     }

     public void SetLeverRotOpenDoor()
     {
          lever.rotation = Quaternion.AngleAxis(rotationLever, transform.right);
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

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          Gizmos.color = Color.blue;
          Gizmos.DrawWireSphere(transform.position, maxDistancePushLever);
     }
#endif
}
