using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformActor : MonoBehaviour
{
     public LayerMask layer;
     public Rigidbody _myRBody;
     public CharacterController controller;
     public Transform origin;
     public float maxDistanceRay;
     private Rigidbody _rbody;

#if UNITY_EDITOR
     public bool seeRangeDistanceRay;
#endif

     void FixedUpdate()
     {
          PlatformDetector();
     }

     public void PlatformDetector()
     {
          RaycastHit _hitInfo;

          if (Physics.Raycast(origin.position, Vector3.down, out _hitInfo, maxDistanceRay, layer))
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
                         if (_myRBody != null)
                         {
                              if (_rbody.velocity != Vector3.zero)
                              {
                                   _myRBody.velocity = _rbody.velocity;
                              }
                         }
                         else
                         {
                              Debug.Log("Rigidbody null!");
                         }
                    }
               }
          }
          else
          {
               _rbody = null;
          }
     }

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          if (seeRangeDistanceRay)
          {
               Gizmos.color = Color.blue;
               Gizmos.DrawRay(origin.position, Vector3.down * maxDistanceRay);
          }
     }
#endif
}