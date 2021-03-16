using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
     public static GameManager instance;
     public SettingsData settingsData;
     public SaveSettings saveSettings;
     public AudioSettings audioSettings;

     void Awake()
     {
          instance = this;
          DontDestroyOnLoad(gameObject);
          SceneManager.sceneLoaded += OnSceneLoaded;
     }

     public void LoadScene(int index, int lastScene)
     {    
          if(InRuntimePersistantData.Instance.lastLoadedLevel == -1)
          {
               InRuntimePersistantData.Instance.lastLoadedLevel = lastScene;
          }

          SceneManager.LoadScene(index);
     }

     public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
     {
          if(scene.buildIndex != InRuntimePersistantData.Instance.lastLoadedLevel)
          {    
               return;
          }

          var cachedComponents = InRuntimePersistantData.Instance.cachedPersistenteComponentInfo;
          
          if(cachedComponents == null)
          {
               return;
          }

          var currentLevelManager = GameObject.FindObjectOfType<LevelManager>();

          if(currentLevelManager != null)
          {
               foreach (var item in cachedComponents)
               {    
                    GameObject currentObj = null;

                    foreach (var Component in currentLevelManager.inScenePersitenteComponente)
                    {
                         if(Component.name == item.cachedObjectName)
                         {
                              currentObj = Component.gameObject;
                              break;
                         }
                    }

                    if(currentObj == null)
                    {
                         Instantiate(item.referencePrefab, item.lastPosition, Quaternion.Euler(item.lastRotation));
                    }
                    else
                    {
                         currentObj.transform.position = item.lastPosition;
                         currentObj.transform.rotation = Quaternion.Euler(item.lastRotation);
                         currentObj.SetActive(true);
                    }
               }
          }
          else
          {
               Debug.Log("Current Level Manager null");
          }
          
          InRuntimePersistantData.Instance.cachedPersistenteComponentInfo = new List<InRuntimePersistenteComponentInfo>();
          InRuntimePersistantData.Instance.lastLoadedLevel = -1;
     }
}
