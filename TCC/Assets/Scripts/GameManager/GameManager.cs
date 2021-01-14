using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public static GameManager instance;

     public SettingsData settingsData;
     public SaveSettings saveSettings;

     void Awake()
     {
          instance = this;
          DontDestroyOnLoad(gameObject);
     }
}
