using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateController : MonoBehaviour
{
     void Update()
     {
          SetMovementState();
          SetSingleJumpState();
          SetDoubleJumpState();
          SetPushState();
          SetFallingIdleState();
          FallingGround();
     }

     public void SetMovementState()
     {
          if (PlayerController.instance.horizontal == 0 && PlayerController.instance.vertical == 0)
          {
               if (PlayerController.instance.movement.stateCharacter != CharacterState.IDLE &&
                        PlayerController.instance.IsGrounded() &&
                        PlayerController.instance.push.pushingObj == false &&
                        PlayerController.instance.movement.stateCharacter != CharacterState.DEAD &&
                        PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_IDLE &&
                        PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_GROUND)
               {
                    PlayerController.instance.movement.stateCharacter = CharacterState.IDLE;
               }
          }

          if ((PlayerController.instance.horizontal != 0 || PlayerController.instance.vertical != 0) &&
               PlayerController.instance.movement.stateCharacter != CharacterState.RUNNNING &&
               PlayerController.instance.IsGrounded() &&
               PlayerController.instance.movement.rbody.velocity.y <= 0 &&
               PlayerController.instance.push.pushingObj == false &&
               PlayerController.instance.movement.stateCharacter != CharacterState.DEAD)
          {
               PlayerController.instance.movement.stateCharacter = CharacterState.RUNNNING;
          }
     }

     public void SetSingleJumpState()
     {
          if (PlayerController.instance.horizontal == 0 && PlayerController.instance.vertical == 0)
          {
               if (PlayerController.instance.movement.rbody.velocity.y > 0 &&
                   !PlayerController.instance.IsGrounded() &&
                   PlayerController.instance.movement.stateCharacter != CharacterState.DEAD)
               {
                    if (PlayerController.instance.movement.stateCharacter != CharacterState.SINGLE_JUMP &&
                        PlayerController.instance.movement.stateCharacter != CharacterState.DOUBLE_JUMP &&
                        PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_IDLE &&
                        PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_GROUND)
                    {
                         PlayerController.instance.movement.stateCharacter = CharacterState.SINGLE_JUMP;
                    }
               }
          }

          if ((PlayerController.instance.horizontal != 0 ||
                      PlayerController.instance.vertical != 0) &&
                      PlayerController.instance.jump.currentJump == 1 &&
                      !PlayerController.instance.IsGrounded() &&
                      PlayerController.instance.movement.stateCharacter != CharacterState.SINGLE_JUMP_RUNNING &&
                      PlayerController.instance.movement.stateCharacter != CharacterState.DEAD &&
                      PlayerController.instance.movement.stateCharacter != CharacterState.DOUBLE_JUMP &&
                      PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_IDLE &&
                      PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_GROUND)
          {
               PlayerController.instance.movement.stateCharacter = CharacterState.SINGLE_JUMP_RUNNING;
          }
     }

     public void SetDoubleJumpState()
     {
          if (PlayerController.instance.jump.currentJump >= 2 &&
              PlayerController.instance.movement.stateCharacter != CharacterState.DOUBLE_JUMP &&
              (PlayerController.instance.movement.stateCharacter == CharacterState.SINGLE_JUMP ||
              PlayerController.instance.movement.stateCharacter == CharacterState.SINGLE_JUMP_RUNNING ||
              PlayerController.instance.movement.stateCharacter == CharacterState.FALLING_IDLE))
          {
               PlayerController.instance.movement.stateCharacter = CharacterState.DOUBLE_JUMP;
          }
     }

     public void SetPushState()
     {
          if ((PlayerController.instance.horizontal == 0 && PlayerController.instance.vertical == 0) &&
               PlayerController.instance.movement.stateCharacter != CharacterState.PUSHING_IDLE &&
               PlayerController.instance.movement.stateCharacter != CharacterState.DEAD &&
               PlayerController.instance.push.pushingObj == true)
          {
               PlayerController.instance.movement.stateCharacter = CharacterState.PUSHING_IDLE;
          }
          else if ((PlayerController.instance.horizontal != 0 || PlayerController.instance.vertical != 0) &&
                    PlayerController.instance.movement.stateCharacter != CharacterState.PUSHING &&
                    PlayerController.instance.movement.stateCharacter != CharacterState.DEAD &&
                    PlayerController.instance.push.pushingObj == true)
          {
               PlayerController.instance.movement.stateCharacter = CharacterState.PUSHING;
          }
     }

     public void SetFallingIdleState()
     {
          if (PlayerController.instance.movement.rbody.velocity.y < 0f &&
              PlayerController.instance.jump.currentJump != 0 &&
              !PlayerController.instance.IsGrounded() &&
              PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_IDLE &&
              PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_GROUND &&
              PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_RUNNING)
          {
               PlayerController.instance.movement.stateCharacter = CharacterState.FALLING_IDLE;
          }
     }

     public void FallingGround()
     {
          if (PlayerController.instance.movement.stateCharacter == CharacterState.FALLING_IDLE)
          {
               if (PlayerController.instance.IsGrounded())
               {
                    if (PlayerController.instance.horizontal == 0 &&
                        PlayerController.instance.vertical == 0 &&
                        PlayerController.instance.jump.currentJump != 1 &&
                        PlayerController.instance.movement.stateCharacter != CharacterState.IDLE &&
                        PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_GROUND &&
                        PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_RUNNING)
                    {
                         PlayerController.instance.movement.stateCharacter = CharacterState.FALLING_GROUND;
                         PlayerController.instance.jump.fallingDust.Play();
                    }
                    else if (PlayerController.instance.jump.currentJump != 1 &&
                             PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_RUNNING &&
                             PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_GROUND &&
                             PlayerController.instance.movement.stateCharacter != CharacterState.FALLING_RUNNING)
                    {
                         PlayerController.instance.movement.stateCharacter = CharacterState.FALLING_RUNNING;
                         PlayerController.instance.jump.fallingDust.Play();
                    }
               }
          }
     }
}
