using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(PhotonView))]
public class PlayerControllerMultiplayer : Character
{
     [Header("Movement variables")]
     public Movement movement;
     private Vector3 _direction;
     private Vector3 _lastPosition;
     private float _targetAngle;
     private float _angle;
     private float _turnSmoothVelocity;

     [System.Serializable]
     public class Movement
     {
          public Vector3 velocity;
          public LayerMask groundMask;
          public CharacterController controller;
          public Transform cam;
          public Transform targetCam;
          public Transform groundCheck;
          public float groundDistance;
          public float gravity = -9.81f;
          public float fixedGravity;
          public float maxSpeed;
          public float currentSpeed;
          public float fixedMaxSpeed;
          public float acceleration;
          public float turnSmoothtime;
          public bool isGrounded;
          public bool canMove = true;
          public float horizontal, vertical;
     }

     [Header("Jump variables")]
     public LevelMechanics levelMechanics;

     [System.Serializable]
     public class LevelMechanics
     {
          public bool slowing;
          public bool sliding;
          public bool entryTeleport;
          public bool exitTeleport;
          public bool interacting;
     }

     [Header("Jump variables")]
     public Jump jump;

     [System.Serializable]
     public class Jump
     {
          public Transform groundDetector;
          public Transform jumpShadow;
          public Material handMaterial;
          public ParticleSystem fallingDust;
          public float fallMultiplier;
          public float lowJumpMultiplier;
          public int currentJump;
          public int maxJump;
          public float jumpForce;
          public float doubleJumpTime;
          public float doubleJumpCountdown;
          public float speedShadow;
          public float maxDistanceShadow;
          public float rangeSlopeDetector;
          public float rangeFallingGround;
          public float handShaderStrength;
          public float groundDetectorRange;
     }

     [Header("Cliff variables")]
     public Cliff cliff;
     private bool _cliffDectorLockPlayer;

     [System.Serializable]
     public class Cliff
     {
          public float cliffDetectorFwrdDist;
          public float cliffDetectorHeightDist;
          public float cliffDetectorMaxSpeed;
     }

     [Header("Missed Jumps variables")]
     public MissedJump missedJump;

     [System.Serializable]
     public class MissedJump
     {
          public Transform targetMissedJump;
          public float rangeRayMissedJump;
          public float offsetPlayerPos;
          public float timeDetectMissedJump;
          public float countdownMissedJump;
          public float smootnessMissedJump;
          public bool canMiss;
     }

     [Header("Death variables")]
     public Death death;
     private float _headCutoffHeight;
     private float _bodyCutoffHeight;
     private float _pickaxeCutoffHeight;

     [System.Serializable]
     public class Death
     {    
          public Material head;
          public Material body;
          public Material pickaxe;
          public Transform currentPoint;
          public float timeDead;
          public float timeToMoveAfterDead;
          public float speedHeadCutoffHeightDisappear;
          public float speedHeadCutoffHeightAppear;
          public float speedBodyCutoffHeightDisappear;
          public float speedBodyCutoffHeightAppear;
          public float speedPickaxeCutoffHeightDisappear;
          public float speedPickaxeCutoffHeightAppear;
          public float offsetDead;
          public bool dead;
          [HideInInspector]
          public bool canSetAppearShader;
          [HideInInspector]
          public bool canSetDisappearShader;
     }

     [Header("Photon variables")]
     public Photon photon;

    [System.Serializable]
     public class Photon
     { 
          public PhotonView m_PhotonView;
          public GameObject cameraPrefab;
          public Transform camSpawnPoint;
          public Transform m_RacingPosition;
          public Animator animator;
          public TMP_Text playerNameUI;
          public string playerName;
          public GameManager_Demo_ENDM m_Manager;
          private bool isRacing = true;
     }
     
#if UNITY_EDITOR
     [Header("See Range variables")]
     public bool seeRangePush = false;
     public bool seeRangeCliff = false;
     public bool seeRangeGroundDetector = false;
     public bool seeRangeMissedJump = false;
     public bool seeRangePositionDead = false;
     public bool seeRangeFalling = false;
#endif

