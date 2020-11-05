using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slide : MonoBehaviour
{
     public float forceSlide;

     void OnTriggerEnter(Collider collider)
     {
          PlayerController.instance.movement.rbody.AddForce(PlayerController.instance.characterGraphic.transform.forward * forceSlide, ForceMode.Impulse);
     }
}
