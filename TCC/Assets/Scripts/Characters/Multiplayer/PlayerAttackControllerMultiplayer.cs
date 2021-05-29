using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

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
    public PlayerControllerMultiplayer multiplayerController;
    public PlayerAnimationControllerMultiplayer animationController;
    private PhotonView photonView;
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
          photonView = GetComponent<PhotonView>();
     }

     void LateUpdate()
     {
          if (photonView.IsMine) {
               if(canAttack)
               {
                    photonView.RPC("InputsAttack", RpcTarget.AllBuffered);
                    photonView.RPC("CanAttack", RpcTarget.AllBuffered);
                    photonView.RPC("Impulse", RpcTarget.AllBuffered);
                    photonView.RPC("CheckAttaking", RpcTarget.AllBuffered);
               }
          }
     }

     [PunRPC]
     public void InputsAttack()
     {
          if (Input.GetButtonDown("Attack") &&
              //!GameManager.instance.settingsData.settingsOpen &&
              !multiplayerController.levelMechanics.slowing &&
              !multiplayerController.death.dead &&
              !multiplayerController.levelMechanics.sliding &&
              multiplayerController.movement.isGrounded &&
              multiplayerController.movement.canMove)
          {
               if (!attaking)
               {
                    _currentMaxSpeed = multiplayerController.movement.fixedMaxSpeed;
               }

               FirstAttack();               
          }
     }

     public void FirstAttack()
     {
          if (!multiplayerController.death.dead &&
               currentAttack != 3 &&
               multiplayerController.movement.isGrounded)
          {
               _lastAttackTime = Time.time;
               currentAttack++;

               if (currentAttack == 1)
               {
                    animationController.SetFirstAttack();
                    trails[0].SetActive(true);
                    trails[1].SetActive(true);
                    trails[2].SetActive(true);
               }
               currentAttack = Mathf.Clamp(currentAttack, 0, maxCombo);
          }
     }

     public void SecondAttack()
     {
          if (!multiplayerController.death.dead && multiplayerController.movement.canMove)
          {
               if (currentAttack >= 2)
               {
                    animationController.SetSecondAttack();
                    trails[0].SetActive(true);
                    trails[1].SetActive(true);
                    trails[2].SetActive(true);
               }
               else
               {
                    animationController.ResetFirstAttack();
                    trails[0].SetActive(false);
                    trails[1].SetActive(false);
                    trails[2].SetActive(false);
                    multiplayerController.movement.maxSpeed = _currentMaxSpeed;
                    attaking = false;
                    currentAttack = 0;
                    _finalImpulse = Vector3.zero;
               }
          }
     }

     public void FinalAttack()
     {
          if (!multiplayerController.death.dead && multiplayerController.movement.canMove)
          {
               if (currentAttack >= 3)
               {
                    animationController.SetFinalAttack();
                    trails[0].SetActive(true);
                    trails[1].SetActive(true);
                    trails[2].SetActive(true);
               }
               else
               {
                    animationController.ResetFirstAttack();
                    animationController.ResetSecondAttack();
                    trails[0].SetActive(false);
                    trails[1].SetActive(false);
                    trails[2].SetActive(false);
                    multiplayerController.movement.maxSpeed = _currentMaxSpeed;
                    attaking = false;
                    currentAttack = 0;
                    _finalImpulse = Vector3.zero;
               }
          }
     }

     public void ResetAttack()
     {
          animationController.ResetAttacks();
          trails[0].SetActive(false);
          trails[1].SetActive(false);
          trails[2].SetActive(false);
          multiplayerController.movement.maxSpeed = _currentMaxSpeed;
          attaking = false;
          currentAttack = 0;
          _finalImpulse = Vector3.zero;
     }

     [PunRPC]
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
          if (!multiplayerController.death.dead)
          {
               if (multiplayerController.levelMechanics.slowing == false)
               {
                    multiplayerController.movement.maxSpeed = 0f;
                    attaking = true;
               }
          }
     }

     [PunRPC]
     public void Impulse()
     {
          if (attaking)
          {
               if (_finalImpulse == Vector3.zero)
               {
                    _finalImpulse = multiplayerController.transform.position + (multiplayerController.characterGraphic.forward * distanceImpulse);
               }
               else
               {
                    if (!Physics.Raycast(multiplayerController.characterGraphic.position, multiplayerController.characterGraphic.forward, 1.18f))
                    {
                         multiplayerController.movement.velocity.y = 0;
                         multiplayerController.SetControllerPosition(Vector3.MoveTowards(multiplayerController.transform.position, _finalImpulse, attackImpulse * Time.deltaTime));

                         if (_finalImpulse == multiplayerController.transform.position)
                         {                              
                              _finalImpulse = Vector3.zero;
                              multiplayerController.levelMechanics.sliding = false;
                              attaking = false;
                         }
                    }
                    else
                    {
                         _finalImpulse = Vector3.zero;
                         multiplayerController.levelMechanics.sliding = false;
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

     [PunRPC]
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
