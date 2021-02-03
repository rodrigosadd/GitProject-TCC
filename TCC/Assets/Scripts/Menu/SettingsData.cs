using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsData : MonoBehaviour
{
     public string[] resolutionsString;
     public int indexResolution;
     public int currentResolutionIndex = 0;
     public int indexQuality;
     public float xSensitivity;
     public float ySensitivity;
     public int invertX;
     public int invertY;
     public int isFullscreen;
     public bool settingsOpen;
     private Resolution[] _resolutions;

     void Start()
     {
          DontDestroyOnLoad(gameObject);
          GetResolutions();
          GameManager.instance.saveSettings.Load();
          GameManager.instance.settingsData.ApplySettings();
     }

     public void GetResolutions()
     {
          _resolutions = Screen.resolutions;

          List<string> options = new List<string>();

          for (int i = 0; i < _resolutions.Length; i++)
          {
               string option = _resolutions[i].width + "x" + _resolutions[i].height;
               options.Add(option);

               if (_resolutions[i].width == Screen.currentResolution.width &&
                  _resolutions[i].height == Screen.currentResolution.height)
               {
                    currentResolutionIndex = i;
               }
          }
          resolutionsString = options.ToArray();
     }

     public void ApplySettings()
     {
          //Set Resolution
          if (_resolutions == null)
          {
               GetResolutions();
          }
          Resolution resolution = _resolutions[indexResolution];
          Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

          //Set Quality
          QualitySettings.SetQualityLevel(indexQuality);

          //Set FullScreen
          Screen.fullScreen = (isFullscreen == 1) ? true : false;


          if (Camera3rdPerson.instance != null)
          {
               //Set Sensitivity X & Y
               Camera3rdPerson.instance.inputSensitivityX = xSensitivity;
               Camera3rdPerson.instance.inputSensitivityY = ySensitivity;

               //Set Invert
               Camera3rdPerson.instance.invertAxisX = (invertX == 1) ? true : false;
               Camera3rdPerson.instance.invertAxisY = (invertY == 1) ? true : false;
          }
     }
}