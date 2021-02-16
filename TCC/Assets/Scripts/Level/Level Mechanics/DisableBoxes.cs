using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBoxes : MonoBehaviour
{
     void OnTriggerEnter(Collider collider)
     {
          if (collider.tag == "Heavy" ||
              collider.tag == "Light")
          {
               collider.gameObject.SetActive(false);
          }
     }
}
