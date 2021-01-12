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
     public bool menuOpen = false;

     void Start()
     {
          SetDropdownValues();
          InitializeUI();
     }

     void Update()
     {
          Inputs();
     }

     public void Inputs()
     {
          if (Input.GetButtonDown("Cancel") && menuOpen)
          {
               Time.timeScale = 0;
               settings.gameObject.SetActive(true);
               Cursor.lockState = CursorLockMode.Confined;
               Cursor.visible = true;
               menuOpen = false;
          }
          else if (Input.GetButtonDown("Cancel") && !menuOpen)
          {
               Time.timeScale = 1;
               settings.gameObject.SetActive(false);
               Cursor.lockState = CursorLockMode.Locked;
               Cursor.visible = false;
               menuOpen = true;
          }
     }

     public void SetDropdownValues()
     {
          resolutionDropdown.ClearOptions();
          List<string> _options = new List<string>();
          for (int i = 0; i < GameManager.instance.instanceSettingsData.resolutionsString.Length; i++)
          {
               _options.Add(GameManager.instance.instanceSettingsData.resolutionsString[i]);
          }
          resolutionDropdown.AddOptions(_options);
     }

     public void Back()
     {
          Time.timeScale = 1;
          settings.gameObject.SetActive(false);
          Cursor.lockState = CursorLockMode.Locked;
          Cursor.visible = false;
          menuOpen = true;
     }

     public void InitializeUI()
     {
          resolutionDropdown.value = GameManager.instance.instanceSettingsData.indexResolution;
          qualityDropdown.value = GameManager.instance.instanceSettingsData.indexQuality;
          xSensitivitySlider.value = GameManager.instance.instanceSettingsData.xSensitivity;
          ySensitivitySlider.value = GameManager.instance.instanceSettingsData.ySensitivity;
          invertXToggle.isOn = GameManager.instance.instanceSettingsData.invertX == 1;
          invertYToggle.isOn = GameManager.instance.instanceSettingsData.invertY == 1;
          isFullscreen.isOn = GameManager.instance.instanceSettingsData.isFullscreen == 1;

          resolutionDropdown.onValueChanged.AddListener(SetResolutions);
          qualityDropdown.onValueChanged.AddListener(SetQuality);
          xSensitivitySlider.onValueChanged.AddListener(SetXSensitivity);
          ySensitivitySlider.onValueChanged.AddListener(SetYSensitivity);
          invertXToggle.onValueChanged.AddListener(SetInvertX);
          invertYToggle.onValueChanged.AddListener(SetInvertY);
          isFullscreen.onValueChanged.AddListener(SetFullscreen);
     }

     public void SetFullscreen(bool isFullscreen)
     {
          GameManager.instance.instanceSettingsData.isFullscreen = (isFullscreen) ? 1 : 0;
          GameManager.instance.instanceSettingsData.ApplySettings();
     }

     public void LoadScene(int IndexScene)
     {
          SceneManager.LoadScene(IndexScene);
     }

     public void QuitGame()
     {
          Application.Quit();
     }

     public void SetResolutions(int resolutionIndex)
     {
          GameManager.instance.instanceSettingsData.indexResolution = resolutionIndex;
          GameManager.instance.instanceSettingsData.ApplySettings();
     }

     public void SetQuality(int qualityIndex)
     {
          GameManager.instance.instanceSettingsData.indexQuality = qualityIndex;
          GameManager.instance.instanceSettingsData.ApplySettings();
     }

     public void SetXSensitivity(float sensitivity)
     {
          xSensitivitySlider.value = sensitivity;
          GameManager.instance.instanceSettingsData.xSensitivity = sensitivity;
          GameManager.instance.instanceSettingsData.ApplySettings();
     }
     public void SetYSensitivity(float sensitivity)
     {
          ySensitivitySlider.value = sensitivity;
          GameManager.instance.instanceSettingsData.ySensitivity = sensitivity;
          GameManager.instance.instanceSettingsData.ApplySettings();
     }

     public void SetInvertX(bool invert)
     {
          GameManager.instance.instanceSettingsData.invertX = (invert) ? 1 : 0;
          GameManager.instance.instanceSettingsData.ApplySettings();
     }
     public void SetInvertY(bool invert)
     {
          GameManager.instance.instanceSettingsData.invertY = (invert) ? 1 : 0;
          GameManager.instance.instanceSettingsData.ApplySettings();
     }
}
