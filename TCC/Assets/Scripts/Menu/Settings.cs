using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
     public GameObject settings;
     public Dropdown resolutionDropdown;
     public Slider xSensitivitySlider;
     public Slider ySensitivitySlider;
     public Toggle invertXToggle;
     public Toggle invertYToggle;
     private bool _menuOpen = true;

     private Resolution[] _resolutions;

     void Start()
     {
          //DontDestroyOnLoad(gameObject);
          GetResolutions();
     }

     void Update()
     {
          Inputs();
     }

     public void Inputs()
     {
          if (Input.GetButtonDown("Cancel") && _menuOpen)
          {
               Time.timeScale = 0;
               settings.gameObject.SetActive(true);
               Cursor.lockState = CursorLockMode.Confined;
               Cursor.visible = true;
               _menuOpen = false;
          }
          else if (Input.GetButtonDown("Cancel") && !_menuOpen)
          {
               Time.timeScale = 1;
               settings.gameObject.SetActive(false);
               Cursor.lockState = CursorLockMode.Locked;
               Cursor.visible = false;
               _menuOpen = true;
          }
     }

     public void Back()
     {
          Time.timeScale = 1;
          settings.gameObject.SetActive(false);
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
          _menuOpen = true;
     }

     public void SetFullscrenn(bool isFullscreen)
     {
          Screen.fullScreen = isFullscreen;
     }

     public void LoadScene(int IndexScene)
     {
          SceneManager.LoadScene(IndexScene);
     }

     public void QuitGame()
     {
          Application.Quit();
     }

     private void GetResolutions()
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

     public void SetXSensitivity(float sensitivity)
     {
          xSensitivitySlider.value = sensitivity;
          Camera3rdPerson.instance.inputSensitivityX = sensitivity;
     }
     public void SetYSensitivity(float sensitivity)
     {
          ySensitivitySlider.value = sensitivity;
          Camera3rdPerson.instance.inputSensitivityY = sensitivity;
     }

     public void SetInvertX(bool invert)
     {
          Camera3rdPerson.instance.invertAxisX = invert;
     }
     public void SetInvertY(bool invert)
     {
          Camera3rdPerson.instance.invertAxisY = invert;
     }
}
