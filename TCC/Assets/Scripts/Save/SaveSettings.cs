using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSettings : MonoBehaviour
{
     void Start()
     {
          DontDestroyOnLoad(gameObject);
     }

     [ContextMenu("Clean Settings")]
     public void CleanSettings()
     {
          Debug.Log("Clean Settings");
          PlayerPrefs.DeleteKey("IndexMaster");
          PlayerPrefs.DeleteKey("IndexMusic");
          PlayerPrefs.DeleteKey("IndexSFX");
          PlayerPrefs.DeleteKey("IndexResolution");
          PlayerPrefs.DeleteKey("IndexQuality");
          PlayerPrefs.DeleteKey("xSensitivity");
          PlayerPrefs.DeleteKey("ySensitivity");
          PlayerPrefs.DeleteKey("invertX");
          PlayerPrefs.DeleteKey("invertY");
          PlayerPrefs.DeleteKey("isFullscreen");          
     }

     public void Save()
     {
          Debug.Log("Save settings");
          PlayerPrefs.SetFloat("IndexMaster", GameManager.instance.settingsData.masterVolume);
          PlayerPrefs.SetFloat("IndexMusic", GameManager.instance.settingsData.musicVolume);
          PlayerPrefs.SetFloat("IndexSFX", GameManager.instance.settingsData.SFXVolume);
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

          if(PlayerPrefs.HasKey("IndexMaster") == true)
          {
               GameManager.instance.settingsData.masterVolume = PlayerPrefs.GetFloat("IndexMaster");
          }
          else
          {
               GameManager.instance.settingsData.masterVolume = 1f;
          }

          if(PlayerPrefs.HasKey("IndexMusic") == true)
          {
               GameManager.instance.settingsData.musicVolume = PlayerPrefs.GetFloat("IndexMusic");
          }
          else
          {
               GameManager.instance.settingsData.musicVolume = 1f;
          }

          if(PlayerPrefs.HasKey("IndexSFX") == true)
          {
               GameManager.instance.settingsData.SFXVolume = PlayerPrefs.GetFloat("IndexSFX");
          }
          else
          {
               GameManager.instance.settingsData.SFXVolume = 1f;
          }
                    
          GameManager.instance.settingsData.indexResolution = PlayerPrefs.GetInt("IndexResolution");

          if(PlayerPrefs.HasKey("IndexQuality") == true)
          {
               GameManager.instance.settingsData.indexQuality = PlayerPrefs.GetInt("IndexQuality");
          }
          else
          {
               GameManager.instance.settingsData.indexQuality = 2;
          }

          if(PlayerPrefs.HasKey("xSensitivity") == true)
          {
               GameManager.instance.settingsData.xSensitivity = PlayerPrefs.GetFloat("xSensitivity");
          }
          else
          {
               GameManager.instance.settingsData.xSensitivity = 200f;
          }
          
          if(PlayerPrefs.HasKey("ySensitivity") == true)
          {
               GameManager.instance.settingsData.ySensitivity = PlayerPrefs.GetFloat("ySensitivity");
          }
          else
          {
               GameManager.instance.settingsData.ySensitivity = 200f;
          }

          if(PlayerPrefs.HasKey("invertX") == true)
          {
               GameManager.instance.settingsData.invertX = PlayerPrefs.GetInt("invertX");
          }
          else
          {
               GameManager.instance.settingsData.invertX = 0;
          }

          if(PlayerPrefs.HasKey("invertY") == true)
          {
               GameManager.instance.settingsData.invertY = PlayerPrefs.GetInt("invertY");
          }
          else
          {
               GameManager.instance.settingsData.invertY = 0;
          }

          if(PlayerPrefs.HasKey("isFullscreen") == true)
          {
               GameManager.instance.settingsData.isFullscreen = PlayerPrefs.GetInt("isFullscreen");
          }
          else
          {
               GameManager.instance.settingsData.isFullscreen = 1;
          }
     }
}
