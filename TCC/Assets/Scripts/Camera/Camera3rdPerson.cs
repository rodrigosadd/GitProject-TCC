using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera3rdPerson : MonoBehaviour
{
     public static Camera3rdPerson instance;
     public Transform targetCamera;
     public float minDistance = 1.0f;
     public float maxDistance = 4.0f;
     public float clampAngleUp = 80.0f;
     public float clampAngleDown = 50.0f;
     public float inputSensitivityX = 150.0f;
     public float inputSensitivityY = 150.0f;
     public bool invertAxisX;
     public bool invertAxisY;
     private float _mouseX;
     private float _mouseY;
     private float _inputX;
     private float _inputZ;
     private float _finalInputX;
     private float _finalInputZ;
     private float _rotY = 0.0f;
     private float _rotX = 0.0f;

     void Start()
     {
          instance = this;
          Vector3 _rot = transform.localRotation.eulerAngles;
          _rotX = _rot.x;
          _rotY = _rot.y;
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
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

     public void CameraUpdater()
     {
          transform.position = targetCamera.position;
     }
}
