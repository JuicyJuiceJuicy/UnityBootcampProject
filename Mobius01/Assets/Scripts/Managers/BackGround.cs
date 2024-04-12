using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    public float controlSpeed = 0.1f;
    Material mT;
    Vector2 scroll;
    Vector2 control;

    void Start()
    {
        mT = GetComponent<Renderer>().material;
        scroll = new Vector2( 0, scrollSpeed );
    }

    void Update()
    {
        if (GameManager.instance.systemOnline)
        {
            control = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * controlSpeed;
        mT.mainTextureOffset += (scroll + control) * Time.deltaTime;
        }
    }
}
