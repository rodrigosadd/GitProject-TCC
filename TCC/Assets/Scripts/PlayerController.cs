using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public bool seeRengePush = false;

    [Header("Cliff variables")]
    public float cliffDetectorFwrdDist = 5f;
    public float cliffDetectorHeightDist = 5f;
    public float cliffDetectorMaxSpeed = 0.05f;

    private bool cliffDectorLockPlayer;
    private float _horizontal, _vertical;
    private Vector3 lastPosition;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        UpdateMovementPlayer();
        CharacterJump();
        CharacterBetterJump();
        PushingObject();
        CliffDetector();
        CharacterFace();
    }

    private void UpdateMovementPlayer()
    {
        _horizontal = Input.GetAxis("Horizontal");
        _vertical = Input.GetAxis("Vertical");

        if (stateCharacter == CharacterState.BALANCE && (_horizontal != 0 || _vertical != 0))
        {
            cliffDectorLockPlayer = false;
            _horizontal = 0;
            _vertical = 0;
            return;
        }
        else if (stateCharacter == CharacterState.BALANCE)
        {
            stateCharacter = CharacterState.NORMAL;
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
        Vector3 origin = transform.position + characterGraphic.forward * cliffDetectorFwrdDist;

        Ray ray = new Ray(origin, Vector3.up * -1);

        if (!Physics.Raycast(ray, cliffDetectorHeightDist) && IsGrounded() && cliffDectorLockPlayer == true && GetLocomotionSpeed() < cliffDetectorMaxSpeed)
        {
            if (stateCharacter != CharacterState.BALANCE)
            {
                stateCharacter = CharacterState.BALANCE;
            }
        }
        else if (Physics.Raycast(ray, cliffDetectorHeightDist))
        {
            cliffDectorLockPlayer = true;
            if (stateCharacter == CharacterState.BALANCE)
            {
                stateCharacter = CharacterState.NORMAL;
            }
        }
    }

    public float GetLocomotionSpeed()
    {
        if (lastPosition != transform.position)
        {
            Vector2 lastPositionVec = new Vector2(lastPosition.x, lastPosition.z);
            Vector2 lastPositionPlayer = new Vector2(transform.position.x, transform.position.z);

            lastPosition = transform.position;

            return Vector2.Distance(lastPosition, lastPositionPlayer);
        }
        return 0;
    }

    void OnDrawGizmos()
    {
        if (seeRengePush)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, rangePush);
        }
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + characterGraphic.forward * cliffDetectorFwrdDist, 0.3f);
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position + characterGraphic.forward * cliffDetectorFwrdDist, Vector3.up * -1 * cliffDetectorHeightDist);
    }
}
