using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

using UnityEngine.UI;

public class InputSystemManager : SingletonLazy<InputSystemManager>
{
    private void Awake()
    {
        mainCamera = GameObject.Find(cameraName).GetComponent<Camera>();
        virtualController = GameObject.Find("VirtualController");
        virtualController.SetActive(false);
    }

    private void Start()
    {
        InputStart();
    }

    private void Update()
    {
        InputUpdate();
        InputStateDebugUpdate(inputState);
    }

    public Camera MainCamera
    { get => mainCamera; }
    [SerializeField] Camera mainCamera;
    [SerializeField] string cameraName;

    // InputStates ==========================================================================================================================================================================================================

    public enum InputState
    {
        idle,
        push,
        tab,
        hold,
        holdSwipe,
        swipe,
        swipeTab,
        swipeHold,
    }

    public enum InputDirection
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

    // 개인설정
    public float angleAdjust;
    public float distanceAdjust;
    public float timeHoldAdjustMS;
    public float timeSwipeHoldAdjustMS;

    private float timeHoldAdjust;
    private float timeSwipeHoldAdjust;

    // 마우스 위치 화면좌표
    public Vector2 MousePositionDown
    { get => mousePositionDown; }
    private Vector2 mousePositionDown;

    public Vector2 MousePosition
    { get => mousePosition; }
    private Vector2 mousePosition;

    private Vector2 mousePositionDelta;

    // 마우스 위치 월드좌표
    public Vector2 MousePositionDownScreen { get => mousePositionDownScreen; }
    private Vector2 mousePositionDownScreen { get => mainCamera.ScreenToWorldPoint(mousePositionDown); }

    public Vector2 MousePositionScreen { get => mousePositionScreen; }
    private Vector2 mousePositionScreen { get => mainCamera.ScreenToWorldPoint(mousePosition); }

    // 시간, 거리, 방향
    private float mouseTimeHold;
    private float mouseDistance;
    private float mouseAngle;

    // 출력
    public InputState CurrentInputState { get => inputState; }
    private InputState inputState;
    public InputDirection CurrentInputDirection {  get => inputDirection; }
    private InputDirection inputDirection;

    [SerializeField] GameObject virtualController;

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

        if (_isInput) //누른 상태
        {
            if (_isInputDown) // 누른 순간
            {
                //Cursor.lockState = CursorLockMode.None;

                mouseTimeHold = 0;                                  // 시간 초기화
                mouseDistance = 0;                                  // 거리 초기화
                mouseAngle = 0;                                     // 각도 초기화

                mousePositionDown = Input.mousePosition;

                virtualController.SetActive(true);
            }

            mousePositionDelta = mousePosition - mousePositionDown; // 벡터 변화량 갱신

            mouseTimeHold += Time.deltaTime;                                                                            // 시간 갱신
            mouseDistance = mousePositionDelta.magnitude;                                                               // 거리 갱신
            mouseAngle = Mathf.Atan2(mousePositionDelta.y, mousePositionDelta.x) * Mathf.Rad2Deg - angleAdjust + 22.5f; // 각도 갱신

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
                else // timeHoldAdjust < mouseTimeHold, 홀드시
                {
                    inputState = InputState.hold;
                }

                inputDirection = InputDirection.neutral;
            }
            else //distanceAdjust < mouseDistance, 스와이프시
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
        else if (_isInputUp) //뗀 순간
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
                if (mouseTimeHold < timeSwipeHoldAdjust)
                {
                    inputState = InputState.swipeTab;
                }
            }

            virtualController.SetActive(false);
            Debug.Log($"Angle: {mouseAngle},Time: {mouseTimeHold}, Distance:{mouseDistance}");
        }
        else //_isInput == false // 누르지 않은 상태
        {
            //Cursor.lockState = CursorLockMode.Locked;

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
