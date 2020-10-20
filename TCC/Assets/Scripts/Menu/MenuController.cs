using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class MenuController : MonoBehaviour
{
     public void LoadScene(int Num_Scene)
     {
          SceneManager.LoadScene(Num_Scene);
     }

     public void QuitGame()
     {
          Application.Quit();
     }
}
