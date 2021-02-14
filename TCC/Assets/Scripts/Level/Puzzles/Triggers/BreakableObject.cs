using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableObject : MonoBehaviour
{
     public GameObject[] brokenParts;
     public Collider col;
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

          if (hit >= maxHit && !triggerBroken)
          {
               triggerBroken = true;
               col.enabled = false;
          }
     }

     public void CheckLife()
     {
          if (hit != 0)
          {
               for (int i = 0; i < brokenParts.Length; i++)
               {
                    brokenParts[i].SetActive(false);
               }
          }

          if (!triggerBroken)
          {
               if (hit == 0)
               {
                    return;
               }

               brokenParts[hit].SetActive(true);
          }
     }
}
