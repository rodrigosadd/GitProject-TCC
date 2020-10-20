using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
     public float distanceBetween;

     void Update()
     {
          Load();
     }

     void Load()
     {
          distanceBetween = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

          if (distanceBetween < 4)
          {
               SceneManager.LoadScene(2);
          }
     }

}
