using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
     public static GameManager instance;
     public SettingsData settingsData;
     public SaveSettings saveSettings;
     public PlayerStatsData playerStatsData;
     public SavePlayerStats savePlayerStats;
     public AudioSettings audioSettings;

     void Awake()
     {
          instance = this;
          DontDestroyOnLoad(gameObject);
          SceneManager.sceneLoaded += OnSceneLoaded;
     }

     public void LoadScene(int index, int lastScene)
     {    
          if(InRuntimePersistentData.Instance.lastLoadedLevel == -1)
          {
               InRuntimePersistentData.Instance.lastLoadedLevel = lastScene;
          }
          LevelLoader.instance.LoadNextLevel(index);
     }

     public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
     {
          GameManager.instance.savePlayerStats.Load();
          GameManager.instance.playerStatsData.ApplySettings();
          
          if(InRuntimePersistentData.Instance == null || scene.buildIndex != InRuntimePersistentData.Instance.lastLoadedLevel)
          {    
               return;
          }

          var cachedComponents = InRuntimePersistentData.Instance.cachedPersistenteComponentInfo;
          
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
          
          InRuntimePersistentData.Instance.cachedPersistenteComponentInfo = new List<InRuntimePersistentComponentInfo>();
          InRuntimePersistentData.Instance.lastLoadedLevel = -1;          
     }

     public void ClearPersistantDataPosition()
     {
          if(InRuntimePersistentData.Instance == null)
          {    
               return;
          }
          
          InRuntimePersistentData.Instance.cachedPersistenteComponentInfo = new List<InRuntimePersistentComponentInfo>();
     }

     public void ClearKeysPressWBP()
     {
          PressWeightsButtonsPlatform.ClearKeys();
     }
}