     void Start()
     {
          if(photon.m_PhotonView.IsMine) {
          Cursor.visible = false;
          Cursor.lockState = CursorLockMode.Locked;
          movement.fixedMaxSpeed = movement.maxSpeed;
          movement.fixedGravity = movement.gravity;
          ResetValueDissolveShader();
               
               GameObject _cam = Instantiate(photon.cameraPrefab, photon.camSpawnPoint.position, Quaternion.identity);
               _cam.GetComponent<Camera3rdPersonMultiplayer>().targetCamera = this.movement.targetCam;
               movement.cam = _cam.transform;
               RandName();
               photon.playerNameUI.text = photon.playerName;
               return;
          }
          photon.playerNameUI.text = photon.playerName;
     }

     void Update()
     {
          if(photon.m_PhotonView.IsMine)
          {
               Gravity();
               CharacterJump();
               CheckIsGrounded();          
               CliffDetector();
               CharacterFace();
               JumpShadow();
               SlopeDetector();
               SetHandShader();
               ResetCurrentJump();
               CatchMissedJumps();
               CheckDeath();
               SetDissolveShaderAppear();          
               SetDissolveShaderDisappear();
               PlayerConfigsAfterDeath();
          }
     }

     void FixedUpdate()
     {
          if(photon.m_PhotonView.IsMine)
               UpdateMovementPlayer();
     }

     public void SetControllerPosition(Vector3 toPosition)
     {
          movement.controller.enabled = false;
          transform.position = toPosition;
          movement.controller.enabled = true;
     }

     #region Multiplayer
     private void RandName()
     {
          int randLastName = Random.Range(0, 6);

          switch(randLastName) {
               case 0:
                    photon.playerName = "Beto the Legend";
                    break;
               case 1:
                    photon.playerName = "Beto Crusher";
                    break;
               case 2:
                    photon.playerName = "Beto Smasher";
                    break;
               case 3:
                    photon.playerName = "Beto the Lord";
                    break;
               case 4:
                    photon.playerName = "Beto the Fearless";
                    break;
               case 5:
                    photon.playerName = "Beto the Hero";
                    break;
          }
          
     }
     private void SetStart() {
          if(m_Manager.isGameReady && photon.isRacing) {
               SetControllerPosition(photon.m_RacingPosition.position);
          }
     }
     #endregion

     #region Movement Player
     private void UpdateMovementPlayer()
     {
          if (movement.canMove && !death.dead && !levelMechanics.sliding && !levelMechanics.interacting)
          {
               movement.vertical = Input.GetAxis("Vertical");               
               movement.horizontal = Input.GetAxis("Horizontal");

               CharacterMovement();
          }
     }

     public void CharacterMovement()
     {
          _direction = new Vector3(movement.horizontal, 0f, movement.vertical).normalized;

          if (_direction.magnitude >= 0.1f)
          {
               movement.currentSpeed += movement.acceleration * Time.deltaTime;
               movement.currentSpeed = Mathf.Clamp(movement.currentSpeed, 0, movement.maxSpeed);

               Vector3 _moveDirection = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;

               movement.controller.Move(_moveDirection.normalized * movement.currentSpeed * Time.fixedDeltaTime);

          }
          else
          {
               movement.currentSpeed = 0f;
          }
     }

     public void CharacterFace()
     {
          if (movement.canMove && !death.dead && !levelMechanics.entryTeleport && !levelMechanics.exitTeleport && !levelMechanics.sliding)
          {
               if (_direction.magnitude >= 0.1f)
               {
                    _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + movement.cam.eulerAngles.y;
                    _angle = Mathf.SmoothDampAngle(characterGraphic.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity, movement.turnSmoothtime);

                    characterGraphic.rotation = Quaternion.Euler(0f, _angle, 0f);
               }
          }
     }

     public float GetLocomotionSpeed()
     {
          if (_lastPosition != transform.position)
          {
               Vector2 _lastPositionVec = new Vector2(_lastPosition.x, _lastPosition.z);
               Vector2 _lastPositionPlayer = new Vector2(transform.position.x, transform.position.z);

               _lastPosition = transform.position;

               return Vector2.Distance(_lastPosition, _lastPositionPlayer);
          }
          return 0;
     }
     #endregion

