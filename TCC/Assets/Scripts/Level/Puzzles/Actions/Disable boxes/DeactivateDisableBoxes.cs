using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeactivateDisableBoxes : MonoBehaviour
{
     public DisableBoxes[] disableBoxes;
     public StayButton[] stayButtons;

     void Update()
     {
          CheckStayButtonsTriggers();
     }

     public void CheckStayButtonsTriggers()
     {
          bool _isComplete = true;

          for (int i = 0; i < stayButtons.Length; i++)
          {
               if (!stayButtons[i].triggerButton)
               {
                    _isComplete = false;
                    break;
               }
          }

          if (_isComplete)
          {
               Deactivate();
          }
          else
          {
               Active();
          }
     }

     public void Deactivate()
     {
          for (int i = 0; i < disableBoxes.Length; i++)
          {
               if (!disableBoxes[i].gameObject.activeSelf)
               {
                    break;
               }

               disableBoxes[i].gameObject.SetActive(false);
          }
     }

     public void Active()
     {
          for (int i = 0; i < disableBoxes.Length; i++)
          {
               if (disableBoxes[i].gameObject.activeSelf)
               {
                    break;
               }

               disableBoxes[i].gameObject.SetActive(true);
          }
     }
}
