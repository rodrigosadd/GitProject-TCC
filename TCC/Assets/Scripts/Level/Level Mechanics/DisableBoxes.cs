using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBoxes : MonoBehaviour
{
     public Transform interactEmpty;

     void OnTriggerEnter(Collider collider)
     {
          if (collider.tag == "Heavy" ||
              collider.tag == "Light")
          {
               collider.gameObject.SetActive(false);
               collider.transform.parent = interactEmpty.transform;
          }
     }
}
