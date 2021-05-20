using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressWeightsButtonsPlatform : MonoBehaviour
{
     public static List<string> allKeys = new List<string>();
     public Camera3rdPerson camera3RdPerson;
     public Transform targetCam;
     public MovePlatform[] platforms;
     public WeightButton[] weightButtons;
     public float timeToReturnPlayerTarget = 2f;
     public bool seeObject;
     public string key;
     public bool canCheckKey = true;
     private bool isTriggered;
     private float countdownToReturnPlayerTarget;
     private bool canChangeTargetCam; 

     void Start()
     {
          if(canCheckKey)
          {
               allKeys.Add(key);
               isTriggered = PlayerPrefs.HasKey(key) && (PlayerPrefs.GetInt(key) == 1);
          }
     }

     void Update()
     {
          CheckPressButtons();
          CountdownToReturnPlayerTarget();
     }

     public void CheckPressButtons()
     {
          bool isComplete = true;

          for (int i = 0; i < weightButtons.Length; i++)
          {
               if (!weightButtons[i].rightWeight)
               {
                    isComplete = false;
                    break;
               }
          }

          if (isComplete)
          {
               MovePlatforms();
               SeeObjectDrop();
          }
          else
          {
               StopMovePlatforms();
          }
     }

     public void MovePlatforms()
     {
          for (int i = 0; i < platforms.Length; i++)
          {
               platforms[i].Initialize();               
          }
     }

     public void StopMovePlatforms()
     {
          for (int i = 0; i < platforms.Length; i++)
          {
               platforms[i].canMove = false;
               platforms[i].hasRestarted = false;
          }
     }

     public void SeeObjectDrop()
     {
          if(seeObject && !isTriggered)
          {
               if(canCheckKey)
               {
                    isTriggered = true;
                    PlayerPrefs.SetInt(key, isTriggered == true? 1 : 0);
               }

               canChangeTargetCam = true;
               camera3RdPerson.targetCamera = targetCam;
               camera3RdPerson.ConfigToShowObject();
               PlayerController.instance.movement.canMove = false;
          }
     }

     public void CountdownToReturnPlayerTarget()
     {
          if(canChangeTargetCam)
          {
               if(countdownToReturnPlayerTarget < 1)
               {
                    countdownToReturnPlayerTarget += Time.deltaTime / timeToReturnPlayerTarget;
               }
               else
               {
                    canChangeTargetCam = false;
                    camera3RdPerson.targetCamera = PlayerController.instance.movement.targetCam;
                    camera3RdPerson.ResetConfig();
                    PlayerController.instance.movement.canMove = true;
                    seeObject = false;
               }
          }
     }

     public static void ClearKeys()
     {
          for (int i = 0; i < allKeys.Count; i++)
          {
              PlayerPrefs.DeleteKey(allKeys[i]);
          }

          allKeys = new List<string>();
     }
}