     #region Jump
     public void CheckIsGrounded()
     {
          movement.isGrounded = Physics.CheckSphere(movement.groundCheck.position, movement.groundDistance, movement.groundMask);

          if (movement.isGrounded && movement.velocity.y < 0)
          {
               movement.velocity.y = -2f;
          }
     }

     public void Gravity()
     {
          movement.velocity.y += movement.gravity * Time.deltaTime;
          movement.controller.Move(movement.velocity * Time.deltaTime);
     }

     public void CharacterJump()
     {
          DoubleJumpCountdown();

          if (Input.GetButtonDown("Jump") && 
              CanJump() &&
              movement.canMove &&
              !levelMechanics.sliding &&
              //!PlayerAttackController.instance.attaking &&
              //PlayerAttackController.instance.currentAttack == 0 &&
              //!GameManager.instance.settingsData.settingsOpen &&
              //!PlayerAnimationController.instance.afterFalling &&
              (jump.doubleJumpCountdown >= 1 || jump.currentJump == 0))
          {
               movement.velocity.y = Mathf.Sqrt(2f * -2f * movement.gravity);
               //PlayerAnimationController.instance.fallingIdle = false;
               jump.doubleJumpCountdown = 0;
               jump.currentJump++;
          }
     }

     public void ResetCurrentJump()
     {
          if (movement.isGrounded &&
              movement.velocity.y < 0 &&
              //!PlayerAnimationController.instance.fallingIdle &&
              jump.currentJump > 0)
          {
               //PlayerAnimationController.instance.fallingIdle = false;
               jump.currentJump = 0;
               jump.doubleJumpCountdown = 0;
               movement.velocity.y = -2f;
          }
     }

     public void DoubleJumpCountdown()
     {
          if (jump.currentJump > 0)
          {
               if (jump.doubleJumpCountdown < 1)
               {
                    jump.doubleJumpCountdown += Time.deltaTime / jump.doubleJumpTime;
               }
          }
     }

     public bool CanJump()
     {
          return jump.currentJump < jump.maxJump;
     }

     public void JumpShadow()
     {
          if (!movement.isGrounded && !death.dead)
          {
               RaycastHit _hitInfo;
               if (Physics.Raycast(transform.position, Vector3.up * -1, out _hitInfo, jump.maxDistanceShadow))
               {
                    if (_hitInfo.transform.tag != "Player" &&
                        _hitInfo.transform.tag != "Checkpoint")
                    {
                         if (!jump.jumpShadow.gameObject.activeSelf)
                         {
                              jump.jumpShadow.transform.position = _hitInfo.point + new Vector3(0f, 0.05f, 0f);
                         }
                         jump.jumpShadow.gameObject.SetActive(true);
                         jump.jumpShadow.transform.position = Vector3.MoveTowards(jump.jumpShadow.transform.position, _hitInfo.point + new Vector3(0f, 0.05f, 0f), jump.speedShadow * Time.deltaTime);
                         jump.jumpShadow.transform.rotation = Quaternion.FromToRotation(Vector3.up, _hitInfo.normal);
                    }
               }
               else
               {
                    jump.jumpShadow.transform.position = characterGraphic.transform.position;
                    jump.jumpShadow.gameObject.SetActive(false);
               }
          }
          else
          {
               jump.jumpShadow.transform.position = characterGraphic.transform.position;
               jump.jumpShadow.gameObject.SetActive(false);
          }
     }

     public void SlopeDetector()
     {
          RaycastHit _hitInfo;

          if (Physics.Raycast(transform.position + new Vector3(0f, 1f, 0f), transform.up * -1, out _hitInfo, jump.rangeSlopeDetector))
          {
               if (_hitInfo.transform.tag == "Ground")
               {
                    if (_hitInfo.normal != new Vector3(0, 1, 0) && movement.controller.velocity.y <= 0)
                    {
                         SetControllerPosition(new Vector3(transform.position.x, _hitInfo.point.y + transform.localScale.y, transform.position.z));
                    }
               }
          }
     }

