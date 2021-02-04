using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
     }
}
