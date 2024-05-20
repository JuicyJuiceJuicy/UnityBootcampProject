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
        
        if (Input.GetMouseButton(0) == true) //���� ����
        {
            if (Input.GetMouseButtonDown(0) == true) // ���� ����
            {
                Cursor.lockState = CursorLockMode.None;

                mousePositionDown = Input.mousePosition;

                mouseTimeHold = 0;                                  // �ð� �ʱ�ȭ
                mouseDistance = 0;                                  // �Ÿ� �ʱ�ȭ
                mouseAngle = 0;                                     // ���� �ʱ�ȭ
            }

            mousePosition = Input.mousePosition;
            mousePositionDelta = mousePosition - mousePositionDown; // ���� ��ȭ�� ����

            mouseTimeHold += Time.deltaTime;                                                            // �ð� ����
            mouseDistance = mousePositionDelta.magnitude;                                               // �Ÿ� ����
            mouseAngle = Mathf.Atan2(mousePositionDelta.y, mousePositionDelta.x) * Mathf.Rad2Deg;       // ���� ����

            if (mouseAngle < 0)
            {
                mouseAngle += 360;
            }





        }
        else if (Input.GetMouseButtonUp(0) == true) //�� ����
        {
            // ���
            Debug.Log($"Angle: {mouseAngle},Time: {mouseTimeHold}, Distance:{mouseDistance}");
        }
        else //Input.GetMouseButton(0) == false // ������ �ʾ��� ��
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

#elif UNITY_ANDROID //=======================================================================================================================================================================================================
#endif
    }
}