     public void SetHandShader()
     {
          if (jump.currentJump == 2)
          {
               if (jump.handShaderStrength > 0)
               {
                    jump.handShaderStrength -= Time.deltaTime * 2f;
                    jump.handShaderStrength = Mathf.Clamp(jump.handShaderStrength, 0f, 1f);
               }
               jump.handMaterial.SetFloat("Hand_Emission", jump.handShaderStrength);
          }
          else
          {
               if (jump.handShaderStrength < 1)
               {
                    jump.handShaderStrength += Time.deltaTime / 3f;
                    jump.handShaderStrength = Mathf.Clamp(jump.handShaderStrength, 0f, 1f);
               }
               jump.handMaterial.SetFloat("Hand_Emission", jump.handShaderStrength);
          }
     }
     #endregion

     #region Movement Assistance
     public void CliffDetector()
     {
          Vector3 _origin = transform.position + characterGraphic.forward * cliff.cliffDetectorFwrdDist;

          Ray _ray = new Ray(_origin, Vector3.up * -1);

          if (!Physics.Raycast(_ray, cliff.cliffDetectorHeightDist) && 
               movement.isGrounded && 
               _cliffDectorLockPlayer == true && 
               GetLocomotionSpeed() < cliff.cliffDetectorMaxSpeed && 
               movement.currentSpeed < cliff.cliffDetectorMaxSpeed)
               //!PlayerAttackController.instance.attaking)
          {
               //PlayerAnimationController.instance.SetBalance();
          }
          else if (Physics.Raycast(_ray, cliff.cliffDetectorHeightDist))
          {
               _cliffDectorLockPlayer = true;
               //PlayerAnimationController.instance.balance = false;
          }
     }

     public void CatchMissedJumps()
     {
          RaycastHit _hitInfo;

          if (Physics.Raycast(missedJump.targetMissedJump.position, Vector3.down, out _hitInfo, missedJump.rangeRayMissedJump))
          {
               if (_hitInfo.transform.tag == "Ground" ||
               _hitInfo.transform.tag == "Interactable" ||
               _hitInfo.transform.tag == "Light" ||
               _hitInfo.transform.tag == "Heavy" ||
               _hitInfo.transform.tag == "Platform" ||
               _hitInfo.transform.tag == "Breakable")
               {
                    if (missedJump.canMiss)
                    {
                         SetControllerPosition(Vector3.MoveTowards(transform.position, _hitInfo.point + new Vector3(0f, missedJump.offsetPlayerPos, 0f), missedJump.smootnessMissedJump * Time.deltaTime));
                         missedJump.canMiss = false;
                    }
               }
          }

          if (!missedJump.canMiss)
          {
               if (missedJump.countdownMissedJump < 1)
               {
                    missedJump.countdownMissedJump += Time.deltaTime / missedJump.timeDetectMissedJump;
               }
               else
               {
                    missedJump.countdownMissedJump = 0;
                    missedJump.canMiss = true;
               }
          }
     }
     #endregion

     #region Death
     public void CheckDeath()
     {
          if (hit.hitCount >= hit.maxHitCount)
          {
               if (!death.dead)
               {
                    movement.maxSpeed = 0;
                    death.dead = true;      
                    movement.gravity = 0;
                    movement.velocity = Vector3.zero;
                    movement.maxSpeed = 0;               
                    StartCoroutine("AfterDeath");
               }
          }
     }

     IEnumerator AfterDeath()
     {
          death.canSetDisappearShader = true;

          yield return new WaitForSeconds(death.timeDead);

          death.canSetAppearShader = true;
          death.canSetDisappearShader = false;
          levelMechanics.exitTeleport = true;
          SetControllerPosition(death.currentPoint.position);

          yield return new WaitForSeconds(death.timeToMoveAfterDead);
          
          //PlayerAttackController.instance.ResetAttack();
          hit.hitCount = 0;
          movement.maxSpeed = movement.fixedMaxSpeed;
          levelMechanics.exitTeleport = false;
          death.canSetAppearShader = false;
          death.dead = false;
          ResetPlayerConfigsAfterDeath();
     }

