using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class InputSystemManager : SingletonLazy<InputSystemManager>
{
    private void Start()
    {
        InputStart();
    }

    private void Update()
    {
        InputUpdate();
        InputStateDebugUpdate(inputState);
    }

    // InputStates ==========================================================================================================================================================================================================

    private enum InputState
    {
        idle,
        push,
        tab,
        hold,
        holdSwipe,
        swipe,
        swipeHold,
    }

    private enum InputDirection
    {
        neutral,
        right,
        rightUp,
        up,
        leftUp,
        left,
        leftDown,
        down,
        rightDown,
    }

    // Input ================================================================================================================================================================================================================

    // ���μ���
    public float angleAdjust;
    public float distanceAdjust;
    public float timeHoldAdjustMS;
    public float timeSwipeHoldAdjustMS;

    private float timeHoldAdjust;
    private float timeSwipeHoldAdjust;

    // ���콺 ��ġ
    private Vector2 mousePosition;
    private Vector2 mousePositionDelta;
    private Vector2 mousePositionDown;

    // �ð�, �Ÿ�, ����
    private float mouseTimeHold;
    private float mouseDistance;
    private float mouseAngle;

    // ���
    private InputState inputState;
    private InputDirection inputDirection;

    private void InputStart()
    {
        timeHoldAdjust = timeHoldAdjustMS * 0.001f;
        timeSwipeHoldAdjust = timeSwipeHoldAdjustMS * 0.001f;
    }

    private void InputUpdate()
    {
#if UNITY_STANDALONE_WIN

        bool _isInputDown = Input.GetMouseButtonDown(0) == true;
        bool _isInput = Input.GetMouseButton(0) == true;
        bool _isInputUp = Input.GetMouseButtonUp(0) == true;

        mousePosition = Input.mousePosition;

#elif UNITY_ANDROID
#endif

        if (_isInput) //���� ����
        {
            if (_isInputDown) // ���� ����
            {
                Cursor.lockState = CursorLockMode.None;

                mouseTimeHold = 0;                                  // �ð� �ʱ�ȭ
                mouseDistance = 0;                                  // �Ÿ� �ʱ�ȭ
                mouseAngle = 0;                                     // ���� �ʱ�ȭ

                mousePositionDown = Input.mousePosition;
            }

            mousePositionDelta = mousePosition - mousePositionDown; // ���� ��ȭ�� ����

            mouseTimeHold += Time.deltaTime;                                                                            // �ð� ����
            mouseDistance = mousePositionDelta.magnitude;                                                               // �Ÿ� ����
            mouseAngle = Mathf.Atan2(mousePositionDelta.y, mousePositionDelta.x) * Mathf.Rad2Deg + angleAdjust + 22.5f; // ���� ����

            if (mouseAngle < 0)
            {
                mouseAngle += 360;
            }

            if (mouseDistance < distanceAdjust)
            {
                if (mouseTimeHold < timeHoldAdjust)
                {
                    inputState = InputState.push;
                }
                else // timeHoldAdjust < mouseTimeHold, Ȧ���
                {
                    inputState = InputState.hold;
                }

                inputDirection = InputDirection.neutral;
            }
            else //distanceAdjust < mouseDistance, ����������
            {
                if (timeSwipeHoldAdjust < mouseTimeHold)
                {
                    switch (inputState)
                    {
                        case InputState.swipe:
                            inputState = InputState.swipeHold;
                            break;
                    }
                }

                switch (inputState)
                {
                    case InputState.push:
                        inputState = InputState.swipe;
                        break;
                    case InputState.swipeHold:
                        inputState = InputState.swipeHold;
                        break;
                    case InputState.hold:
                        inputState = InputState.holdSwipe;
                        break;
                    case InputState.holdSwipe:
                        inputState = InputState.holdSwipe;
                        break;
                }

                //inputDirection
                switch ((int)mouseAngle / 45)
                {
                    case 0:
                        inputDirection = InputDirection.right;
                        break;
                    case 1:
                        inputDirection = InputDirection.rightUp;
                        break;
                    case 2:
                        inputDirection = InputDirection.up;
                        break;
                    case 3:
                        inputDirection = InputDirection.leftUp;
                        break;
                    case 4:
                        inputDirection = InputDirection.left;
                        break;
                    case 5:
                        inputDirection = InputDirection.leftDown;
                        break;
                    case 6:
                        inputDirection = InputDirection.down;
                        break;
                    case 7:
                        inputDirection = InputDirection.rightDown;
                        break;
                }
            }
        }
        else if (_isInputUp) //�� ����
        {
            if (mouseDistance < distanceAdjust)
            {
                if (mouseTimeHold < timeHoldAdjust)
                {
                    inputState = InputState.tab;
                }
            }
            else //distanceAdjust < mouseDistance
            {

            }

            Debug.Log($"Angle: {mouseAngle},Time: {mouseTimeHold}, Distance:{mouseDistance}");
        }
        else //_isInput == false // ������ ���� ����
        {
            Cursor.lockState = CursorLockMode.Locked;

            inputState = InputState.idle;
            inputDirection = InputDirection.neutral;
        }
    }

    // InputStateDebug ======================================================================================================================================================================================================

    [SerializeField] Text textStateDebug;

    private void InputStateDebugUpdate(InputState inputState)
    {
        if ((inputState == InputState.idle) == false)
        {
            textStateDebug.text = inputState.ToString() + " " + inputDirection;
        }
    }
}