using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
     public CharacterState stateCharacter;
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
     public struct Movement
     {
          public Transform cam;
          public float maxSpeed;
          public float currentSpeed;
          public float acceleration;
          public float turnSmoothtime;
     }

     [Header("Jump variables")]
     public Jump jump;
     private float _doubleJumpCountdown;

     [System.Serializable]
     public struct Jump
     {
          public LayerMask groundLayer;
          public Transform jumpShadow;
          public float fallMultiplier;
          public float lowJumpMultiplier;
          public float groundDetectorRange;
          public float jumpForce;
          public float maxDistanceShadow;
          public float rangeSlopeDetector;
          public float doubleJumpTime;
          public int currentJump;
          public int maxJump;
     }


     [Header("Push variables")]
     public Push push;

     [System.Serializable]
     public struct Push
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

     [Header("Stun variables")]
     public Stun stun;
     private float _distanceBetwen;

     [System.Serializable]
     public struct Stun
     {
          public float rangeStun;
          public float timeStun;
          public float cooldownStun;
          public bool canStun;
     }

     [Header("Cliff variables")]
     public Cliff cliff;
     private bool _cliffDectorLockPlayer;

     [System.Serializable]
     public struct Cliff
     {
          public float cliffDetectorFwrdDist;
          public float cliffDetectorHeightDist;
          public float cliffDetectorMaxSpeed;
     }

     [Header("Missed Jumps variables")]
     public MissedJump missedJump;

     [System.Serializable]
     public struct MissedJump
     {
          public Transform targetMissedJump;
          public float rangeRayMissedJump;
          public float offsetPlayerPos;
          public float timeDetectMissedJump;
          public float countdownMissedJump;
          public float smootnessMissedJump;
          public bool canMiss;
     }

#if UNITY_EDITOR
     [Header("See Range variables")]
     public bool seeRangePush = false;
     public bool seeRangeStun = false;
     public bool seeRangeCliff = false;
     public bool seeRangegroundDetector = false;
     public bool seeRangeMissedJump = false;
