using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    Rigidbody2D rB;
    SpriteRenderer sR;
    Animator aN;

    Vector2 accelV;
    float accel = 50f;

    float speed = 10f;

    Vector2 decelV;
    float decel = 30f;

    float jump = 15f;

    bool grounded = false;

    void Start()
    {
        rB = GetComponent<Rigidbody2D>();
        sR = GetComponent<SpriteRenderer>();
        aN = GetComponent<Animator>();
    }

    private void Update()
    {
        GetAccel();
        Jump();
        Move();
        Flip();
        Anim();

    }

    void Move()
    {
        if (0 < rB.velocity.x && rB.velocity.x < speed)
        {
            rB.velocity += (accelV - decelV) * Time.deltaTime;
        }
        else if (speed < rB.velocity.x)
        {
            rB.velocity = new(speed, rB.velocity.y);
        }
        else if (0 < -rB.velocity.x && -rB.velocity.x < speed)
        {
            rB.velocity += (accelV + decelV) * Time.deltaTime;
        }
        else if (speed < -rB.velocity.x)
        {
            rB.velocity = new(-speed, rB.velocity.y);
        }
        else
        {
            rB.velocity += (accelV) * Time.deltaTime;
        }
    }

    void GetAccel()
    {
        accelV = new(Input.GetAxis("Horizontal") * (accel + decel), 0);
        decelV = new(decel, 0);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump"))
        {
            rB.velocity += new Vector2(rB.velocity.x, jump);
        }
    }

    void Flip()
    {
        if (0 < Input.GetAxis("Horizontal"))
        {
            sR.flipX = false;
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            sR.flipX = true;
        }
    }

    void Anim()
    {
        if (Input.GetAxis("Horizontal") != 0)
        {
            aN.SetBool("run", true);
        }
        else if (Input.GetAxis("Horizontal") == 0)
        {
            aN.SetBool("run", false);
        }

        if (Input.GetButtonDown("Jump"))
        {
            aN.SetBool("jump", true);
        }

        //fall

        void OnCollisionEnter2D(Collision2D collision)
        {
            grounded = true;
        }
        void OnCollisionExit2D(Collision2D collision)
        {
            grounded = false;
        }
    }
}
