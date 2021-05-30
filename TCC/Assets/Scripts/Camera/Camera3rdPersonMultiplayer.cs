using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Camera3rdPersonMultiplayer : MonoBehaviour
{
     public Transform targetCamera;
     public Transform targetCameraShake;
     public float minDistance;
     public float fixedMinDistance = 1.0f;
     public float maxDistance;
     public float fixedMaxDistance = 4.0f;
     public float clampAngleUp = 80.0f;
     public float clampAngleDown = 50.0f;
     public float inputSensitivityX = 150.0f;
     public float inputSensitivityY = 150.0f;
     public bool invertAxisX;
     public bool invertAxisY;
     public bool canMove;
     public bool showObject;
     public float strengthShake;
     private float _mouseX;
     private float _mouseY;
     private float _inputX;
     private float _inputZ;
     private float _finalInputX;
     private float _finalInputZ;
     private float _rotY = 0.0f;
     private float _rotX = 0.0f;
     private Vector3 _startPositionTarget;

     void Start()
     {
          Vector3 _rot = transform.localRotation.eulerAngles;
          _rotX = _rot.x;
          _rotY = _rot.y;
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
          minDistance = fixedMinDistance;
          maxDistance = fixedMaxDistance;
          _startPositionTarget = targetCamera.position;
     }

     void Update()
     {
          SetCamRotation();
     }

     void LateUpdate()
     {
          CameraUpdater();
     }

     public void SetCamRotation()
     {
          if (canMove)
          {
               // We setup the rotation of the sticks here
               _inputX = Input.GetAxis("RightStickHorizontal");
               _inputZ = Input.GetAxis("RightStickVertical");

               _mouseX = Input.GetAxis("Mouse X");
               _mouseY = Input.GetAxis("Mouse Y");

               _finalInputX = _inputX + _mouseX;
               _finalInputZ = _inputZ + _mouseY;

               if (!invertAxisX)
               {
                    _rotX += _finalInputZ * inputSensitivityX * Time.deltaTime;
               }
               else
               {
                    _rotX -= _finalInputZ * inputSensitivityX * Time.deltaTime;
               }

               if (!invertAxisY)
               {
                    _rotY += _finalInputX * inputSensitivityY * Time.deltaTime;
               }
               else
               {
                    _rotY -= _finalInputX * inputSensitivityY * Time.deltaTime;
               }

               _rotX = Mathf.Clamp(_rotX, -clampAngleDown, clampAngleUp);

               transform.rotation = Quaternion.Euler(_rotX, _rotY, 0f);
          }
     }

     public void CameraUpdater()
     {
          if(!showObject)
          {
               transform.position = targetCamera.position;
          }
     }

     public void ConfigToShowObject()
     {
          showObject = true;
          transform.position = targetCamera.position;
          transform.rotation = targetCamera.rotation;
          minDistance = 0;
          maxDistance = 0;
          canMove = false;
     }

     public void ResetConfig()
     {
          showObject = false;
          minDistance = fixedMinDistance;
          maxDistance = fixedMaxDistance;
          canMove = true;
     }

     public void CameraShake(float durationShake)
     {    
          Sequence sequence = DOTween.Sequence();

          sequence.AppendCallback(() => targetCamera = targetCameraShake)
                  .Append(targetCameraShake.DOShakePosition(durationShake, strengthShake).OnComplete(()=> targetCamera = PlayerController.instance.movement.targetCam))
                  .AppendCallback(() => targetCameraShake.position = targetCamera.position);
     }
}
