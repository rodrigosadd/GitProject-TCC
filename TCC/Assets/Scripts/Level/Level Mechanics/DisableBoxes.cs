using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableBoxes : MonoBehaviour
{
     public Transform interactEmpty;
     private GameObject _currentBox;
     private bool _deactiveBox;
     private float _countdown;

     void Update()
     {
          ActiveBox();
     }

     void OnTriggerEnter(Collider collider)
     {
          if ((collider.tag == "Heavy" ||
              collider.tag == "Light") &&
              !_deactiveBox)
          {
               _deactiveBox = true;
               _currentBox = collider.gameObject;
               collider.transform.position = collider.GetComponent<WeightBox>().targetToRespawn.position;
               collider.transform.rotation = collider.GetComponent<WeightBox>().targetToRespawn.rotation;
               collider.gameObject.SetActive(false);
               collider.transform.parent = interactEmpty.transform;
          }
     }

     void ActiveBox()
     {
          if (_deactiveBox)
          {
               if (_countdown < 1)
               {
                    _countdown += Time.deltaTime / 1f;
               }
               else
               {
                    _countdown = 0;
                    _deactiveBox = false;
                    _currentBox.gameObject.SetActive(true);
               }
          }
     }
}
