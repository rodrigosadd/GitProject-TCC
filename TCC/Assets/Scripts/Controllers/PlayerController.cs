using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
     [Header("Animations variables")]
     public Animator animator;

     [Header("Cliff variables")]
     public float cliffDetectorFwrdDist = 5f;
     public float cliffDetectorHeightDist = 5f;
     public float cliffDetectorMaxSpeed = 0.05f;

     [Header("Missed Jumps variables")]
     public Transform targetMissedJump;
     public float rangeRayMissedJump;
     public float offsetPlayerPos;
     public float timeDetectMissedJump;
     public float countdownMissedJump;
     public float smootnessMissedJump;
     public bool canMiss = true;

#if UNITY_EDITOR
     [Header("See Range variables")]
     public bool seeRangePush = false;
     public bool seeRangeStun = false;
     public bool seeRangeCliff = false;
     public bool seeRangegroundDetector = false;
     public bool seeRangeMissedJump = false;
#endif

     public static PlayerController instance;

     private bool _cliffDectorLockPlayer;
     private float _horizontal, _vertical;
     private Vector3 _lastPosition;

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
          PlatformDetector();
          PlayerAnimations();
          CatchMissedJumps();
     }

     void LateUpdate()
     {
          JumpShadow();
     }

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

     public void CliffDetector()
     {
          Vector3 _origin = transform.position + characterGraphic.forward * cliffDetectorFwrdDist;

          Ray _ray = new Ray(_origin, Vector3.up * -1);

          if (!Physics.Raycast(_ray, cliffDetectorHeightDist) && IsGrounded() && _cliffDectorLockPlayer == true && GetLocomotionSpeed() < cliffDetectorMaxSpeed)
          {
               if (stateCharacter != CharacterState.BALANCE)
               {
                    stateCharacter = CharacterState.BALANCE;
               }
          }
          else if (Physics.Raycast(_ray, cliffDetectorHeightDist))
          {
               _cliffDectorLockPlayer = true;
               if (stateCharacter == CharacterState.BALANCE)
               {
                    stateCharacter = CharacterState.RUNNNING;
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

     public void StunningEnemy()
     {
          if (Input.GetKeyDown(KeyCode.E) && canStun == true)
          {
               StunEnemy();
               StartCoroutine("CooldownStun");
          }
     }

     public void CatchMissedJumps()
     {
          RaycastHit _hitInfo;

          if (Physics.Raycast(targetMissedJump.position, Vector3.down, out _hitInfo, rangeRayMissedJump))
          {
               if (_hitInfo.transform.tag == "Ground" || _hitInfo.transform.tag == "Interactable")
               {
                    if (canMiss)
                    {
                         transform.position = Vector3.MoveTowards(transform.position, _hitInfo.point + new Vector3(0f, offsetPlayerPos, 0f), smootnessMissedJump * Time.deltaTime);
                         canMiss = false;
                    }
               }
          }

          if (!canMiss)
          {
               if (countdownMissedJump < 1)
               {
                    countdownMissedJump += Time.deltaTime / timeDetectMissedJump;
               }
               else
               {
                    countdownMissedJump = 0;
                    canMiss = true;
               }
          }
     }

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          if (seeRangePush)
          {
               Gizmos.color = Color.magenta;
               Gizmos.DrawLine(middleOfThePlayer.position, middleOfThePlayer.position + middleOfThePlayer.forward * rangePush);
          }

          if (seeRangeCliff)
          {
               Gizmos.color = Color.red;
               Gizmos.DrawSphere(transform.position + characterGraphic.forward * cliffDetectorFwrdDist, 0.3f);
               Gizmos.color = Color.green;
               Gizmos.DrawRay(transform.position + characterGraphic.forward * cliffDetectorFwrdDist, Vector3.up * -1 * cliffDetectorHeightDist);
          }

          if (seeRangeStun)
          {
               Gizmos.color = Color.yellow;
               Gizmos.DrawWireSphere(transform.position, rangeStun);
          }

          if (seeRangegroundDetector)
          {
               Debug.DrawRay(transform.position, Vector3.up * -1 * groundDetectorRange, Color.blue);
          }

          if (seeRangeMissedJump)
          {
               Debug.DrawRay(targetMissedJump.position, Vector3.down * rangeRayMissedJump, Color.red);
          }
     }
#endif
}
