using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
     public static PlayerController instance;

     [Header("Movement variables")]
     public Movement movement;
     private Vector3 _direction;
     private Vector3 _lastPosition;
     private float _targetAngle;
     private float _angle;
     private float _turnSmoothVelocity;
     private float _horizontal, _vertical;

     [System.Serializable]
     public class Movement
     {
          public CharacterState stateCharacter;
          public Rigidbody rbody;
          public Transform cam;
          public float maxSpeed;
          public float currentSpeed;
          public float acceleration;
          public float turnSmoothtime;
          public float fixedMaxSpeed;
          public bool slowed;
     }

     [Header("Jump variables")]
     public Jump jump;

     [System.Serializable]
     public class Jump
     {
          public LayerMask groundLayer;
          public Transform jumpShadow;
          public Material handMaterial;
          public GameObject jumpEffect;
          public Transform boneHand;
          public float doubleJumpCountdown;
          public float fallMultiplier;
          public float lowJumpMultiplier;
          public float groundDetectorRange;
          public float jumpForce;
          public float maxDistanceShadow;
          public float speedShadow;
          public float rangeSlopeDetector;
          public float doubleJumpTime;
          public float handShaderStrength;
          public int currentJump;
          public int maxJump;
     }

     [Header("Push variables")]
     public Push push;

     [System.Serializable]
     public class Push
     {
          public Slow slowReference;
          public Transform targetPush;
          public Transform currentTargetPush;
          public Transform middleOfThePlayer;
          public float rangePush;
          public float rangeDropObject;
          public float currentMaxSpeed;
          public bool pushingObj;
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
     private float _countdownDeath;
     private float _currentMaxSpeed;

     [System.Serializable]
     public class Death
     {
          public Transform currentPoint;
          public bool dead;
     }

#if UNITY_EDITOR
     [Header("See Range variables")]
     public bool seeRangePush = false;
     public bool seeRangeCliff = false;
     public bool seeRangegroundDetector = false;
     public bool seeRangeMissedJump = false;
#endif

     void Start()
     {
          instance = this;

          Cursor.visible = false;
          Cursor.lockState = CursorLockMode.Locked;
          movement.fixedMaxSpeed = movement.maxSpeed;
     }

     void Update()
     {
          UpdateMovementPlayer();
          CharacterBetterJump();
          CharacterJump();
          PushingObject();
          CliffDetector();
          CharacterFace();
          SlopeDetector();
          SetHandShader();
          PlayerAnimations();
          CatchMissedJumps();
          JumpShadow();
          CheckDeath();
          CountdownAfterDeath();
          //SetJumpEffect();
     }

     #region Movement Player
     private void UpdateMovementPlayer()
     {
          _horizontal = Input.GetAxis("Horizontal");
          _vertical = Input.GetAxis("Vertical");

          if (movement.stateCharacter == CharacterState.BALANCE && (_horizontal != 0 || _vertical != 0))
          {
               _cliffDectorLockPlayer = false;
               _horizontal = 0;
               _vertical = 0;
               return;
          }
          else if (movement.stateCharacter == CharacterState.BALANCE)
          {
               movement.stateCharacter = CharacterState.RUNNNING;
          }
          CharacterMovement(_horizontal, _vertical);
     }

     public void CharacterMovement(float horizontal, float vertical)
     {
          _horizontal = horizontal;
          _vertical = vertical;

          _direction = new Vector3(_horizontal, 0f, _vertical).normalized;

          if (_direction.magnitude >= 0.1f && movement.stateCharacter != CharacterState.DEAD)
          {
               movement.currentSpeed += movement.acceleration * Time.deltaTime;
               movement.currentSpeed = Mathf.Clamp(movement.currentSpeed, 0, movement.maxSpeed);

               Vector3 _moveDirection = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;

               transform.Translate(_moveDirection.normalized * movement.currentSpeed * Time.deltaTime);
          }
          else
          {
               movement.currentSpeed = 0f;
          }

          if ((_horizontal != 0 || _vertical != 0) && movement.stateCharacter != CharacterState.RUNNNING && IsGrounded() && movement.rbody.velocity.y <= 0 && push.pushingObj == false && movement.stateCharacter != CharacterState.DEAD)
          {
               movement.stateCharacter = CharacterState.RUNNNING;
          }
          else if ((_horizontal != 0 || _vertical != 0) && !IsGrounded() && movement.stateCharacter != CharacterState.SINGLE_JUMP_RUNNING && movement.stateCharacter != CharacterState.DEAD && movement.stateCharacter != CharacterState.DOUBLE_JUMP)
          {
               movement.stateCharacter = CharacterState.SINGLE_JUMP_RUNNING;
          }

          if (_horizontal == 0 && _vertical == 0)
          {
               if (movement.rbody.velocity.y > 0 && !IsGrounded() && movement.stateCharacter != CharacterState.DEAD)
               {
                    if (movement.stateCharacter != CharacterState.SINGLE_JUMP && movement.stateCharacter != CharacterState.DOUBLE_JUMP)
                    {
                         movement.stateCharacter = CharacterState.SINGLE_JUMP;
                    }
               }
               else if (movement.stateCharacter != CharacterState.IDLE && IsGrounded() && push.pushingObj == false && movement.stateCharacter != CharacterState.DEAD)
               {
                    movement.stateCharacter = CharacterState.IDLE;
               }
          }
     }

     public void CharacterFace()
     {
          if (_direction.magnitude >= 0.1f && movement.stateCharacter != CharacterState.DEAD)
          {
               _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + movement.cam.eulerAngles.y;
               _angle = Mathf.SmoothDampAngle(characterGraphic.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity, movement.turnSmoothtime);

               characterGraphic.rotation = Quaternion.Euler(0f, _angle, 0f);
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
     public void SlopeDetector()
     {
          RaycastHit _hitInfo;

          if (Physics.Raycast(push.middleOfThePlayer.position, push.middleOfThePlayer.up * -1, out _hitInfo, jump.rangeSlopeDetector))
          {
               if (_hitInfo.transform.tag == "Ground")
               {
                    if (_hitInfo.normal != new Vector3(0, 1, 0) && movement.rbody.velocity.y <= 0)
                    {
                         transform.position = new Vector3(transform.position.x, _hitInfo.point.y + transform.localScale.y, transform.position.z);
                    }
               }
          }
     }

     public void CharacterJump()
     {
          DoubleJumpCountdown();
          if (Input.GetButtonDown("Jump") && CanJump() && movement.stateCharacter != CharacterState.PUSHING && push.pushingObj == false && (jump.doubleJumpCountdown >= 1 || jump.currentJump == 0) && movement.stateCharacter != CharacterState.DEAD)
          {
               movement.rbody.velocity = Vector3.up * jump.jumpForce;
               jump.doubleJumpCountdown = 0;
               jump.currentJump++;

               if (jump.currentJump < 2 && movement.stateCharacter != CharacterState.SINGLE_JUMP)
               {
                    movement.stateCharacter = CharacterState.SINGLE_JUMP;
               }
               else if (jump.currentJump >= 2 && movement.stateCharacter != CharacterState.DOUBLE_JUMP)
               {
                    movement.stateCharacter = CharacterState.DOUBLE_JUMP;
               }
          }
          if (IsGrounded() && movement.rbody.velocity.y < 0)
          {
               jump.currentJump = 0;
               jump.doubleJumpCountdown = 0;
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

     public void CharacterBetterJump()
     {
          if (movement.rbody.velocity.y <= 0)
          {
               movement.rbody.velocity += Vector3.up * Physics.gravity.y * (jump.fallMultiplier - 1) * Time.deltaTime;
          }
          else if (movement.rbody.velocity.y > 0 && !Input.GetButton("Jump"))
          {
               movement.rbody.velocity += Vector3.up * Physics.gravity.y * (jump.lowJumpMultiplier - 1) * Time.deltaTime;
          }
     }

     public bool IsGrounded()
     {
          Ray _ray = new Ray(transform.position, Vector3.up * -1 * jump.groundDetectorRange);
          return Physics.Raycast(_ray, jump.groundDetectorRange, jump.groundLayer);
     }

     public bool CanJump()
     {
          return jump.currentJump < jump.maxJump;
     }

     public void JumpShadow()
     {
          if (!IsGrounded())
          {
               RaycastHit _hitInfo;
               if (Physics.Raycast(transform.position, Vector3.up * -1, out _hitInfo, jump.maxDistanceShadow))
               {
                    if (_hitInfo.transform.tag != "Player")
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

     public void SetJumpEffect()
     {
          if (jump.currentJump == 2)
          {
               jump.jumpEffect.SetActive(true);
          }
          else
          {
               jump.jumpEffect.SetActive(false);
          }
     }
     #endregion

     #region Pushing Object
     private void PushingObject()
     {
          if (Input.GetButton("Fire2"))
          {
               PushObject();
          }
          else if (Input.GetButtonUp("Fire2"))
          {
               DropObject();
          }
     }

     public void PushObject()
     {
          RaycastHit _hit;

          if (Physics.Raycast(push.middleOfThePlayer.position, push.middleOfThePlayer.forward, out _hit, push.rangePush))
          {
               if (_hit.transform.tag == "Interactable")
               {
                    push.pushingObj = true;
                    movement.stateCharacter = CharacterState.PUSHING;
                    _hit.transform.position = push.targetPush.position;
                    push.currentTargetPush = _hit.transform;
                    _hit.transform.parent = push.targetPush.transform;

                    if (push.slowReference == null)
                    {
                         push.currentMaxSpeed = movement.fixedMaxSpeed;
                         movement.maxSpeed = 2f;
                    }
                    else
                    {
                         if (movement.slowed == false)
                         {
                              push.slowReference = null;
                              push.currentMaxSpeed = movement.fixedMaxSpeed;
                              movement.maxSpeed = 2f;
                         }
                    }

               }
               if ((_horizontal == 0 && _vertical == 0) && movement.stateCharacter != CharacterState.PUSHING_IDLE && movement.stateCharacter != CharacterState.DEAD)
               {
                    movement.stateCharacter = CharacterState.PUSHING_IDLE;
               }
               else if ((_horizontal != 0 || _vertical != 0) && movement.stateCharacter != CharacterState.PUSHING && movement.stateCharacter != CharacterState.DEAD)
               {
                    movement.stateCharacter = CharacterState.PUSHING;
               }
          }
     }

     public void DropObject()
     {
          RaycastHit _hit;

          if (Physics.Raycast(push.currentTargetPush.position, Vector3.up * -1, out _hit, push.rangeDropObject))
          {
               if (_hit.transform.position != null)
               {
                    push.pushingObj = false;
                    push.currentTargetPush.parent = null;
                    Vector3 pivotCorrection = new Vector3(0f, 0.8f, 0f); // Just to correct the pivot of unity objects
                    push.currentTargetPush.position = _hit.point + pivotCorrection;
                    movement.maxSpeed = push.currentMaxSpeed;
                    push.slowReference = null;
               }
          }

     }
     #endregion

     #region Movement Assistance
     public void CliffDetector()
     {
          Vector3 _origin = transform.position + characterGraphic.forward * cliff.cliffDetectorFwrdDist;

          Ray _ray = new Ray(_origin, Vector3.up * -1);

          if (!Physics.Raycast(_ray, cliff.cliffDetectorHeightDist) && IsGrounded() && _cliffDectorLockPlayer == true && GetLocomotionSpeed() < cliff.cliffDetectorMaxSpeed)
          {
               if (movement.stateCharacter != CharacterState.BALANCE)
               {
                    movement.stateCharacter = CharacterState.BALANCE;
               }
          }
          else if (Physics.Raycast(_ray, cliff.cliffDetectorHeightDist))
          {
               _cliffDectorLockPlayer = true;
               if (movement.stateCharacter == CharacterState.BALANCE)
               {
                    movement.stateCharacter = CharacterState.RUNNNING;
               }
          }
     }

     public void CatchMissedJumps()
     {
          RaycastHit _hitInfo;

          if (Physics.Raycast(missedJump.targetMissedJump.position, Vector3.down, out _hitInfo, missedJump.rangeRayMissedJump))
          {
               if (_hitInfo.transform.tag == "Ground" || _hitInfo.transform.tag == "Interactable")
               {
                    if (missedJump.canMiss)
                    {
                         transform.position = Vector3.MoveTowards(transform.position, _hitInfo.point + new Vector3(0f, missedJump.offsetPlayerPos, 0f), missedJump.smootnessMissedJump * Time.deltaTime);
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
                    movement.stateCharacter = CharacterState.DEAD;
                    movement.rbody.useGravity = false;
                    characterCollider.enabled = false;
                    _currentMaxSpeed = movement.fixedMaxSpeed;
                    movement.maxSpeed = 0;
                    death.dead = true;
               }
          }
     }

     void CountdownAfterDeath()
     {
          if (death.dead)
          {
               if (_countdownDeath < 1)
               {
                    _countdownDeath += Time.deltaTime / 2f;
               }
               else
               {
                    hit.hitCount = 0;
                    movement.stateCharacter = CharacterState.IDLE;
                    transform.position = death.currentPoint.position;
                    movement.rbody.useGravity = true;
                    characterCollider.enabled = true;
                    movement.maxSpeed = _currentMaxSpeed;
                    _countdownDeath = 0;
                    death.dead = false;
               }
          }
     }
     #endregion

     #region Animations
     public void PlayerAnimations()
     {
          animator.SetFloat("Horizontal", _horizontal);
          animator.SetFloat("Vertical", _vertical);
          animator.SetBool("IsGrounded", IsGrounded());

          switch (movement.stateCharacter)
          {
               case CharacterState.IDLE:
                    animator.SetBool("Idle", true);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
                    animator.SetBool("Dying", false);
                    animator.SetBool("Balance", false);
                    break;
               case CharacterState.RUNNNING:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", true);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
                    animator.SetBool("Dying", false);
                    animator.SetBool("Balance", false);
                    break;
               case CharacterState.SINGLE_JUMP:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", true);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
                    animator.SetBool("Dying", false);
                    animator.SetBool("Balance", false);
                    break;
               case CharacterState.SINGLE_JUMP_RUNNING:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", true);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
                    animator.SetBool("Dying", false);
                    animator.SetBool("Balance", false);
                    break;
               case CharacterState.DOUBLE_JUMP:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", true);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
                    animator.SetBool("Dying", false);
                    animator.SetBool("Balance", false);
                    break;
               case CharacterState.PUSHING_IDLE:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", true);
                    animator.SetBool("Dying", false);
                    animator.SetBool("Balance", false);
                    break;
               case CharacterState.PUSHING:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", true);
                    animator.SetBool("Pushing Idle", false);
                    animator.SetBool("Dying", false);
                    animator.SetBool("Balance", false);
                    break;
               case CharacterState.DEAD:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
                    animator.SetBool("Dying", true);
                    animator.SetBool("Balance", false);
                    break;
               case CharacterState.BALANCE:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
                    animator.SetBool("Dying", false);
                    animator.SetBool("Balance", true);
                    break;
          }
     }
     #endregion

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          if (seeRangePush)
          {
               Gizmos.color = Color.magenta;
               Gizmos.DrawLine(push.middleOfThePlayer.position, push.middleOfThePlayer.position + push.middleOfThePlayer.forward * push.rangePush);
          }

          if (seeRangeCliff)
          {
               Gizmos.color = Color.red;
               Gizmos.DrawSphere(transform.position + characterGraphic.forward * cliff.cliffDetectorFwrdDist, 0.3f);
               Gizmos.color = Color.green;
               Gizmos.DrawRay(transform.position + characterGraphic.forward * cliff.cliffDetectorFwrdDist, Vector3.up * -1 * cliff.cliffDetectorHeightDist);
          }

          if (seeRangegroundDetector)
          {
               Debug.DrawRay(transform.position, Vector3.up * -1 * jump.groundDetectorRange, Color.blue);
          }

          if (seeRangeMissedJump)
          {
               Gizmos.color = Color.red;
               Gizmos.DrawSphere(missedJump.targetMissedJump.position, 0.1f);
               Debug.DrawRay(missedJump.targetMissedJump.position, Vector3.down * missedJump.rangeRayMissedJump, Color.green);
          }
     }
#endif
}
