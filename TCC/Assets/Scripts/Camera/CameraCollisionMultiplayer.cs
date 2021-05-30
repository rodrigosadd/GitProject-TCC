using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollisionMultiplayer : MonoBehaviour
{
     public LayerMask layerCollision;
     public float smooth = 10.0f;
     public float distance;
     public Camera3rdPersonMultiplayer multiplayerCamera;
     private Vector3 _dollyDir;

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
          Vector3 desiredCameraPos = transform.parent.TransformPoint(_dollyDir * multiplayerCamera.maxDistance);

          RaycastHit _hitInfo;

          if (Physics.Linecast(transform.parent.position, desiredCameraPos, out _hitInfo, layerCollision))
          {
               distance = Mathf.Clamp(_hitInfo.distance, multiplayerCamera.minDistance, multiplayerCamera.maxDistance);
          }
          else
          {
               distance = multiplayerCamera.maxDistance;
          }

          transform.localPosition = Vector3.Lerp(transform.localPosition, _dollyDir * distance, Time.deltaTime * smooth);
     }
}
