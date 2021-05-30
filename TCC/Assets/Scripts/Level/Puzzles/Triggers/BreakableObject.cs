using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class BreakableObject : MonoBehaviour
{
     public BreakableObjectType type;
     public GameObject[] objects;
     public Transform[] targets;
     public GameObject[] brokenParts;
     public Rigidbody rbody;
     public Collider col;
     public float durationShake;
     public float strengthShake;
     public int maxHit;
     public int hit;
     public bool triggerBroken;

     public UnityEvent OnBroken;

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
          transform.DOShakePosition(durationShake, strengthShake);
          if (hit >= maxHit && !triggerBroken)
          {
               triggerBroken = true;
               col.enabled = false;
               DropObject();     
               OnBroken?.Invoke();          
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

     public void DropObject()
     {
          if (type == BreakableObjectType.DROP_OBJECT)
          {
               for (int i = 0; i < objects.Length; i++)
               {
                    objects[i].SetActive(true);
                    objects[i].transform.position = targets[i].position;
                    objects[i].transform.parent = null;
               }
          }
     }
}
