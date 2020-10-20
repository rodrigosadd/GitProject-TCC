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
     public float pistonSpeed;
     public float pistonOffset;
     [Range(0, 2)]
     public float fanSpeed;
     public MeshRenderer meshObj;
     public BoxCollider boxObj;
     public float timeToMovePiston;
     public bool goingDown = true;
     private float time;
     private float maxSpeedAux;
     private bool OffFloor = false;
     private Vector3 initialPositionPiston;
     private float currentCountdown;

    private void OnTriggerEnter(Collider other)
     {
          if (mechanicType == MechanicType.KILL)
          {
            //Kill the character.
            Debug.Log("Personagem morreu.");
            PlayerController.instance.maxSpeed = 0;
          }
          else if (mechanicType == MechanicType.SLIDE)
          {
               //Slide the character.
               PlayerController.instance.rbody.AddForce(PlayerController.instance.characterGraphic.transform.forward * forceSlide, ForceMode.Impulse);
          }
          else if(mechanicType == MechanicType.FAN)
          {
               Debug.Log("Personagem morreu.");
               PlayerController.instance.maxSpeed = 0;              
          }
          else if(mechanicType == MechanicType.PISTON)
          {
               Debug.Log("Personagem morreu.");
               PlayerController.instance.maxSpeed = 0;
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
     public void RotateFan()
     {
        if (mechanicType == MechanicType.FAN)
        {
            this.gameObject.transform.Rotate(0, 0, fanSpeed);
        }
     }
     public void MovePiston()
     {
        if(mechanicType == MechanicType.PISTON)
          {
            RaycastHit _hitInfo;
            if(Physics.Raycast(transform.position, Vector3.down, out _hitInfo, 30f))
            {
               float distanceUntilPoint = Vector3.Distance(transform.position, _hitInfo.point);
               float distanceUntilFirstPosition = Vector3.Distance(transform.position, initialPositionPiston);
               if(goingDown)
               {
                    transform.position = Vector3.MoveTowards(transform.position, _hitInfo.point + new Vector3(0f, pistonOffset, 0f), pistonSpeed * Time.deltaTime);
                    if(distanceUntilPoint <= pistonOffset)
                    {
                         if(currentCountdown < 1)
                         {
                              currentCountdown += Time.deltaTime / timeToMovePiston;
                         }
                         else
                         {
                              goingDown = false;
                              currentCountdown = 0;
                         }
                    }
               }
               else
               {
                    transform.position = Vector3.MoveTowards(transform.position, initialPositionPiston, pistonSpeed * Time.deltaTime);
                    if(distanceUntilFirstPosition <= 0)
                    {
                        goingDown = true;
                    }
               }
            }
          }
     }
     public void GetPistonInitialPosition()
     {
          initialPositionPiston = transform.position;
     }
     private void Start() 
     {
          GetPistonInitialPosition();
     }
     private void Update()
     {
          DesableFloor();
          RotateFan();
          MovePiston();
     }
}
