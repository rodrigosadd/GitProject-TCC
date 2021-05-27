using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackControllerMultiplayer : MonoBehaviour
{   
    public static PlayerAttackControllerMultiplayer instance;
    public Transform targetAttack;
    public LayerMask layerObjs;
    public GameObject[] trails;
    public int maxCombo;
    public int currentAttack;
    public float delayNextAttack;
    public float maxDistanceAttack;
    public float attackImpulse;
    public float distanceImpulse;
    public float timeToResetAttack;
    public bool attaking;
    public bool canAttack;
    private float _countdownReset;

#if UNITY_EDITOR
     public bool seeAttackRange;
#endif

     private float _currentMaxSpeed;
     private float _lastAttackTime;
     private Vector3 _finalImpulse;

     void Awake()
     {
          instance = this;
     }

     void LateUpdate()
     {
          if(canAttack)
          {
               InputsAttack();
               CanAttack();
               Impulse();
               CheckAttaking();
          }
     }

     public void InputsAttack()
     {
          if (Input.GetButtonDown("Attack") &&
              //!GameManager.instance.settingsData.settingsOpen &&
              !PlayerControllerMultiplayer.instance.levelMechanics.slowing &&
              !PlayerControllerMultiplayer.instance.death.dead &&
              !PlayerControllerMultiplayer.instance.levelMechanics.sliding &&
              PlayerControllerMultiplayer.instance.movement.isGrounded &&
              PlayerControllerMultiplayer.instance.movement.canMove)
          {
               if (!attaking)
               {
                    _currentMaxSpeed = PlayerControllerMultiplayer.instance.movement.fixedMaxSpeed;
               }

               FirstAttack();               
          }
     }

     public void FirstAttack()
     {
          if (!PlayerControllerMultiplayer.instance.death.dead &&
               currentAttack != 3 &&
               PlayerControllerMultiplayer.instance.movement.isGrounded)
          {
               _lastAttackTime = Time.time;
               currentAttack++;

               if (currentAttack == 1)
               {
                    PlayerAnimationControllerMultiplayer.instance.SetFirstAttack();
                    trails[0].SetActive(true);
                    trails[1].SetActive(true);
                    trails[2].SetActive(true);
               }
               currentAttack = Mathf.Clamp(currentAttack, 0, maxCombo);
          }
     }

     public void SecondAttack()
     {
          if (!PlayerControllerMultiplayer.instance.death.dead && PlayerControllerMultiplayer.instance.movement.canMove)
          {
               if (currentAttack >= 2)
               {
                    PlayerAnimationControllerMultiplayer.instance.SetSecondAttack();
                    trails[0].SetActive(true);
                    trails[1].SetActive(true);
                    trails[2].SetActive(true);
               }
               else
               {
                    PlayerAnimationControllerMultiplayer.instance.ResetFirstAttack();
                    trails[0].SetActive(false);
                    trails[1].SetActive(false);
                    trails[2].SetActive(false);
                    PlayerControllerMultiplayer.instance.movement.maxSpeed = _currentMaxSpeed;
                    attaking = false;
                    currentAttack = 0;
                    _finalImpulse = Vector3.zero;
               }
          }
     }

     public void FinalAttack()
     {
          if (!PlayerControllerMultiplayer.instance.death.dead && PlayerControllerMultiplayer.instance.movement.canMove)
          {
               if (currentAttack >= 3)
               {
                    PlayerAnimationControllerMultiplayer.instance.SetFinalAttack();
                    trails[0].SetActive(true);
                    trails[1].SetActive(true);
                    trails[2].SetActive(true);
               }
               else
               {
                    PlayerAnimationControllerMultiplayer.instance.ResetFirstAttack();
                    PlayerAnimationControllerMultiplayer.instance.ResetSecondAttack();
                    trails[0].SetActive(false);
                    trails[1].SetActive(false);
                    trails[2].SetActive(false);
                    PlayerControllerMultiplayer.instance.movement.maxSpeed = _currentMaxSpeed;
                    attaking = false;
                    currentAttack = 0;
                    _finalImpulse = Vector3.zero;
               }
          }
     }

     public void ResetAttack()
     {
          PlayerAnimationControllerMultiplayer.instance.ResetAttacks();
          trails[0].SetActive(false);
          trails[1].SetActive(false);
          trails[2].SetActive(false);
          PlayerControllerMultiplayer.instance.movement.maxSpeed = _currentMaxSpeed;
          attaking = false;
          currentAttack = 0;
          _finalImpulse = Vector3.zero;
     }

     public void CheckAttaking()
     {
          if (currentAttack != 0)
          {
               if (_countdownReset < 1)
               {
                    _countdownReset += Time.deltaTime / timeToResetAttack;
               }
               else
               {
                    _countdownReset = 0;
                    ResetAttack();
               }
          }
          else
          {
               _countdownReset = 0;
          }
     }

     public void Attacking()
     {
          if (!PlayerControllerMultiplayer.instance.death.dead)
          {
               if (PlayerControllerMultiplayer.instance.levelMechanics.slowing == false)
               {
                    PlayerControllerMultiplayer.instance.movement.maxSpeed = 0f;
                    attaking = true;
               }
          }
     }

     public void Impulse()
     {
          if (attaking)
          {
               if (_finalImpulse == Vector3.zero)
               {
                    _finalImpulse = PlayerControllerMultiplayer.instance.transform.position + (PlayerControllerMultiplayer.instance.characterGraphic.forward * distanceImpulse);
               }
               else
               {
                    if (!Physics.Raycast(PlayerControllerMultiplayer.instance.characterGraphic.position, PlayerControllerMultiplayer.instance.characterGraphic.forward, 1.18f))
                    {
                         PlayerControllerMultiplayer.instance.movement.velocity.y = 0;
                         PlayerControllerMultiplayer.instance.SetControllerPosition(Vector3.MoveTowards(PlayerControllerMultiplayer.instance.transform.position, _finalImpulse, attackImpulse * Time.deltaTime));

                         if (_finalImpulse == PlayerControllerMultiplayer.instance.transform.position)
                         {                              
                              _finalImpulse = Vector3.zero;
                              PlayerControllerMultiplayer.instance.levelMechanics.sliding = false;
                              attaking = false;
                         }
                    }
                    else
                    {
                         _finalImpulse = Vector3.zero;
                         PlayerControllerMultiplayer.instance.levelMechanics.sliding = false;
                         attaking = false;
                    }
               }
          }
     }

     public void AttackDetection()
     {
          Collider[] _hitObject = Physics.OverlapSphere(targetAttack.position, maxDistanceAttack, layerObjs);

          foreach (Collider _hit in _hitObject)
          {
               if (_hit.tag == "Enemy")
               {
                    _hit.transform.GetComponent<Enemy>().TakeHit();
               }
               else if (_hit.tag == "Breakable")
               {
                    _hit.transform.GetComponent<BreakableObject>().TakeHit();
               }
               else if(_hit.tag == "Boss")
               {
                    _hit.transform.GetComponentInParent<BossController>().TakeDamage();
               }
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
