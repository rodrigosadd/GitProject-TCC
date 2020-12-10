using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
     public static CameraCollision instance;
     public LayerMask layerCollision;
     public float smooth = 10.0f;
     public float distance;
     private Vector3 _dollyDir;

     void Start()
     {
          instance = this;
     }

     void Awake()
     {
          _dollyDir = transform.localPosition.normalized;
          distance = transform.localPosition.magnitude;
     }

     void Update()
     {
          Collision();
     }

     public void Collision()
     {
          Vector3 desiredCameraPos = transform.parent.TransformPoint(_dollyDir * Camera3rdPerson.instance.maxDistance);

          RaycastHit _hitInfo;

          if (Physics.Linecast(transform.parent.position, desiredCameraPos, out _hitInfo, layerCollision))
          {
               distance = Mathf.Clamp(_hitInfo.distance, Camera3rdPerson.instance.minDistance, Camera3rdPerson.instance.maxDistance);
          }
          else
          {
               distance = Camera3rdPerson.instance.maxDistance;
          }

          transform.localPosition = Vector3.Lerp(transform.localPosition, _dollyDir * distance, Time.deltaTime * smooth);
     }
}
