using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleButtons : MonoBehaviour
{
     public Transform doorLeft;
     public Transform targetInitialLPos;
     public Transform targetMoveLeft;
     public Transform doorRight;
     public Transform targetInitialRPos;
     public Transform targetMoveRight;
     public WeightButton[] weightButtons;
     public WeightButton[] fakeWeightButtons;
     public GameObject[] objAnswer;
     public GameObject[] objCorrectAnswer;
     public float speedMoveDoor;

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
               OpenDoor();
          }
          else
          {
               CloseDoor();
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

     public void OpenDoor()
     {
          doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetMoveLeft.position, speedMoveDoor * Time.deltaTime);
          doorRight.position = Vector3.MoveTowards(doorRight.position, targetMoveRight.position, speedMoveDoor * Time.deltaTime);
     }

     public void CloseDoor()
     {
          doorLeft.position = Vector3.MoveTowards(doorLeft.position, targetInitialLPos.position, speedMoveDoor * Time.deltaTime);
          doorRight.position = Vector3.MoveTowards(doorRight.position, targetInitialRPos.position, speedMoveDoor * Time.deltaTime);
     }
}