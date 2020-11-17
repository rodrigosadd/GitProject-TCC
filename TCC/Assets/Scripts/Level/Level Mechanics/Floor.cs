using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor : MonoBehaviour
{
     public MeshRenderer meshObj;
     public BoxCollider boxObj;
     public GameObject obj;
     public float timeDisableFloor;
     public float timeEnableFloor;

     private bool OffFloor = false;
     private float time;

     private void Update()
     {
          DisableFloor();
     }

     public void DisableFloor()
     {
          if (OffFloor == true)
          {
               time = time + 1 * Time.deltaTime;
               if (time >= timeDisableFloor)
               {
                    meshObj.enabled = false;
                    boxObj.enabled = false;
                    obj.SetActive(false);
               }

               if (time >= (timeEnableFloor + timeDisableFloor))
               {
                    meshObj.enabled = true;
                    boxObj.enabled = true;
                    obj.SetActive(true);
                    OffFloor = false;
                    time = 0;
               }
          }
     }

     void OnCollisionEnter(Collision collision)
     {
          if (collision.transform.tag == "Player")
          {
               OffFloor = true;
          }

     }
}
