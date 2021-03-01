using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
     public Rigidbody rbody;
     public Transform[] spotsToMovePlatform;
     public Transform interactObjectEmpty;
     public int spotToMove;
     public float speed;
     public float waitTimeToMove;
     private GameObject _currentBox;
     private float _countdown;
     private bool _canMove = true;
     private bool _canCopyVelocity;

     public virtual void MovementBetweenSpots()
     {
          if (_canMove)
          {
               rbody.MovePosition(Vector3.MoveTowards(transform.position, spotsToMovePlatform[spotToMove].position, speed * Time.fixedDeltaTime));
          }

          if (transform.position == spotsToMovePlatform[spotToMove].position)
          {
               if (_countdown == 0)
               {
                    _canMove = false;
                    spotToMove++;
               }
               if (spotToMove >= spotsToMovePlatform.Length)
               {
                    spotToMove = 0;
               }
          }
     }

     public void CountdownToMove()
     {
          if (!_canMove)
          {
               if (_countdown < 1)
               {
                    _countdown += Time.deltaTime / waitTimeToMove;
                    _canMove = false;
               }
               else
               {
                    _countdown = 0;
                    _canMove = true;
               }
          }
     }

     public void SetVelocityToCurrentBox()
     {
          if (_currentBox != null && !_currentBox.activeSelf)
          {
               _currentBox = null;
               return;
          }

          if (_canCopyVelocity)
          {
               _currentBox.GetComponent<Rigidbody>().velocity = rbody.velocity;
          }
     }

     void OnTriggerEnter(Collider other)
     {
          if (other.tag == "Light" ||
              other.tag == "Heavy")
          {
               _currentBox = other.gameObject;
               _canCopyVelocity = true;
               other.transform.parent = transform;
          }
     }

     void OnTriggerStay(Collider other)
     {
          if (other.tag == "Player")
          {
               PlayerController.instance.movement.controller.Move(rbody.velocity * Time.fixedDeltaTime);
          }
     }

     void OnTriggerExit(Collider other)
     {
          if (other.tag == "Light" ||
              other.tag == "Heavy")
          {
               _canCopyVelocity = false;
               _currentBox = null;
               other.transform.parent = interactObjectEmpty;
          }
     }
}
