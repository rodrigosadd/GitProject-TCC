using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
     public GameObject settings;
     public Dropdown resolutionDropdown;
     public Dropdown qualityDropdown;
     public Slider xSensitivitySlider;
     public Slider ySensitivitySlider;
     public Toggle invertXToggle;
     public Toggle invertYToggle;
     public Toggle isFullscreen;
     public Button backButton;
     public bool settingsOpen = false;

     void Start()
     {
          InitializeListerners();
          SetupUIValues();
          GameManager.instance.settingsData.ApplySettings();
     }

     void Update()
     {
          Inputs();
          SetDropdownValues();
          CheckUIValues();
     }

     public void Inputs()
     {
          if (!IsMenuScene())
          {
               if (Input.GetButtonDown("Cancel") && !settingsOpen)
               {
                    Time.timeScale = 0;
                    settings.gameObject.SetActive(true);
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    settingsOpen = true;
                    GetSettingsOpen();
               }
               else if (Input.GetButtonDown("Cancel") && settingsOpen)
               {
                    Time.timeScale = 1;
                    settings.gameObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    settingsOpen = false;
                    GetSettingsOpen();
                    GameManager.instance.saveSettings.Save();
               }
          }
     }

     public void Back()
     {
          if (!IsMenuScene())
          {
               Time.timeScale = 1;
               settings.gameObject.SetActive(false);
               Cursor.lockState = CursorLockMode.Locked;
               Cursor.visible = false;
               settingsOpen = false;
               GetSettingsOpen();
          }
     }

     public void GetSettingsOpen()
     {
          GameManager.instance.settingsData.settingsOpen = settingsOpen;
     }

     public bool IsMenuScene()
     {
          if (SceneManager.GetActiveScene().name == "Menu")
          {
               return true;
          }
          else
          {
               return false;
          }
     }

     public void SetDropdownValues()
     {
          if (resolutionDropdown.options.Count != GameManager.instance.settingsData.resolutionsString.Length)
          {
               resolutionDropdown.ClearOptions();
               List<string> _options = new List<string>();
               for (int i = 0; i < GameManager.instance.settingsData.resolutionsString.Length; i++)
               {
                    _options.Add(GameManager.instance.settingsData.resolutionsString[i]);
               }

               resolutionDropdown.AddOptions(_options);
          }
     }

     public void InitializeListerners()
     {
          resolutionDropdown.onValueChanged.AddListener(SetResolutions);
          qualityDropdown.onValueChanged.AddListener(SetQuality);
          xSensitivitySlider.onValueChanged.AddListener(SetXSensitivity);
          ySensitivitySlider.onValueChanged.AddListener(SetYSensitivity);
          invertXToggle.onValueChanged.AddListener(SetInvertX);
          invertYToggle.onValueChanged.AddListener(SetInvertY);
          isFullscreen.onValueChanged.AddListener(SetFullscreen);
          backButton.onClick.AddListener(GameManager.instance.saveSettings.Save);
     }

     public void SetupUIValues()
     {
          resolutionDropdown.value = GameManager.instance.settingsData.indexResolution;
          qualityDropdown.value = GameManager.instance.settingsData.indexQuality;
          xSensitivitySlider.value = GameManager.instance.settingsData.xSensitivity;
          ySensitivitySlider.value = GameManager.instance.settingsData.ySensitivity;
          invertXToggle.isOn = GameManager.instance.settingsData.invertX == 1;
          invertYToggle.isOn = GameManager.instance.settingsData.invertY == 1;
          isFullscreen.isOn = GameManager.instance.settingsData.isFullscreen == 1;
     }

     public void CheckUIValues()
     {
          bool invertXtoggle = GameManager.instance.settingsData.invertX == 1;
          bool invertYtoggle = GameManager.instance.settingsData.invertY == 1;
          bool isFullScreenToggle = GameManager.instance.settingsData.isFullscreen == 1;

          if (resolutionDropdown.value != GameManager.instance.settingsData.indexResolution ||
             qualityDropdown.value != GameManager.instance.settingsData.indexQuality ||
             xSensitivitySlider.value != GameManager.instance.settingsData.xSensitivity ||
             ySensitivitySlider.value != GameManager.instance.settingsData.ySensitivity ||
             invertXToggle.isOn != invertXtoggle ||
             invertYToggle.isOn != invertYtoggle ||
             isFullscreen.isOn != isFullScreenToggle)
          {
               SetupUIValues();
          }
     }

     public void SetFullscreen(bool isFullscreen)
     {
          GameManager.instance.settingsData.isFullscreen = (isFullscreen) ? 1 : 0;
          GameManager.instance.settingsData.ApplySettings();
     }

     public void LoadScene(int indexScene)
     {
          SceneManager.LoadScene(indexScene);
     }

     public void QuitGame()
     {
          Application.Quit();
     }

     public void SetResolutions(int resolutionIndex)
     {
          GameManager.instance.settingsData.indexResolution = resolutionIndex;
          GameManager.instance.settingsData.ApplySettings();
     }

     public void SetQuality(int qualityIndex)
     {
          GameManager.instance.settingsData.indexQuality = qualityIndex;
          GameManager.instance.settingsData.ApplySettings();
     }

     public void SetXSensitivity(float sensitivity)
     {
          xSensitivitySlider.value = sensitivity;
          GameManager.instance.settingsData.xSensitivity = sensitivity;
          GameManager.instance.settingsData.ApplySettings();
     }
     public void SetYSensitivity(float sensitivity)
     {
          ySensitivitySlider.value = sensitivity;
          GameManager.instance.settingsData.ySensitivity = sensitivity;
          GameManager.instance.settingsData.ApplySettings();
     }

     public void SetInvertX(bool invert)
     {
          GameManager.instance.settingsData.invertX = (invert) ? 1 : 0;
          GameManager.instance.settingsData.ApplySettings();
     }
     public void SetInvertY(bool invert)
     {
          GameManager.instance.settingsData.invertY = (invert) ? 1 : 0;
          GameManager.instance.settingsData.ApplySettings();
     }
}
