using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Floor : MonoBehaviour
{
     public MeshRenderer meshObj;
     public BoxCollider boxObj;
     public BoxCollider boxObjTrigger;
     public GameObject obj;
     public Vector3 punchRotation;
     public float durationPunch;
     public float timeDisableFloor;
     public float timeEnableFloor;
     public bool offFloor = false;
     private float time;


     void Update()
     {
          DisableFloor();
     }

     public void DisableFloor()
     {
          if (offFloor == true)
          {
               time = time + 1 * Time.deltaTime;
               if (time >= timeDisableFloor)
               {
                    meshObj.enabled = false;
                    boxObj.enabled = false;
                    boxObjTrigger.enabled = false;
                    obj.SetActive(false);
               }

               if (time >= (timeEnableFloor + timeDisableFloor))
               {
                    meshObj.enabled = true;
                    boxObj.enabled = true;
                    boxObjTrigger.enabled = true;
                    obj.SetActive(true);
                    offFloor = false;
                    time = 0;
               }
          }
     }

     void OnTriggerEnter(Collider other)
     {
          if (other.transform.tag == "Player")
          {
               offFloor = true;
               transform.DOPunchRotation(punchRotation, durationPunch);
          }
     }
}
