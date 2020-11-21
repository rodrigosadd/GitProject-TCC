using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
     public Transform spot;
     public GameObject animCheckPoint;
     public float timeToDeactivate;
     public bool active;

     private float _countdownDeactivate;

     void Update()
     {
          DeactivateCheckPoint();
     }

     public void DeactivateCheckPoint()
     {
          if (active)
          {
               if (_countdownDeactivate < 1)
               {
                    _countdownDeactivate += Time.deltaTime / timeToDeactivate;
               }
               else
               {
                    _countdownDeactivate = 0;
                    animCheckPoint.SetActive(false);
                    active = false;
               }
          }
     }

     void OnTriggerEnter(Collider collider)
     {
          if (PlayerController.instance.death.currentPoint != spot)
          {
               if (collider.tag == "Player" && !active)
               {
                    PlayerController.instance.death.currentPoint = spot;
                    animCheckPoint.SetActive(true);
                    active = true;
               }
          }
     }
}
