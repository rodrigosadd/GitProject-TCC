using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleNineButtons : MonoBehaviour
{
     public Transform doorLeft;
     public Transform targetMoveLeft;
     public Transform targetInitialLPos;
     public Transform doorRight;
     public Transform targetMoveRight;
     public Transform targetInitialRPos;
     public WeightButton[] weightButtons;
     public int amountChosenButtons;
     public int[] chosenButtons;
     public float speedMoveDoor;
     private int[] checkEqualChosen;
     private int count;

     void Start()
     {
          chosenButtons = new int[amountChosenButtons];
          checkEqualChosen = new int[amountChosenButtons];
     }

     void Update()
     {
          DrawChosenButtons();
          CheckPressButtons();
     }

     public void DrawChosenButtons()
     {
          if (count != amountChosenButtons)
          {
               for (int i = 0; i < amountChosenButtons; i++)
               {
               Inicio:

                    int randonButton = Random.Range(0, weightButtons.Length);

                    for (int j = 0; j < amountChosenButtons; j++)
                    {
                         if (checkEqualChosen[j] == randonButton)
                         {
                              goto Inicio;
                         }
                    }

                    chosenButtons[i] = randonButton;
                    checkEqualChosen[i] = randonButton;
                    count++;
               }
          }
     }

     public void CheckPressButtons()
     {
          float distanceBetweenLeft = Vector3.Distance(targetMoveLeft.position, doorLeft.position);
          float distanceBetweenRight = Vector3.Distance(targetMoveRight.position, doorRight.position);
          float distanceBetweenInitialL = Vector3.Distance(targetInitialLPos.position, doorLeft.position);
          float distanceBetweenInitialR = Vector3.Distance(targetInitialRPos.position, doorRight.position);

          if (weightButtons[chosenButtons[0]].rightWeight &&
              weightButtons[chosenButtons[1]].rightWeight &&
              weightButtons[chosenButtons[2]].rightWeight)
          {
               if (distanceBetweenLeft > 0.5f &&
                   distanceBetweenRight > 0.5f)
               {
                    Vector3 directionDoorLeft = targetMoveLeft.position - doorLeft.position;
                    Vector3 directionDoorRight = targetMoveRight.position - doorRight.position;

                    doorLeft.position += directionDoorLeft.normalized * speedMoveDoor * Time.deltaTime;
                    doorRight.position += directionDoorRight.normalized * speedMoveDoor * Time.deltaTime;
               }
          }
          else
          {
               if (distanceBetweenLeft > 0.4f &&
                    distanceBetweenRight > 0.4f)
               {
                    Vector3 directionDoorLeft = targetInitialLPos.position - doorLeft.position;
                    Vector3 directionDoorRight = targetInitialRPos.position - doorRight.position;

                    doorLeft.position += directionDoorLeft.normalized * speedMoveDoor * Time.deltaTime;
                    doorRight.position += directionDoorRight.normalized * speedMoveDoor * Time.deltaTime;
               }
          }
     }
}
