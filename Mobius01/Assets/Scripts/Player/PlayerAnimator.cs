using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator aN;

    void Start()
    {
        aN = GetComponent<Animator>();

        aN.SetBool("up", false);
        aN.SetBool("down", true);
        aN.SetBool("left", false);
        aN.SetBool("right", false);
    }

    void Update()
    {
        if (GameManager.instance.systemOnline)
        {
            if (0.5f < Input.GetAxisRaw("Vertical"))
            {
                aN.SetBool("up", true);
                aN.SetBool("down", false);
            }
            else if (Input.GetAxisRaw("Vertical") < -0.5f)
            {
                aN.SetBool("up", false);
                aN.SetBool("down", true);
            }
            else
            {
                aN.SetBool("up", false);
                aN.SetBool("down", false);
            }

            if (Input.GetAxisRaw("Horizontal") < -0.5f)
            {
                aN.SetBool("left", true);
                aN.SetBool("right", false);
            }
            else if (0.5f < Input.GetAxisRaw("Horizontal"))
            {
                aN.SetBool("left", false);
                aN.SetBool("right", true);
            }
            else
            {
                aN.SetBool("left", false);
                aN.SetBool("right", false);
            }
        }
    }
}
