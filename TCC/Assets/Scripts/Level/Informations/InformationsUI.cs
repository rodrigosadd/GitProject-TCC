using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InformationsUI : MonoBehaviour
{
     public GameObject informationsObj;
     public Text descriptionText;
     public bool trigged;

     [Multiline(8)]
     public string description;

     void Start()
     {
          SetDescription();
     }

     void Update()
     {
          CloseInformations();
     }

     public void SetDescription()
     {
          descriptionText.text = description;
     }

     public void CloseInformations()
     {
          if (Input.GetButtonDown("Fire3") && trigged)
          {
               informationsObj.SetActive(false);
               gameObject.SetActive(false);
               trigged = false;
          }
     }

     void OnTriggerEnter(Collider collider)
     {
          if (collider.transform.tag == "Player")
          {
               informationsObj.SetActive(true);
               trigged = true;
          }
     }
}
