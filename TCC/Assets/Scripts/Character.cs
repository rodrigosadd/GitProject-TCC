using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Rigidbody rbody;
    public Transform characterGraphic;
    public Transform cam;
    public float speed = 6f;
    public float turnSmoothtime = 0.1f;
    public float jumpForce = 5f;
    public float fallMultiplier = 2.5f;
    public float lowJumpMultiplier = 2f;
    public int maxJump = 2;

    private float horizontal, vertical;
    private float turnSmoothVelocity;
    private int currentJump;

    void Update()
    {
        CharacterMovement();
        CharacterJump();
        CharacterBetterJump();
    }

    public void CharacterMovement()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(characterGraphic.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothtime);

            characterGraphic.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

            transform.Translate(moveDirection.normalized * speed * Time.deltaTime);
        }
    }

    public void CharacterJump()
    {
        if (Input.GetButtonDown("Jump") && CanJump())
        {
            rbody.velocity = Vector3.up * jumpForce;
            currentJump++;
        }
        if (rbody.velocity.y == 0)
        {
            currentJump = 0;
        }
    }

    public void CharacterBetterJump()
    {
        if (rbody.velocity.y < 0)
        {
            rbody.velocity += Vector3.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rbody.velocity.y > 0 && !Input.GetButton("Jump"))
        {
            rbody.velocity += Vector3.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public bool CanJump()
    {
        return currentJump < maxJump;
    }
}
