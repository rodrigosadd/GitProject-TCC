using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleWeightsButtonsDoor : OpenDoor
{
     public WeightButton[] weightButtons;
     public WeightButton[] fakeWeightButtons;
     public GameObject[] objAnswer;
     public GameObject[] objCorrectAnswer;

     void Start()
     {
          StartPositionTargets();
     }

     void Update()
     {
          CheckPressButtons();
          ShowAnswer();
          ShowButtonPressed();
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

          for (int i = 0; i < fakeWeightButtons.Length; i++)
          {
               if (fakeWeightButtons[i].rightWeight)
               {
                    isComplete = false;
                    break;
               }
          }

          if (isComplete)
          {
               CanOpenDoor();
          }
          else
          {
               CanCloseDoor();
          }
     }

     public void ShowAnswer()
     {
          for (int i = 0; i < weightButtons.Length; i++)
          {
               objAnswer[i].SetActive(true);
          }
     }

     public void ShowButtonPressed()
     {
          for (int i = 0; i < weightButtons.Length; i++)
          {
               objAnswer[i].SetActive(!weightButtons[i].rightWeight);
               objCorrectAnswer[i].SetActive(weightButtons[i].rightWeight);
          }
     }
}