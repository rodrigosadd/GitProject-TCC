using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour
{
     public GameObject[] firstButtons;
     public GameObject settingsPanel, creditsPanel, quitPanel;

     public void LoadScene(int IndexScene)
     {
          SceneManager.LoadScene(IndexScene);
     }

     public void QuitGame()
     {
          Application.Quit();
     }

     public void OpenSettings()
     {
          if (settingsPanel.activeSelf)
          {
               EventSystem.current.SetSelectedGameObject(null);
               EventSystem.current.SetSelectedGameObject(firstButtons[0]);
          }
          else
          {
               EventSystem.current.SetSelectedGameObject(null);
               EventSystem.current.SetSelectedGameObject(firstButtons[1]);
          }
     }

     public void OpenCredits()
     {
          if (creditsPanel.activeSelf)
          {
               EventSystem.current.SetSelectedGameObject(null);
               EventSystem.current.SetSelectedGameObject(firstButtons[2]);
          }
          else
          {
               EventSystem.current.SetSelectedGameObject(null);
               EventSystem.current.SetSelectedGameObject(firstButtons[3]);
          }
     }

     public void OpenQuit()
     {
          if (quitPanel.activeSelf)
          {
               EventSystem.current.SetSelectedGameObject(null);
               EventSystem.current.SetSelectedGameObject(firstButtons[4]);
          }
          else
          {
               EventSystem.current.SetSelectedGameObject(null);
               EventSystem.current.SetSelectedGameObject(firstButtons[5]);
          }
     }
}
