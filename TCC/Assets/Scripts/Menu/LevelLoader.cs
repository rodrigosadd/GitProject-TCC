using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public static LevelLoader instance;
    public Animator transition;
    public float transitionTime = 1f;

    void Awake()
    {
        instance = this;
    }

    public void LoadNextLevel(int levelIndex)
    {
        StartCoroutine(LoadLevel(levelIndex));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        if(PlayerController.instance != null)
        {
            PlayerController.instance.movement.canMove = false;
        }

        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);

        if(PlayerController.instance != null)
        {
            PlayerController.instance.movement.canMove = true;
        }

        SceneManager.LoadScene(levelIndex);
    }
}
