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

     [System.Serializable]
     public class Movement
     {
          public Vector3 velocity;
          public LayerMask groundMask;
          public CharacterController controller;
          public Transform cam;
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
          public bool slowing;
          public bool sliding;
          public bool teleporting;
          public float horizontal, vertical;
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

     [Header("Push variables")]
     public Push push;
     private float _countdownAfterDropping;
     private float _countdownSetPositionDropObject;

     [System.Serializable]
     public class Push
     {
          public Slow slowReference;
          public Transform targetDropPush;
          public Transform targetPushLight;
          public Transform targetPushHeavy;
          public Transform currentTargetPush;
          public Transform middleOfThePlayer;
          public float rangePush;
          public float rangeDrop;
          public float timeToReturnMovement;
          public float timeToSetPositionDropObject;
          public float speedPushLight;
          public float speedPushHeavy;
          public float velocityDropObject;
          public float maxDistanceCurrentObject;
          public bool pushingObj;
          public bool droppingObj;
          public bool setPositionDropObject;
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
          public float offsetDead;
          public bool dead;
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
          instance = this;

          Cursor.visible = false;
          Cursor.lockState = CursorLockMode.Locked;
          movement.fixedMaxSpeed = movement.maxSpeed;
          movement.fixedGravity = movement.gravity;
     }

     void Update()
     {
          Gravity();
          CheckIsGrounded();
          CharacterJump();
          //CharacterBetterJump();
          PushingObject();
          SetPositionCurrentTargetPush();
          SetPositionDropObject();
          CountdownAfterDropping();
          CountdownSetPositionDropObject();
          CliffDetector();
          CharacterFace();
          SlopeDetector();
          SetHandShader();
          ResetCurrentJump();
          CatchMissedJumps();
          JumpShadow();
          CheckDeath();
          CountdownAfterDeath();
     }

     void FixedUpdate()
     {
          UpdateMovementPlayer();
     }

     public void SetControllerPosition(Vector3 toPosition)
     {
          movement.controller.enabled = false;
          transform.position = toPosition;
          movement.controller.enabled = true;
     }

     #region Movement Player
     private void UpdateMovementPlayer()
     {
          if (!death.dead && !movement.sliding && !push.droppingObj)
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

               movement.controller.Move(_moveDirection.normalized * movement.currentSpeed * Time.deltaTime);
          }
          else
          {
               movement.currentSpeed = 0f;
          }
     }

     public void CharacterFace()
     {
          if (!death.dead && !movement.teleporting && !movement.sliding && !push.droppingObj)
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

          if (Input.GetButtonDown("Jump") && CanJump() &&
              push.pushingObj == false &&
              !movement.sliding &&
              !PlayerAttackController.instance.attaking &&
              PlayerAttackController.instance.currentAttack == 0 &&
              !GameManager.instance.settingsData.settingsOpen &&
              !PlayerAnimationController.instance.afterFalling &&
              (jump.doubleJumpCountdown >= 1 || jump.currentJump == 0))
          {
               movement.velocity.y = Mathf.Sqrt(2f * -2f * movement.gravity);
               PlayerAnimationController.instance.fallingIdle = false;
               jump.doubleJumpCountdown = 0;
               jump.currentJump++;
          }
     }

     public void ResetCurrentJump()
     {
          if (movement.isGrounded &&
              movement.velocity.y < 0 &&
              !PlayerAnimationController.instance.fallingIdle &&
              jump.currentJump > 0)
          {
               PlayerAnimationController.instance.fallingIdle = false;
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

     // public void CharacterBetterJump()
     // {
     //      if (movement.velocity.y <= 0)
     //      {
     //           movement.velocity += Vector3.up * Physics.gravity.y * (jump.fallMultiplier - 1) * Time.deltaTime;
     //      }
     //      else if (movement.velocity.y > 0 && !Input.GetButton("Jump"))
     //      {
     //           movement.velocity += Vector3.up * Physics.gravity.y * (jump.lowJumpMultiplier - 1) * Time.deltaTime;
     //      }
     // }

     public bool CanJump()
     {
          return jump.currentJump < jump.maxJump;
     }

     public void JumpShadow()
     {
          if (!movement.isGrounded)
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

     public void SlopeDetector()
     {
          RaycastHit _hitInfo;

          if (Physics.Raycast(push.middleOfThePlayer.position, push.middleOfThePlayer.up * -1, out _hitInfo, jump.rangeSlopeDetector))
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

     #region Pushing Object
     private void PushingObject()
     {
          if (Input.GetButton("Push") &&
              movement.isGrounded &&
              PlayerAttackController.instance.currentAttack == 0)
          {
               PushObject();
          }
          else if (Input.GetButtonUp("Push"))
          {
               DropObject();
          }

          if (push.currentTargetPush != null && !push.currentTargetPush.gameObject.activeSelf)
          {
               DropObject();
          }
     }

     public void PushObject()
     {
          RaycastHit _hit;

          if (push.currentTargetPush == null)
          {
               if (Physics.Raycast(push.middleOfThePlayer.position, push.middleOfThePlayer.forward, out _hit, push.rangePush))
               {
                    if (_hit.transform.tag == "Light")
                    {
                         push.pushingObj = true;
                         _hit.transform.position = push.targetPushLight.position;
                         push.currentTargetPush = _hit.transform;
                         SetLightPushSpeed();
                    }
                    else if (_hit.transform.tag == "Heavy")
                    {
                         push.pushingObj = true;
                         _hit.transform.position = push.targetPushHeavy.position;
                         push.currentTargetPush = _hit.transform;
                         SetHeavyPushSpeed();
                    }
               }
          }
     }

     public void SetPositionCurrentTargetPush()
     {
          if (push.currentTargetPush != null && !push.setPositionDropObject)
          {
               if (push.currentTargetPush.tag == "Light")
               {
                    push.currentTargetPush.position = push.targetPushLight.position;
                    push.currentTargetPush.rotation = push.targetPushLight.rotation;
               }
               else if (push.currentTargetPush.tag == "Heavy")
               {
                    push.currentTargetPush.position = push.targetPushHeavy.position;
                    push.currentTargetPush.rotation = push.targetPushHeavy.rotation;
               }
          }
     }

     public void DropObject()
     {
          if (push.currentTargetPush != null)
          {
               if (push.currentTargetPush.tag == "Light")
               {
                    push.pushingObj = false;
                    push.droppingObj = true;
                    push.setPositionDropObject = true;
                    movement.maxSpeed = movement.fixedMaxSpeed;
                    push.slowReference = null;
                    movement.turnSmoothtime = 0.15f;
               }
               else if (push.currentTargetPush.tag == "Heavy")
               {
                    push.pushingObj = false;
                    push.droppingObj = true;
                    push.setPositionDropObject = true;
                    movement.maxSpeed = movement.fixedMaxSpeed;
                    push.slowReference = null;
                    movement.turnSmoothtime = 0.15f;
               }

          }

     }

     public void SetPositionDropObject()
     {
          if (push.currentTargetPush != null && push.setPositionDropObject)
          {
               if (push.currentTargetPush.tag == "Light")
               {
                    push.currentTargetPush.position = Vector3.MoveTowards(push.currentTargetPush.position, push.targetDropPush.position, push.velocityDropObject * Time.deltaTime);
                    push.currentTargetPush.rotation = push.targetDropPush.rotation;
               }
               else if (push.currentTargetPush.tag == "Heavy")
               {
                    push.currentTargetPush.position = Vector3.MoveTowards(push.currentTargetPush.position, push.targetDropPush.position, push.velocityDropObject * Time.deltaTime);
                    push.currentTargetPush.rotation = push.targetDropPush.rotation;
               }
          }
     }

     public void CountdownSetPositionDropObject()
     {
          if (push.setPositionDropObject && !push.pushingObj)
          {
               if (_countdownSetPositionDropObject < 1)
               {
                    _countdownSetPositionDropObject += Time.deltaTime / push.timeToSetPositionDropObject;
               }
               else
               {
                    _countdownSetPositionDropObject = 0;
                    push.setPositionDropObject = false;
                    push.currentTargetPush = null;
                    movement.currentSpeed = 0f;
               }
          }
     }

     public void CountdownAfterDropping()
     {
          if (push.droppingObj && !push.pushingObj)
          {
               if (_countdownAfterDropping < 1)
               {
                    _countdownAfterDropping += Time.deltaTime / push.timeToReturnMovement;
               }
               else
               {
                    _countdownAfterDropping = 0;
                    push.droppingObj = false;
               }
          }
     }

     public void SetLightPushSpeed()
     {
          if (push.slowReference == null)
          {
               movement.maxSpeed = push.speedPushLight;
               movement.turnSmoothtime = 0.25f;
          }
          else
          {
               if (movement.slowing == false)
               {
                    push.slowReference = null;
                    movement.maxSpeed = push.speedPushLight;
                    movement.turnSmoothtime = 0.25f;
               }
          }
     }

     public void SetHeavyPushSpeed()
     {
          if (push.slowReference == null)
          {
               movement.maxSpeed = push.speedPushHeavy;
               movement.turnSmoothtime = 0.4f;
          }
          else
          {
               if (movement.slowing == false)
               {
                    push.slowReference = null;
                    movement.maxSpeed = push.speedPushHeavy;
                    movement.turnSmoothtime = 0.4f;
               }
          }
     }
     #endregion

     #region Movement Assistance
     public void CliffDetector()
     {
          Vector3 _origin = transform.position + characterGraphic.forward * cliff.cliffDetectorFwrdDist;

          Ray _ray = new Ray(_origin, Vector3.up * -1);

          if (!Physics.Raycast(_ray, cliff.cliffDetectorHeightDist) && movement.isGrounded && _cliffDectorLockPlayer == true && GetLocomotionSpeed() < cliff.cliffDetectorMaxSpeed)
          {
               PlayerAnimationController.instance.SetBalance();
          }
          else if (Physics.Raycast(_ray, cliff.cliffDetectorHeightDist))
          {
               _cliffDectorLockPlayer = true;
               PlayerAnimationController.instance.balance = false;
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
                   _hitInfo.transform.tag == "Heavy")
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
                    _currentMaxSpeed = movement.fixedMaxSpeed;
                    movement.maxSpeed = 0;
                    death.dead = true;
               }
          }
     }

     public void CountdownAfterDeath()
     {
          if (death.dead)
          {
               if (_countdownDeath < 1)
               {
                    _countdownDeath += Time.deltaTime / 2f;
               }
               else
               {
                    PlayerAttackController.instance.ResetAttack();
                    hit.hitCount = 0;
                    SetControllerPosition(death.currentPoint.position);
                    movement.maxSpeed = _currentMaxSpeed;
                    _countdownDeath = 0;
                    death.dead = false;
                    PlayerController.instance.animator.SetBool("Idle", true);
                    PlayerController.instance.animator.SetBool("Dying", false);
               }
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
               Gizmos.color = Color.red;
               Gizmos.DrawLine(push.targetDropPush.position, push.targetDropPush.position + Vector3.down * push.rangeDrop);
          }

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
