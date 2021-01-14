using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSettings : MonoBehaviour
{
     void Start()
     {
          DontDestroyOnLoad(gameObject);
     }

     public void Save()
     {
          Debug.Log("Save settings");
          PlayerPrefs.SetInt("IndexResolution", GameManager.instance.settingsData.indexResolution);
          PlayerPrefs.SetInt("IndexQuality", GameManager.instance.settingsData.indexQuality);
          PlayerPrefs.SetFloat("xSensitivity", GameManager.instance.settingsData.xSensitivity);
          PlayerPrefs.SetFloat("ySensitivity", GameManager.instance.settingsData.ySensitivity);
          PlayerPrefs.SetInt("invertX", GameManager.instance.settingsData.invertX);
          PlayerPrefs.SetInt("invertY", GameManager.instance.settingsData.invertY);
          PlayerPrefs.SetInt("isFullscreen", GameManager.instance.settingsData.isFullscreen);

     }

     public void Load()
     {
          Debug.Log("Load settings");
          GameManager.instance.settingsData.indexResolution = PlayerPrefs.GetInt("IndexResolution");
          GameManager.instance.settingsData.indexQuality = PlayerPrefs.GetInt("IndexQuality");
          GameManager.instance.settingsData.xSensitivity = PlayerPrefs.GetFloat("xSensitivity");
          GameManager.instance.settingsData.ySensitivity = PlayerPrefs.GetFloat("ySensitivity");
          GameManager.instance.settingsData.invertX = PlayerPrefs.GetInt("invertX");
          GameManager.instance.settingsData.invertY = PlayerPrefs.GetInt("invertY");
          GameManager.instance.settingsData.isFullscreen = PlayerPrefs.GetInt("isFullscreen");
     }
}
