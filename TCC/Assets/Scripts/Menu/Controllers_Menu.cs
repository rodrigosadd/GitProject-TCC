using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;

public class Controllers_Menu : MonoBehaviour
{

    public void LoadScene(int Num_Scene)
    {
        SceneManager.LoadScene(Num_Scene);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
