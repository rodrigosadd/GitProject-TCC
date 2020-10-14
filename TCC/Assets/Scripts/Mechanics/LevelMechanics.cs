using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMechanics : MonoBehaviour
{
     public MechanicType mechanicType;

     [Header("Level Mechanics variables")]
     public float forceSlide;
     public float slowValue;
     public bool slowed = false;
     public float timeDisableFloor;
     public float timeEnableFloor;
     public MeshRenderer meshObj;
     public BoxCollider boxObj;

     private float time;
     private float maxSpeedAux;
     private bool OffFloor = false;

     private void OnTriggerEnter(Collider other)
     {
          if (mechanicType == MechanicType.KILL)
          {
               //Kill the character.
          }
          else if (mechanicType == MechanicType.PUSH)
          {
               //Push the character.
          }
          else if (mechanicType == MechanicType.SLIDE)
          {
               //Slide the character.
               PlayerController.instance.rbody.AddForce(PlayerController.instance.characterGraphic.transform.forward * forceSlide, ForceMode.Impulse);
          }
     }

     private void OnTriggerStay(Collider other)
     {
          if (mechanicType == MechanicType.SLOW)
          {
               if (!slowed && other.transform.tag == "Player")
               {
                    maxSpeedAux = PlayerController.instance.maxSpeed;
                    PlayerController.instance.maxSpeed = slowValue;
                    slowed = true;
               }
          }
     }

     private void OnCollisionEnter(Collision collision)
     {
          if (mechanicType == MechanicType.FLOOR)
          {
               if (collision.transform.tag == "Player")
               {
                    OffFloor = true;
               }
          }
     }

     private void OnTriggerExit(Collider other)
     {
          if (mechanicType == MechanicType.SLOW)
          {
               if (slowed && other.transform.tag == "Player")
               {
                    PlayerController.instance.maxSpeed = maxSpeedAux;
                    slowed = false;
               }
          }
     }

     public void DesableFloor()
     {
          if (OffFloor == true)
          {
               time = time + 1 * Time.deltaTime;
               if (time >= timeDisableFloor)
               {
                    meshObj.enabled = false;
                    boxObj.enabled = false;
               }

               if (time >= (timeEnableFloor + timeDisableFloor))
               {
                    meshObj.enabled = true;
                    boxObj.enabled = true;
                    OffFloor = false;
                    time = 0;
               }
          }
     }

     private void Update()
     {
          DesableFloor();

          if (mechanicType == MechanicType.FAN)
          {
               this.gameObject.transform.Rotate(0, 0, 3);
          }
     }
}
