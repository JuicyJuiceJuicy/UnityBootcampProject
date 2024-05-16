using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MouseInputSystem : MonoBehaviour
{
    private Camera camera;

    private Vector2 mousePosition;

    private Vector2 mousePositionDown;
    private Vector2 mousePositionUp;

    private Vector2 mouseSwipe;
    private float mouseSwipeAngle;

    void Start()
    {
        camera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    void Update()
    {
        mousePosition = Input.mousePosition;

        if (Input.GetMouseButton(0) == true)
        {
            Cursor.lockState = CursorLockMode.None;
            
            if (Input.GetMouseButtonDown(0) == true)
            {
                mousePositionDown = mousePosition;
            }
        }
        else if (Input.GetMouseButtonUp(0) == true)
        {
            mousePositionUp = mousePosition;
            mouseSwipe = mousePositionUp - mousePositionDown;

            float _mouseSwipeAngle = Mathf.Atan2(mouseSwipe.y, mouseSwipe.x) * Mathf.Rad2Deg;

            if (0 < _mouseSwipeAngle)
            {
                mouseSwipeAngle = _mouseSwipeAngle;
            }
            else //(_mouseSwipeAngle < 0)
            {
                mouseSwipeAngle = _mouseSwipeAngle + 360;
            }

            Debug.Log(mouseSwipeAngle);
        }
        else //Input.GetMouseButton(0) == false
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }
}