#endif

     void Start()
     {
          instance = this;

          Cursor.visible = false;
          Cursor.lockState = CursorLockMode.Locked;
     }

     void Update()
     {
          UpdateMovementPlayer();
          CharacterBetterJump();
          CharacterJump();
          PushingObject();
          CliffDetector();
          CharacterFace();
          StunningEnemy();
          SlopeDetector();
          PlayerAnimations();
          CatchMissedJumps();
          JumpShadow();
     }

     #region Movement Player
     private void UpdateMovementPlayer()
     {
          _horizontal = Input.GetAxis("Horizontal");
          _vertical = Input.GetAxis("Vertical");

          if (stateCharacter == CharacterState.BALANCE && (_horizontal != 0 || _vertical != 0))
          {
               _cliffDectorLockPlayer = false;
               _horizontal = 0;
               _vertical = 0;
               return;
          }
          else if (stateCharacter == CharacterState.BALANCE)
          {
               stateCharacter = CharacterState.RUNNNING;
          }
          CharacterMovement(_horizontal, _vertical);
     }

     public void CharacterMovement(float horizontal, float vertical)
     {
          _horizontal = horizontal;
          _vertical = vertical;

          _direction = new Vector3(_horizontal, 0f, _vertical).normalized;

          if (_direction.magnitude >= 0.1f)
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

          if ((_horizontal != 0 || _vertical != 0) && stateCharacter != CharacterState.RUNNNING && IsGrounded() && rbody.velocity.y <= 0 && push.pushingObj == false)
          {
               stateCharacter = CharacterState.RUNNNING;
          }
          else if ((_horizontal != 0 || _vertical != 0) && !IsGrounded() && stateCharacter != CharacterState.SINGLE_JUMP_RUNNING)
          {
               stateCharacter = CharacterState.SINGLE_JUMP_RUNNING;
          }

          if (_horizontal == 0 && _vertical == 0)
          {
               if (rbody.velocity.y > 0 && !IsGrounded())
               {
                    if (stateCharacter != CharacterState.SINGLE_JUMP)
                    {
                         stateCharacter = CharacterState.SINGLE_JUMP;
                    }
               }
               else if (stateCharacter != CharacterState.IDLE && IsGrounded() && push.pushingObj == false)
               {
                    stateCharacter = CharacterState.IDLE;
               }
          }
     }

     public void CharacterFace()
     {
          if (_direction.magnitude >= 0.1f)
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
                    if (_hitInfo.normal != new Vector3(0, 1, 0) && rbody.velocity.y <= 0)
                    {
                         transform.position = new Vector3(transform.position.x, _hitInfo.point.y + transform.localScale.y, transform.position.z);
                    }
               }
          }
     }

     public void CharacterJump()
     {
          DoubleJumpCountdown();
          if (Input.GetButtonDown("Jump") && CanJump() && stateCharacter != CharacterState.PUSHING && push.pushingObj == false && (_doubleJumpCountdown >= 1 || jump.currentJump == 0))
          {
               rbody.velocity = Vector3.up * jump.jumpForce;
               _doubleJumpCountdown = 0;
               jump.currentJump++;

               if (jump.currentJump < 2)
               {
                    stateCharacter = CharacterState.SINGLE_JUMP;
               }
               else if (jump.currentJump >= 2)
               {
                    stateCharacter = CharacterState.DOUBLE_JUMP;
               }
          }
          if (IsGrounded() && rbody.velocity.y < 0)
          {
               jump.currentJump = 0;
               _doubleJumpCountdown = 0;
               if ((_vertical != 0 || _horizontal != 0) && stateCharacter != CharacterState.RUNNNING && push.pushingObj == false)
               {
                    stateCharacter = CharacterState.RUNNNING;
               }
          }
     }

     public void DoubleJumpCountdown()
     {
          if (jump.currentJump > 0)
          {
               if (_doubleJumpCountdown < 1)
               {
                    _doubleJumpCountdown += Time.deltaTime / jump.doubleJumpTime;
               }
          }
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
                         jump.jumpShadow.transform.position = Vector3.MoveTowards(jump.jumpShadow.transform.position, _hitInfo.point + new Vector3(0f, 0.05f, 0f), 10f * Time.deltaTime);
                         jump.jumpShadow.transform.rotation = Quaternion.FromToRotation(Vector3.up, _hitInfo.normal);
                    }
               }
               else
               {
                    jump.jumpShadow.gameObject.SetActive(false);
               }
          }
          else
          {
               jump.jumpShadow.gameObject.SetActive(false);
          }
     }

     public void CharacterBetterJump()
     {
          if (rbody.velocity.y <= 0)
          {
               rbody.velocity += Vector3.up * Physics.gravity.y * (jump.fallMultiplier - 1) * Time.deltaTime;
          }
          else if (rbody.velocity.y > 0 && !Input.GetButton("Jump"))
          {
               rbody.velocity += Vector3.up * Physics.gravity.y * (jump.lowJumpMultiplier - 1) * Time.deltaTime;
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
     #endregion 

     #region Pushing Object
     private void PushingObject()
     {
          if (Input.GetMouseButton(0))
          {
               PushObject();
          }
          else if (Input.GetMouseButtonUp(0))
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
                    stateCharacter = CharacterState.PUSHING;
                    _hit.transform.position = push.targetPush.position;
                    push.currentTargetPush = _hit.transform;
                    _hit.transform.parent = push.targetPush.transform;

                    if (push.slowReference == null)
                    {
                         push.currentMaxSpeed = movement.maxSpeed;
                         movement.maxSpeed = 2f;
                    }
                    else
                    {
                         if (push.slowReference.slowed == false)
                         {
                              push.slowReference = null;
                              push.currentMaxSpeed = movement.maxSpeed;
                              movement.maxSpeed = 2f;
                         }
                    }

               }
               if ((_horizontal == 0 && _vertical == 0) && stateCharacter != CharacterState.PUSHING_IDLE)
               {
                    stateCharacter = CharacterState.PUSHING_IDLE;
               }
               else if ((_horizontal != 0 || _vertical != 0) && stateCharacter != CharacterState.PUSHING)
               {
                    stateCharacter = CharacterState.PUSHING;
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

     #region  Stunning Enemy
     public void StunningEnemy()
     {
          if (Input.GetKeyDown(KeyCode.E) && stun.canStun == true)
          {
               StunEnemy();
               StartCoroutine("CooldownStun");
          }
     }
     public void StunEnemy()
     {
          _distanceBetwen = Vector3.Distance(transform.position, EnemyController.instance.transform.position);

          if (_distanceBetwen <= stun.rangeStun && EnemyController.instance.stateEnemy != EnemyState.STUNNED)
          {
               StartCoroutine("TimeStuned");
          }
     }

     public IEnumerator TimeStuned()
     {
          EnemyController.instance.patrol.enemyAgent.speed = 0;
          EnemyController.instance.stateEnemy = EnemyState.STUNNED;

          yield return new WaitForSeconds(stun.timeStun);

          EnemyController.instance.patrol.enemyAgent.speed = 4;
          EnemyController.instance.stateEnemy = EnemyState.PATROLLING;
     }

     public IEnumerator CooldownStun()
     {
          stun.canStun = false;
          yield return new WaitForSeconds(stun.cooldownStun);
          stun.canStun = true;
     }
     #endregion

     #region Movement Assistance
     public void CliffDetector()
     {
          Vector3 _origin = transform.position + characterGraphic.forward * cliff.cliffDetectorFwrdDist;

          Ray _ray = new Ray(_origin, Vector3.up * -1);

          if (!Physics.Raycast(_ray, cliff.cliffDetectorHeightDist) && IsGrounded() && _cliffDectorLockPlayer == true && GetLocomotionSpeed() < cliff.cliffDetectorMaxSpeed)
          {
               if (stateCharacter != CharacterState.BALANCE)
               {
                    stateCharacter = CharacterState.BALANCE;
               }
          }
          else if (Physics.Raycast(_ray, cliff.cliffDetectorHeightDist))
          {
               _cliffDectorLockPlayer = true;
               if (stateCharacter == CharacterState.BALANCE)
               {
                    stateCharacter = CharacterState.RUNNNING;
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

     #region Animations
     public void PlayerAnimations()
     {
          animator.SetFloat("Horizontal", _horizontal);
          animator.SetFloat("Vertical", _vertical);
          animator.SetBool("IsGrounded", IsGrounded());

          switch (stateCharacter)
          {
               case CharacterState.IDLE:
                    animator.SetBool("Idle", true);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
                    break;
               case CharacterState.RUNNNING:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", true);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
                    break;
               case CharacterState.SINGLE_JUMP:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", true);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
                    break;
               case CharacterState.SINGLE_JUMP_RUNNING:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", true);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
                    break;
               case CharacterState.DOUBLE_JUMP:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", true);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
                    break;
               case CharacterState.PUSHING_IDLE:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", true);
                    break;
               case CharacterState.PUSHING:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", true);
                    animator.SetBool("Pushing Idle", false);
                    break;
               case CharacterState.DEAD:
                    // animator.SetBool("Idle", false);
                    // animator.SetBool("Running", false);
                    // animator.SetBool("Single Jump", false);
                    // animator.SetBool("Single Jump Running", false);
                    // animator.SetBool("Double Jump", false);
                    // animator.SetBool("Pushing", false);
                    // animator.SetBool("Pushing Idle", false);
                    // animator.SetBool("Dying", true);
                    break;
               case CharacterState.DISABLED:
                    // animator.SetBool("Idle", false);
                    // animator.SetBool("Running", false);
                    // animator.SetBool("Single Jump", false);
                    // animator.SetBool("Single Jump Running", false);
                    // animator.SetBool("Double Jump", false);
                    // animator.SetBool("Pushing", false);
                    // animator.SetBool("Pushing Idle", false);
                    // animator.SetBool("Dying", true);
                    break;
               case CharacterState.BALANCE:
                    animator.SetBool("Idle", false);
                    animator.SetBool("Running", false);
                    animator.SetBool("Single Jump", false);
                    animator.SetBool("Single Jump Running", false);
                    animator.SetBool("Double Jump", false);
                    animator.SetBool("Pushing", false);
                    animator.SetBool("Pushing Idle", false);
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

          if (seeRangeStun)
          {
               Gizmos.color = Color.yellow;
               Gizmos.DrawWireSphere(transform.position, stun.rangeStun);
          }

          if (seeRangegroundDetector)
          {
               Debug.DrawRay(transform.position, Vector3.up * -1 * jump.groundDetectorRange, Color.blue);
          }

          if (seeRangeMissedJump)
          {
               Debug.DrawRay(missedJump.targetMissedJump.position, Vector3.down * missedJump.rangeRayMissedJump, Color.red);
          }
     }
#endif
}
