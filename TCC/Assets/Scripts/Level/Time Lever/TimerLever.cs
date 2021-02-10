using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerLever : MonoBehaviour
{
     public Transform doorLeft;
     public Transform targetMoveLeft;
     public Transform targetInitialLPos;
     public Transform doorRight;
     public Transform targetMoveRight;
     public Transform targetInitialRPos;
     public Transform lever;
     public bool triggerLever;

     void Update()
     {

     }

     public void Lever()
     {
          float distanceBetween = Vector3.Distance(transform.position, PlayerController.instance.transform.position);
     }
}
