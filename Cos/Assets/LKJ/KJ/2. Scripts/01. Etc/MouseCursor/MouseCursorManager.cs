using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorManager : MonoBehaviour
{
    [Header("MosueCursor")]
    // ����ī�޶��� ���콺 Ŀ�� �̹����� ���ϴ� �̹����� �ٲ�.
    [SerializeField] Texture2D cursorImage;
    [SerializeField] Texture2D cursorClickImage;


    void Start()
    {
        // ó�� ���콺 �̹��� ����.
        Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
    }

    
    void Update()
    {
        // ���콺�� ������ ��.
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorClickImage, Vector2.zero, CursorMode.Auto);
        }
        // ���콺�� ������ �ʾ��� ��.
        else if(!Input.GetMouseButton(0))
        {
            Cursor.SetCursor(cursorImage, Vector2.zero, CursorMode.Auto);
        }
    }
}
