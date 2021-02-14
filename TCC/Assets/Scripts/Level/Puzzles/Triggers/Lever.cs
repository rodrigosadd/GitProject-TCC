using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
     public Transform lever;
     public float maxDistancePushLever;
     public float timeToCloseDoor;
     public float rotationLever;
     public bool triggerLever;

     void Update()
     {
          PushLever();
          SetLeverRotCloseDoor();
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
          lever.rotation = Quaternion.AngleAxis(rotationLever, Vector3.right);
     }

     public void SetLeverRotCloseDoor()
     {
          if (!triggerLever)
          {
               lever.rotation = Quaternion.AngleAxis(rotationLever * -1, Vector3.right);
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
