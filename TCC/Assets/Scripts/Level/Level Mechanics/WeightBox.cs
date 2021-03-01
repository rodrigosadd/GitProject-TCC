using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightBox : MonoBehaviour
{
     public Vector3 targetToRespawn;

     void Start()
     {
          targetToRespawn = new Vector3(transform.position.x, transform.position.y, transform.position.z);
     }
}
