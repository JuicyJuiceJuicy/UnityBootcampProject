using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseInputSystem : MonoBehaviour
{
    private void Start()
    {

    }

    private void Update()
    {
        MouseInputUpdate();
    }

    public float angleAdjust;
    public float timeHoldAdjust;

    private Vector2 mousePosition;

    private Vector2 mousePositionDown;
    private Vector2 mousePositionDelta;

    private float mouseTimeHold;
    private float mouseDistance;
    private float mouseAngle;

    private void MouseInputUpdate()
    {
#if UNITY_STANDALONE_WIN //==================================================================================================================================================================================================
        
        if (Input.GetMouseButton(0) == true) //누른 상태
        {
            if (Input.GetMouseButtonDown(0) == true) // 누른 순간
            {
                Cursor.lockState = CursorLockMode.None;

                mousePositionDown = Input.mousePosition;

                mouseTimeHold = 0;                                  // 시간 초기화
                mouseDistance = 0;                                  // 거리 초기화
                mouseAngle = 0;                                     // 각도 초기화
            }

            mousePosition = Input.mousePosition;
            mousePositionDelta = mousePosition - mousePositionDown; // 벡터 변화량 갱신

            mouseTimeHold += Time.deltaTime;                                                            // 시간 갱신
            mouseDistance = mousePositionDelta.magnitude;                                               // 거리 갱신
            mouseAngle = Mathf.Atan2(mousePositionDelta.y, mousePositionDelta.x) * Mathf.Rad2Deg;       // 각도 갱신

            if (mouseAngle < 0)
            {
                mouseAngle += 360;
            }





        }
        else if (Input.GetMouseButtonUp(0) == true) //뗀 순간
        {
            // 출력
            Debug.Log($"Angle: {mouseAngle},Time: {mouseTimeHold}, Distance:{mouseDistance}");
        }
        else //Input.GetMouseButton(0) == false // 누르지 않았을 때
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

#elif UNITY_ANDROID //=======================================================================================================================================================================================================
#endif
    }
}
