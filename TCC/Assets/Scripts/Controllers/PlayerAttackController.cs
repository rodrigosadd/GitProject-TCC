using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
     public Transform targetAttack;
     public LayerMask layerEnemy;
     public GameObject[] trails;
     public int maxCombo;
     public int currentAttack;
     public float delayNextAttack;
     public float maxDistanceAttack;
     public float attackImpulse;
     public float distanceImpulse;
     public bool attaking;

#if UNITY_EDITOR
     public bool seeAttackRange;
#endif

     private float _currentMaxSpeed;
     private float _lastAttackTime;
     private Vector3 _finalImpulse;

     void LateUpdate()
     {
          InputsAttack();
          CanAttack();
          Dash();
     }

     public void InputsAttack()
     {
          if (Input.GetButtonDown("Fire1") && PlayerController.instance.movement.slowed == false && (PlayerController.instance.movement.stateCharacter == CharacterState.RUNNNING || PlayerController.instance.movement.stateCharacter == CharacterState.IDLE) && !PlayerController.instance.death.dead)
          {
               if (!attaking)
               {
                    _currentMaxSpeed = PlayerController.instance.movement.fixedMaxSpeed;
               }
               FirstAttack();
          }
     }

     public void FirstAttack()
     {
          if (!PlayerController.instance.death.dead)
          {
               _lastAttackTime = Time.time;
               currentAttack++;

               if (currentAttack == 1)
               {
                    PlayerController.instance.animator.SetBool("First Attack", true);
                    trails[0].SetActive(true);
                    trails[1].SetActive(true);
                    trails[2].SetActive(true);
               }
               currentAttack = Mathf.Clamp(currentAttack, 0, maxCombo);
          }
     }

     public void SecondAttack()
     {
          if (!PlayerController.instance.death.dead)
          {
               if (currentAttack >= 2)
               {
                    PlayerController.instance.animator.SetBool("Second Attack", true);
                    trails[0].SetActive(true);
                    trails[1].SetActive(true);
                    trails[2].SetActive(true);
               }
               else
               {
                    PlayerController.instance.animator.SetBool("First Attack", false);
                    trails[0].SetActive(false);
                    trails[1].SetActive(false);
                    trails[2].SetActive(false);
                    PlayerController.instance.movement.maxSpeed = _currentMaxSpeed;
                    attaking = false;
                    currentAttack = 0;
               }
          }
     }

     public void FinalAttack()
     {
          if (!PlayerController.instance.death.dead)
          {
               if (currentAttack >= 3)
               {
                    PlayerController.instance.animator.SetBool("Final Attack", true);
                    trails[0].SetActive(true);
                    trails[1].SetActive(true);
                    trails[2].SetActive(true);
               }
               else
               {
                    PlayerController.instance.animator.SetBool("Second Attack", false);
                    currentAttack = 0;
               }
          }
     }

     public void ResetAttack()
     {
          PlayerController.instance.animator.SetBool("First Attack", false);
          PlayerController.instance.animator.SetBool("Second Attack", false);
          PlayerController.instance.animator.SetBool("Final Attack", false);
          trails[0].SetActive(false);
          trails[1].SetActive(false);
          trails[2].SetActive(false);
          PlayerController.instance.movement.maxSpeed = _currentMaxSpeed;
          attaking = false;
          currentAttack = 0;
     }

     public void Attaking()
     {
          if (!PlayerController.instance.death.dead)
          {
               if (PlayerController.instance.movement.slowed == false)
               {
                    PlayerController.instance.movement.maxSpeed = 0f;
                    attaking = true;
               }
          }
     }

     public void Dash()
     {
          if (attaking)
          {
               if (_finalImpulse == Vector3.zero)
               {
                    _finalImpulse = PlayerController.instance.transform.position + (PlayerController.instance.characterGraphic.forward * distanceImpulse);
               }
               else
               {
                    PlayerController.instance.transform.position = Vector3.MoveTowards(PlayerController.instance.transform.position, _finalImpulse, attackImpulse * Time.deltaTime);

                    if (_finalImpulse == PlayerController.instance.transform.position)
                    {
                         _finalImpulse = Vector3.zero;
                         attaking = false;
                    }
               }
          }
     }

     public void AttackDetection()
     {
          Collider[] _hitEnemy = Physics.OverlapSphere(targetAttack.position, maxDistanceAttack, layerEnemy);

          foreach (Collider _hit in _hitEnemy)
          {
               _hit.transform.GetComponent<Enemy>().TakeHit();
          }
     }

     public void CanAttack()
     {
          if (Time.time - _lastAttackTime > delayNextAttack)
          {
               currentAttack = 0;
          }
     }

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          if (seeAttackRange)
          {
               Gizmos.color = Color.blue;
               Gizmos.DrawWireSphere(targetAttack.position, maxDistanceAttack);
          }
     }
#endif
}
