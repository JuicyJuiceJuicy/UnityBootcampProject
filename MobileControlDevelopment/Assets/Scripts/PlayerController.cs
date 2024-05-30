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
        MoveDirectionUpdate();
        SetMoveDirection();
        PlayerControl();
        
    }

    // SetMoveDirection =====================================================================================================
    void SetMoveDirection()
    {
        switch (VirtualControllerManager.Instance.CurrentInputDirection)
        {
            case VirtualControllerManager.InputDirection.neutral:
                moveDirection = new Vector3(0, 0, 0);
                break;
            case VirtualControllerManager.InputDirection.right:
                moveDirection = new Vector3(1, 0, 0);
                break;
            case VirtualControllerManager.InputDirection.rightUp:
                moveDirection = new Vector3(1, 1, 0);
                break;
            case VirtualControllerManager.InputDirection.up:
                moveDirection = new Vector3(0, 1, 0);
                break;
            case VirtualControllerManager.InputDirection.leftUp:
                moveDirection = new Vector3(-1, 1, 0);
                break;
            case VirtualControllerManager.InputDirection.left:
                moveDirection = new Vector3(-1, 0, 0);
                break;
            case VirtualControllerManager.InputDirection.leftDown:
                moveDirection = new Vector3(-1, -1, 0);
                break;
            case VirtualControllerManager.InputDirection.down:
                moveDirection = new Vector3(0, -1, 0);
                break;
            case VirtualControllerManager.InputDirection.rightDown:
                moveDirection = new Vector3(1, -1, 0);
                break;
        }
    }

    void PlayerControl()
    {
        switch (VirtualControllerManager.Instance.CurrentInputState)
        {
            case VirtualControllerManager.InputState.idle:
                
                break;
            case VirtualControllerManager.InputState.push:

                break;
            case VirtualControllerManager.InputState.swipeHold:
                if (moveDirection.y == 0)
                {
                    Move();
                }
                else if (moveDirection.y == 1)
                {
                    Jump();
                }
                else //(moveDirection.y == -1)
                {
                    // 슬라이딩? 다운? 넉백? 원거리?
                }
                break;
            case VirtualControllerManager.InputState.tab:
                Attack();
                break;
            case VirtualControllerManager.InputState.swipeTab:

                SetInputDirection();

                if (moveDirection.y == 0)
                {
                    Dodge();
                }
                else if (moveDirection.y == 1)
                {
                    Jump();
                }
                else //(moveDirection.y == -1)
                {
                    // 슬라이딩? 다운? 넉백? 원거리?
                }
                
                break;
        }
    }
}
