using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
     public Dropdown resolutionDropdown;

     private Resolution[] _resolutions;

     void Start()
     {
          GetResolutions();
     }

     public void LoadScene(int IndexScene)
     {
          SceneManager.LoadScene(IndexScene);
     }

     public void QuitGame()
     {
          Application.Quit();
     }

     public void SetFullscrenn(bool isFullscreen)
     {
          Screen.fullScreen = isFullscreen;
     }

     void GetResolutions()
     {
          _resolutions = Screen.resolutions;

          resolutionDropdown.ClearOptions();

          List<string> options = new List<string>();

          int currentResolutionIndex = 0;
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

          resolutionDropdown.AddOptions(options);
          resolutionDropdown.value = currentResolutionIndex;
          resolutionDropdown.RefreshShownValue();
     }

     public void SetResolutions(int resolutionIndex)
     {
          Resolution resolution = _resolutions[resolutionIndex];
          Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
     }

     public void SetQuality(int qualityIndex)
     {
          QualitySettings.SetQualityLevel(qualityIndex);
     }
}
