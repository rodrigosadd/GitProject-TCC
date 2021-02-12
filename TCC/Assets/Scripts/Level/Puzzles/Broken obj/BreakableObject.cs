using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
     public GameObject[] brokenParts;
     public int maxHit;
     public int hit;
     public bool triggerBroken;

     void Start()
     {
          maxHit = brokenParts.Length;
     }

     void Update()
     {
          CheckLife();
     }

     public void TakeHit()
     {
          hit++;

          if (hit >= maxHit)
          {
               triggerBroken = true;
               gameObject.SetActive(false);
          }
     }

     public void CheckLife()
     {
          if (hit == 0)
          {
               return;
          }

          if (brokenParts[hit - 1] != null)
          {
               brokenParts[hit - 1].SetActive(false);
          }

          brokenParts[hit].SetActive(true);
     }
}
