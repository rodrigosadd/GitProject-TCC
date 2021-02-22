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
                    obj.SetActive(false);
               }

               if (time >= (timeEnableFloor + timeDisableFloor))
               {
                    meshObj.enabled = true;
                    boxObj.enabled = true;
                    obj.SetActive(true);
                    offFloor = false;
                    time = 0;
               }
          }
     }

     void OnCollisionEnter(Collision collision)
     {
          if (collision.transform.tag == "Player")
          {
               offFloor = true;
          }
     }
}
