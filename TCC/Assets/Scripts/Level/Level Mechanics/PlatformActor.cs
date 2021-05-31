using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActor : MonoBehaviour
{
     public LayerMask layer;
     public Rigidbody myRBody;
     public CharacterController controller;
     public Transform origin;
     public float maxDistanceRay;
     private Rigidbody _rbody;
     private bool _isOnThePlatform;

// #if UNITY_EDITOR
//      public bool seeRangeDistanceRay;
// #endif

     void Start()
     {
          if(controller == null)
          {
               myRBody = GetComponent<Rigidbody>();
          }
     }

     void FixedUpdate()
     {
          PlatformDetector();
     }

     public void PlatformDetector()
     {
          RaycastHit _hitInfo;

          if (Physics.Raycast(origin.position, origin.up * -1, out _hitInfo, maxDistanceRay, layer))
          {
               if (_rbody == null)
               {
                    _rbody = _hitInfo.transform.gameObject.GetComponent<Rigidbody>();
               }
               else
               {
                    if (controller != null)
                    {
                         controller.Move(_rbody.velocity * Time.fixedDeltaTime);
                    }
                    else
                    {
                         if (myRBody != null)
                         {
                              if (_rbody.velocity != Vector3.zero)
                              {
                                   myRBody.velocity = _rbody.velocity;
                                   _isOnThePlatform = true;
                              }
                         }
                         else
                         {
                              Debug.Log("Rigidbody null!");
                         }
                    }
               }

               if(_rbody.velocity == Vector3.zero && _isOnThePlatform && controller == null)
               {
                    myRBody.velocity = Vector3.zero;
               }
          }
          else
          {
               if(_rbody != null)
               {                     
                    _rbody = null;
                    _isOnThePlatform = false;
               }
          }
     }

// #if UNITY_EDITOR
//      void OnDrawGizmos()
//      {
//           if (seeRangeDistanceRay)
//           {
//                Gizmos.color = Color.blue;
//                Gizmos.DrawRay(origin.position, (transform.up * -1) * maxDistanceRay);
//           }
//      }
// #endif
}