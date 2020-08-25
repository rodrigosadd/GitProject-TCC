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
        CharacterMovement();
        CharacterJump();
        CharacterBetterJump();
        PushingObject();
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
