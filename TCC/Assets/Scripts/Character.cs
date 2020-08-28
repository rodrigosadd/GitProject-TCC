using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterState stateCharacter;
    public Rigidbody rbody;
    public Transform characterGraphic;
    public Transform cam;

    [Header("Movement variables")]
    public float speed = 6f;
    public float turnSmoothtime = 0.1f;

    [Header("Jump variables")]
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public float groundDetectorRange = 1f;
    public float jumpForce = 5f;
    public int maxJump = 2;
    public LayerMask groundLayer;

    [Header("Push variables")]
    public Transform targetPush;
    public float rangePush = 10f;
    public float rangeDropObject = 2f;

    private Transform _currentTargetPush = null;
    private float _horizontal, _vertical;
    private float _turnSmoothVelocity;
    private int _currentJump;

    public void CharacterMovement(float horizontal, float vertical)
    {
        _horizontal = horizontal;
        _vertical = vertical;

        Vector3 direction = new Vector3(_horizontal, 0f, _vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(characterGraphic.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothtime);

            characterGraphic.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            transform.Translate(moveDirection.normalized * speed * Time.deltaTime);
        }
    }

    public void CharacterJump()
    {
        if (Input.GetButtonDown("Jump") && CanJump() && stateCharacter != CharacterState.PUSHING)
        {
            stateCharacter = CharacterState.JUMP;
            rbody.velocity = Vector3.up * jumpForce;
            _currentJump++;
        }
        if (IsGrounded() && stateCharacter == CharacterState.JUMP && _currentJump >= maxJump)
        {
            _currentJump = 0;
            stateCharacter = CharacterState.NORMAL;
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
        Ray ray = new Ray(transform.position, Vector3.up * -1);
        return Physics.Raycast(ray, groundDetectorRange, groundLayer);
    }

    public bool CanJump()
    {
        return _currentJump < maxJump;
    }

    public void PushObject()
    {
        RaycastHit hit;

        if (Physics.Raycast(characterGraphic.position, characterGraphic.forward + Vector3.up, out hit, rangePush))
        {
            if (hit.transform.tag == "Interactable")
            {
                stateCharacter = CharacterState.PUSHING;
                hit.transform.position = targetPush.position;
                _currentTargetPush = hit.transform;
                hit.transform.parent = targetPush.transform;
                speed = 3f;
            }
        }
    }

    public void DropObject()
    {
        RaycastHit hit;

        if (Physics.Raycast(_currentTargetPush.position, Vector3.up * -1, out hit, rangeDropObject))
        {
            if (hit.transform.position != null)
            {
                stateCharacter = CharacterState.NORMAL;
                _currentTargetPush.parent = null;
                Vector3 pivotCorrection = new Vector3(0f, 0.8f, 0f); // Just to correct the pivot of unity objects
                _currentTargetPush.position = hit.point + pivotCorrection;
                speed = 6f;
            }
        }

    }
}
