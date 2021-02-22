using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeightButton : MonoBehaviour
{
     public WeightType weightType;
     public LayerMask layerBox;
     public Collider[] amount;
     public float maxDistanceCheck;
     public int amountLights;
     public int amountHeavies;
     public bool rightWeight;

     void Update()
     {
          CheckAmountEntry();
     }

     public void CheckAmountEntry()
     {
          amount = Physics.OverlapSphere(transform.position, maxDistanceCheck, layerBox);

          amountLights = 0;
          amountHeavies = 0;

          for (int i = 0; i < amount.Length; i++)
          {
               if (amount.Length != amountLights + amountHeavies)
               {
                    if (amount.Length != amountLights + amountHeavies)
                    {
                         if (amount[i].transform.tag == "Light" ||
                             amount[i].transform.tag == "Player")
                         {
                              amountLights++;
                         }
                         if (amount[i].transform.tag == "Heavy")
                         {
                              amountHeavies++;
                         }
                    }
               }
          }

          TriggerButton();
     }

     public void TriggerButton()
     {
          if (weightType == WeightType.LIGHT)
          {
               if (amountLights == 1 &&
                   amountHeavies == 0)
               {
                    rightWeight = true;
               }
               else
               {
                    rightWeight = false;
               }
          }
          else if (weightType == WeightType.HEAVY)
          {
               if ((amountLights == 2 && amountHeavies == 0) ||
                   (amountHeavies == 1 && amountLights == 0))
               {
                    rightWeight = true;
               }
               else
               {
                    rightWeight = false;
               }
          }
     }

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          Gizmos.color = Color.blue;
          Gizmos.DrawWireSphere(transform.position, maxDistanceCheck);
     }
#endif
}
