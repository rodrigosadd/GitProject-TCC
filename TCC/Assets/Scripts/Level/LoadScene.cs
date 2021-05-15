using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
     public Transform playerStartPosition;
     public int currentScene;
     public int indexScene;

     void OnTriggerEnter(Collider other)
     {
          GameManager.instance.savePlayerStats.Save(); 
          InRuntimePersistantData.CachePersistenteComponents(playerStartPosition.position);
          GameManager.instance.LoadScene(indexScene, currentScene);
     }
}
