using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
     public int indexScene;
     private float _distanceBetween;

     void Update()
     {
          Load();
     }

     void Load()
     {
          _distanceBetween = Vector3.Distance(transform.position, PlayerController.instance.transform.position);

          if (_distanceBetween < 2)
          {
               SceneManager.LoadScene(indexScene);
          }
     }

}
