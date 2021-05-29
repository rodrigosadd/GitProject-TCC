using UnityEngine;
using System.Collections;
using FMODUnity;

public class Stats : MonoBehaviour
{
    public MeshRenderer meshRenderer;
    public Collider coll;
    public GameObject itemInformationsPanel;
    public Camera3rdPerson camera3RdPerson;
    public Transform targetCam;
    public Transform targetShowItem;
    public float timeToReturnPlayerTarget = 2f;
    public float maxDistancePickedUp;
    public float speedRotate;
    private bool _canCloseSeeObj;
    private bool _canPickUpObj = true;

    [EventRef] public string collectSound;
    public void RotateObject()
    {
        transform.Rotate(0f, speedRotate, 0f);
    }

    public void SeeObjectDrop()
     {   
          if(!PlayerController.instance.levelMechanics.pickingUpItem && _canPickUpObj)
          {
               _canPickUpObj = false;
               camera3RdPerson.targetCamera = targetCam;
               camera3RdPerson.ConfigToShowObject();
               PlayerController.instance.movement.canMove = false;
               transform.position = targetShowItem.position;
               PlayerAnimationController.instance.SetPowerUp();
               PlayerController.instance.levelMechanics.pickingUpItem = true;
               _canCloseSeeObj = true;
               itemInformationsPanel.SetActive(true);
          }
     }

     public void ReturnPlayerTarget()
     {
          if(Input.GetButtonDown("Cancel") && _canCloseSeeObj)
          {
               camera3RdPerson.targetCamera = PlayerController.instance.movement.targetCam;
               camera3RdPerson.ResetConfig();
               PlayerController.instance.movement.canMove = true;
               PlayerController.instance.movement.canMove = true;
               _canCloseSeeObj = false;
               itemInformationsPanel.SetActive(false);
               meshRenderer.enabled = false;
               coll.enabled = false;
               StartCoroutine(Delay());
          }
     }

     IEnumerator Delay()
     {
          yield return new WaitForSeconds(1f);
          PlayerController.instance.levelMechanics.pickingUpItem = false;
     }
}
