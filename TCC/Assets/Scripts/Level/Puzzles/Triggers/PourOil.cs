using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PourOil : BreakableObject
{
     public GameObject oil;

     void Update()
     {
          ActiveOil();
          CheckLife();
     }

     public void ActiveOil()
     {
          if (triggerBroken)
          {
               oil.SetActive(true);
          }
     }
}
