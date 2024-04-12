using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2 : MonoBehaviour
{
    Transform transform;
    Rigidbody2D rigidbody;

    private float speed; // 속도
    [SerializeField] float thrust = 10f; // 추력, 항력
    private float throttle = 1f; // 스로틀
    [SerializeField] float drag = 10f; // 항력
    [SerializeField] float cruiseSpeed = 2f; // 순항속도, 이 속도에서 추력과 항력이 균형을 이룸

    private float turnSpeed; // 회전속도
    [SerializeField] float turnRate = 10f; // 선회율
    [SerializeField] float cruiseTurnRate = 120f; // 선회율
    [SerializeField] float restore = 10f; // 복원력
    private float turnInput; // 선회입력


    private void Awake()
    {
        transform = GetComponent<Transform>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        speed += ((thrust * throttle) - (drag * (speed/ cruiseSpeed))) * Time.fixedDeltaTime;
        transform.position += transform.up * speed * Time.fixedDeltaTime;
        //Vector2.Angle

        turnInput = -Input.GetAxis("Horizontal");
        if (turnSpeed < 0)
        {
            Debug.Log(turnSpeed);
            turnSpeed += ((turnInput * turnRate) - (restore * turnSpeed / cruiseTurnRate)) * Time.fixedDeltaTime;
        }
        else if (0 < turnSpeed)
        {
            Debug.Log(turnSpeed);
            turnSpeed += ((turnInput * turnRate) - (restore * turnSpeed / cruiseTurnRate)) * Time.fixedDeltaTime;
        }
        else
        {
            turnSpeed += (turnInput * turnRate) * Time.fixedDeltaTime;
        }

        transform.Rotate(new Vector3(0, 0, turnSpeed));
    }
}
