using UnityEngine;
using FMODUnity;

public class Stats : MonoBehaviour
{
    public Camera3rdPerson camera3RdPerson;
    public Transform targetCam;
    public Transform targetShowItem;
    public float timeToReturnPlayerTarget = 2f;
    public float maxDistancePickedUp;
    public float speedRotate;
    public bool seeObject;
    private float countdownToReturnPlayerTarget;
    private bool canChangeTargetCam; 

    [EventRef] public string collectSound;
    public void RotateObject()
    {
        transform.Rotate(0f, speedRotate, 0f);
    }

    public void SeeObjectDrop()
     {
          if(seeObject)
          {
               canChangeTargetCam = true;
               camera3RdPerson.targetCamera = targetCam;
               camera3RdPerson.ConfigToShowObject();
               PlayerController.instance.movement.canMove = false;
               transform.position = targetShowItem.position;
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
                    gameObject.SetActive(false);
                    seeObject = false;
               }
          }
     }
}