     public void PlayerConfigsAfterDeath()
     {    
          if(death.canSetAppearShader)
          {
               movement.gravity = 0;
               movement.velocity = Vector3.zero;
               movement.maxSpeed = 0;                   
          }     
     }

     public void ResetPlayerConfigsAfterDeath()
     {
          movement.gravity = movement.fixedGravity;
          movement.maxSpeed = movement.fixedMaxSpeed;   
     }

     public void ResetValueDissolveShader()
     {
          _headCutoffHeight = -1f;
          _bodyCutoffHeight = -1f;
          _pickaxeCutoffHeight = -1;
          death.head.SetFloat("_Cutoff_Height", -1f);
          death.body.SetFloat("_Cutoff_Height", -1f);
          death.pickaxe.SetFloat("_Cutoff_Height", -1f);
     }

     public void SetDissolveShaderAppear()
     {
          if(death.canSetAppearShader)
          {
               _headCutoffHeight -= Time.deltaTime * death.speedHeadCutoffHeightAppear ;
               _headCutoffHeight = Mathf.Clamp(_headCutoffHeight, -1f, 5f);
               death.head.SetFloat("_Cutoff_Height", _headCutoffHeight);

               _bodyCutoffHeight -= Time.deltaTime * death.speedBodyCutoffHeightAppear;
               _bodyCutoffHeight = Mathf.Clamp(_bodyCutoffHeight, -1f, 5f);
               death.body.SetFloat("_Cutoff_Height", _bodyCutoffHeight);

               _pickaxeCutoffHeight -= Time.deltaTime * death.speedPickaxeCutoffHeightAppear;
               _pickaxeCutoffHeight = Mathf.Clamp(_pickaxeCutoffHeight, -1f, 5f);
               death.pickaxe.SetFloat("_Cutoff_Height", _pickaxeCutoffHeight);
          }
     }

     public void SetDissolveShaderDisappear()
     {  
          if(death.canSetDisappearShader)
          {
               _headCutoffHeight += Time.deltaTime * death.speedHeadCutoffHeightDisappear;
               _headCutoffHeight = Mathf.Clamp(_headCutoffHeight, -1f, 5f);
               death.head.SetFloat("_Cutoff_Height", _headCutoffHeight);

               _bodyCutoffHeight += Time.deltaTime * death.speedBodyCutoffHeightDisappear;
               _bodyCutoffHeight = Mathf.Clamp(_bodyCutoffHeight, -1f, 5f);
               death.body.SetFloat("_Cutoff_Height", _bodyCutoffHeight);

               _pickaxeCutoffHeight += Time.deltaTime * death.speedPickaxeCutoffHeightDisappear;
               _pickaxeCutoffHeight = Mathf.Clamp(_pickaxeCutoffHeight, -1f, 5f);
               death.pickaxe.SetFloat("_Cutoff_Height", _pickaxeCutoffHeight);
          }
     }
     #endregion

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          if (seeRangeCliff)
          {
               Gizmos.color = Color.red;
               Gizmos.DrawSphere(transform.position + characterGraphic.forward * cliff.cliffDetectorFwrdDist, 0.3f);
               Gizmos.color = Color.green;
               Gizmos.DrawRay(transform.position + characterGraphic.forward * cliff.cliffDetectorFwrdDist, Vector3.up * -1 * cliff.cliffDetectorHeightDist);
          }

          if (seeRangeGroundDetector)
          {
               Gizmos.color = Color.green;
               Gizmos.DrawSphere(movement.groundCheck.position, movement.groundDistance);               
          }

          if (seeRangeMissedJump)
          {
               Gizmos.color = Color.red;
               Gizmos.DrawSphere(missedJump.targetMissedJump.position, 0.1f);
               Debug.DrawRay(missedJump.targetMissedJump.position, Vector3.down * missedJump.rangeRayMissedJump, Color.green);
          }

          if (seeRangePositionDead)
          {
               Gizmos.color = Color.cyan;
               Gizmos.DrawRay(transform.position, Vector3.down * 10f);
          }

          if (seeRangeFalling)
          {
               Gizmos.color = Color.magenta;
               Gizmos.DrawRay(transform.position, Vector3.down * jump.rangeFallingGround);
          }
     }
#endif
}
