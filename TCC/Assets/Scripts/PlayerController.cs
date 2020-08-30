﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{

    [Header("Cliff variables")]
    public float cliffDetectorFwrdDist = 5f;
    public float cliffDetectorHeightDist = 5f;
    public float cliffDetectorMaxSpeed = 0.05f;

#if UNITY_EDITOR
    [Header("See Range variables")]
    public bool seeRangePush = false;
    public bool seeRangeStun = false;
    public bool seeRangeCliff = false;
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
            stateCharacter = CharacterState.WALKING;
        }
        CharacterMovement(_horizontal, _vertical);
    }

    private void PushingObject()
    {
        if (Input.GetMouseButtonDown(0))
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
                stateCharacter = CharacterState.WALKING;
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

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (seeRangePush)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, rangePush);
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
    }
#endif
}
