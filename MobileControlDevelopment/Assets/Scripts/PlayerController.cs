using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CharacterController
{


    void Start()
    {

    }

    void Update()
    {
        switch (InputSystemManager.Instance.CurrentInputState)
        {
            case InputSystemManager.InputState.idle:
                animator.SetInteger("State", (int)CharacterState.idle);
                break;
            case InputSystemManager.InputState.push:
                animator.SetInteger("State", (int)CharacterState.push);
                break;
            case InputSystemManager.InputState.swipeHold:
                animator.SetInteger("State", (int)CharacterState.move);
                break;
            case InputSystemManager.InputState.tab:
                animator.SetInteger("State", (int)CharacterState.attack);
                break;
            case InputSystemManager.InputState.swipeTab:
                animator.SetInteger("State", (int)CharacterState.dash);
                break;
        }

        switch (InputSystemManager.Instance.CurrentInputDirection)
        {
            case InputSystemManager.InputDirection.neutral:
                moveDirection = new Vector3(0, 0, 0);
                break;
            case InputSystemManager.InputDirection.right:
                moveDirection = new Vector3(1, 0, 0);
                break;
            case InputSystemManager.InputDirection.left:
                moveDirection = new Vector3(-1, 0, 0);
                break;
        }
    }

    public enum CharacterState
    {
        idle,
        push,
        move,
        attack,
        dash,
    }
}
