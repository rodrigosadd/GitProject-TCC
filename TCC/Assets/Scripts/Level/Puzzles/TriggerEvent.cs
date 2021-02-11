using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEvent : MonoBehaviour
{
     public UnityEvent myTrigger;

     void OnTriggerEnter(Collider other)
     {
          if (other.tag == "Player")
          {
               myTrigger?.Invoke();
          }
     }
}
