using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : Character
{
    public bool seeRengePush = false;

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
    }

    private void UpdateMovementPlayer()
    {
        float _horizontal = Input.GetAxis("Horizontal");
        float _vertical = Input.GetAxis("Vertical");
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

    void OnDrawGizmos()
    {
        if (seeRengePush)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, rangePush);
        }
    }
}
