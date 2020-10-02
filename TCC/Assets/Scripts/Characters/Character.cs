using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Character : MonoBehaviour
{
    public CharacterState stateCharacter;
    public Rigidbody rbody;
    public Transform characterGraphic;
    public Transform cam;

    [Header("Movement variables")]
    public float maxSpeed = 6f;
    public float currentSpeed = 0f;
    public float acceleration = 1f;
    public float turnSmoothtime = 0.1f;

    [Header("Jump variables")]
    public Transform jumpShadow;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float groundDetectorRange = 1f;
    public float jumpForce = 5f;
    public int currentJump = 0;
    public int maxJump = 2;
    public float maxDistanceShadow = 10f;
    public LayerMask groundLayer;

    [Header("Push variables")]
    public Transform targetPush;
    public Transform currentTargetPush;
    public float rangePush = 10f;
    public float rangeDropObject = 2f;

    [Header("Stun variables")]
    public float rangeStun = 2f;
    public float timeStun = 2f;
    public float cooldownStun = 5f;
    public bool canStun = true;

    private Vector3 _direction;
    private float _distanceBetwen;
    private float _targetAngle;
    private float _angle;
    private float _horizontal, _vertical;
    private float _turnSmoothVelocity;

    public void CharacterMovement(float horizontal, float vertical)
    {
        _horizontal = horizontal;
        _vertical = vertical;

        _direction = new Vector3(_horizontal, 0f, _vertical).normalized;

        if (_direction.magnitude >= 0.1f)
        {
            currentSpeed += acceleration * Time.deltaTime;
            currentSpeed = Mathf.Clamp(currentSpeed, 0, maxSpeed);

            Vector3 _moveDirection = Quaternion.Euler(0f, _targetAngle, 0f) * Vector3.forward;

            transform.Translate(_moveDirection.normalized * currentSpeed * Time.deltaTime);
        }
        else
        {
            currentSpeed = 0f;
        }

        if ((_horizontal != 0 || _vertical != 0) && stateCharacter != CharacterState.RUNNNING && IsGrounded() && rbody.velocity.y == 0)
        {
            stateCharacter = CharacterState.RUNNNING;
        }
        else if ((_horizontal != 0 || _vertical != 0) && rbody.velocity.y > 0 && stateCharacter != CharacterState.SINGLE_JUMP_RUNNING)
        {
            stateCharacter = CharacterState.SINGLE_JUMP_RUNNING;
        }

        if (_horizontal == 0 && _vertical == 0)
        {
            if (rbody.velocity.y > 0 && !IsGrounded())
            {
                stateCharacter = CharacterState.SINGLE_JUMP;
            }
            else if (stateCharacter != CharacterState.IDLE)
            {
                stateCharacter = CharacterState.IDLE;
            }
        }
    }

    public void CharacterFace()
    {
        if (_direction.magnitude >= 0.1f)
        {
            _targetAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            _angle = Mathf.SmoothDampAngle(characterGraphic.eulerAngles.y, _targetAngle, ref _turnSmoothVelocity, turnSmoothtime);

            characterGraphic.rotation = Quaternion.Euler(0f, _angle, 0f);
        }
    }

    public void CharacterJump()
    {
        if (Input.GetButtonDown("Jump") && CanJump() && stateCharacter != CharacterState.PUSHING)
        {
            rbody.velocity = Vector3.up * jumpForce;
            currentJump++;
            if (currentJump < 2)
            {
                stateCharacter = CharacterState.SINGLE_JUMP;
            }
            else if (currentJump == 2)
            {
                stateCharacter = CharacterState.DOUBLE_JUMP;
            }
        }
        if (IsGrounded() && currentJump >= maxJump)
        {
            currentJump = 0;
            if (stateCharacter != CharacterState.RUNNNING)
            {
                stateCharacter = CharacterState.RUNNNING;
            }
        }
    }

    public void JumpShadow()
    {
        if (!IsGrounded())
        {
            RaycastHit _hitInfo;
            if (Physics.Raycast(transform.position, Vector3.up * -1, out _hitInfo, maxDistanceShadow))
            {
                if (_hitInfo.transform.tag != "Player")
                {
                    if (!jumpShadow.gameObject.activeSelf)
                    {
                        jumpShadow.transform.position = _hitInfo.point + new Vector3(0f, 0.05f, 0f);
                    }
                    jumpShadow.gameObject.SetActive(true);
                    jumpShadow.transform.position = Vector3.MoveTowards(jumpShadow.transform.position, _hitInfo.point + new Vector3(0f, 0.05f, 0f), 10f * Time.deltaTime);
                    jumpShadow.transform.rotation = Quaternion.FromToRotation(Vector3.up, _hitInfo.normal);
                }
            }
            else
            {
                jumpShadow.gameObject.SetActive(false);
            }
        }
        else
        {
            jumpShadow.gameObject.SetActive(false);
        }
    }

    public void CharacterBetterJump()
    {
        if (rbody.velocity.y <= 0)
        {
            rbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rbody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public bool IsGrounded()
    {
        Ray _ray = new Ray(transform.position, Vector3.up * -1);
        return Physics.Raycast(_ray, groundDetectorRange, groundLayer);
    }

    public bool CanJump()
    {
        return currentJump < maxJump;
    }

    public void PushObject()
    {
        RaycastHit _hit;

        if (Physics.Raycast(characterGraphic.position, characterGraphic.forward + Vector3.up, out _hit, rangePush))
        {
            if (_hit.transform.tag == "Interactable")
            {
                stateCharacter = CharacterState.PUSHING;
                _hit.transform.position = targetPush.position;
                currentTargetPush = _hit.transform;
                _hit.transform.parent = targetPush.transform;
                maxSpeed = 3f;
            }
        }
    }

    public void DropObject()
    {
        RaycastHit _hit;

        if (Physics.Raycast(currentTargetPush.position, Vector3.up * -1, out _hit, rangeDropObject))
        {
            if (_hit.transform.position != null)
            {
                stateCharacter = CharacterState.RUNNNING;
                currentTargetPush.parent = null;
                Vector3 pivotCorrection = new Vector3(0f, 0.8f, 0f); // Just to correct the pivot of unity objects
                currentTargetPush.position = _hit.point + pivotCorrection;
                maxSpeed = 9f;
            }
        }

    }

    public void StunEnemy()
    {
        _distanceBetwen = Vector3.Distance(transform.position, EnemyController.instance.transform.position);

        if (_distanceBetwen <= rangeStun && EnemyController.instance.stateEnemy != EnemyState.STUNNED)
        {
            StartCoroutine("TimeStuned");
        }
    }

    public IEnumerator TimeStuned()
    {
        EnemyController.instance.enemyAgent.speed = 0;
        EnemyController.instance.stateEnemy = EnemyState.STUNNED;

        yield return new WaitForSeconds(timeStun);

        EnemyController.instance.enemyAgent.speed = 4;
        EnemyController.instance.stateEnemy = EnemyState.PATROLLING;
    }

    public IEnumerator CooldownStun()
    {
        canStun = false;
        yield return new WaitForSeconds(cooldownStun);
        canStun = true;
    }

    public void PlatformDetector()
    {
        RaycastHit _hit;

        if (Physics.Raycast(characterGraphic.position, Vector3.up * -1, out _hit, groundDetectorRange))
        {
            if (_hit.transform.tag == "Platform")
            {
                transform.parent = _hit.transform;
            }
            else
            {
                transform.parent = null;
            }
        }
    }
}
