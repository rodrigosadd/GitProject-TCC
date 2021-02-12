using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Settings : MonoBehaviour
{
     public GameObject settings, confirmQuitPanel, quitButton, menuButton;
     public GameObject[] firstButtons;
     public Slider masterSlider;
     public Slider musicSlider;
     public Slider sfxSlider;
     public Dropdown resolutionDropdown;
     public Dropdown qualityDropdown;
     public Slider xSensitivitySlider;
     public Slider ySensitivitySlider;
     public Toggle invertXToggle;
     public Toggle invertYToggle;
     public Toggle isFullscreen;
     public UnityEngine.UI.Button backButton;
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
          ActiveButtons();
     }

     public void LoadScene(int indexScene)
     {
          GameManager.instance.saveSettings.Save();
          SceneManager.LoadScene(indexScene);
     }

     public void QuitGame()
     {
          Debug.Log("Quit!");
          Application.Quit();
     }

     public void Inputs()
     {
          if (!IsMenuScene())
          {
               if (Input.GetButtonDown("Cancel") && !settingsOpen)
               {
                    SetPauseTimeScale();
                    settings.gameObject.SetActive(true);
                    Cursor.lockState = CursorLockMode.Confined;
                    Cursor.visible = true;
                    settingsOpen = true;
                    GetSettingsOpen();
                    OpenSettings();
               }
               else if (Input.GetButtonDown("Cancel") && settingsOpen)
               {
                    SetNormalTimeScale();
                    settings.gameObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                    settingsOpen = false;
                    GetSettingsOpen();
                    GameManager.instance.saveSettings.Save();
                    CloseSettings();
               }
          }
     }

     public void Back()
     {
          if (!IsMenuScene())
          {
               SetNormalTimeScale();
               settings.gameObject.SetActive(false);
               Cursor.lockState = CursorLockMode.Locked;
               Cursor.visible = false;
               settingsOpen = false;
               GetSettingsOpen();
               GameManager.instance.saveSettings.Save();
               CloseSettings();
          }
          else
          {
               settings.gameObject.SetActive(false);
               settingsOpen = false;
               GetSettingsOpen();
               GameManager.instance.saveSettings.Save();
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

     public void SetNormalTimeScale()
     {
          Time.timeScale = 1;
     }

     public void SetPauseTimeScale()
     {
          Time.timeScale = 0;
     }

     public void OpenSettings()
     {
          if (settings.activeSelf)
          {
               EventSystem.current.SetSelectedGameObject(null);
               EventSystem.current.SetSelectedGameObject(firstButtons[0]);
          }
     }

     public void CloseSettings()
     {
          EventSystem.current.SetSelectedGameObject(null);
          EventSystem.current.SetSelectedGameObject(firstButtons[1]);
     }

     public void OpenConfirmQuit()
     {
          if (confirmQuitPanel.activeSelf)
          {
               EventSystem.current.SetSelectedGameObject(null);
               EventSystem.current.SetSelectedGameObject(firstButtons[2]);
          }
     }

     public void CloseConfirmQuit()
     {
          EventSystem.current.SetSelectedGameObject(null);
          EventSystem.current.SetSelectedGameObject(firstButtons[0]);
     }

     public void ActiveButtons()
     {
          if (!IsMenuScene())
          {
               quitButton.SetActive(true);
               menuButton.SetActive(true);
          }
          else
          {
               quitButton.SetActive(false);
               menuButton.SetActive(false);
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
               resolutionDropdown.value = GameManager.instance.settingsData.currentResolutionIndex;
               resolutionDropdown.RefreshShownValue();
          }
     }

     public void InitializeListerners()
     {
          masterSlider.onValueChanged.AddListener(GameManager.instance.audioSettings.MasterVolumeLevel);
          musicSlider.onValueChanged.AddListener(GameManager.instance.audioSettings.MusicVolumeLevel);
          sfxSlider.onValueChanged.AddListener(GameManager.instance.audioSettings.SFXVolumeLevel);
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
          masterSlider.value = GameManager.instance.settingsData.masterVolume;
          musicSlider.value = GameManager.instance.settingsData.musicVolume;
          sfxSlider.value = GameManager.instance.settingsData.SFXVolume;
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

          if (masterSlider.value != GameManager.instance.settingsData.masterVolume ||
              musicSlider.value != GameManager.instance.settingsData.musicVolume ||
              sfxSlider.value != GameManager.instance.settingsData.SFXVolume ||
              resolutionDropdown.value != GameManager.instance.settingsData.indexResolution ||
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
