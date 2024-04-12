using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Transform transform;
    Animator animator;

    // 현재 속도
    private float speed;
    // 현재 각도
    private float direction;
    // 현재 회전속도
    private float turnSpeed = 0;

    // 추력, 항력
    [SerializeField] float thrust = 10f;
    // 스로틀
    private float throttle = 1f;
    // 순항속도
    [SerializeField] float cruiseSpeed = 2f;

    // 선회율
    [SerializeField] float turnRate = 120f;

    // X, Y축 입력
    private float horizontalInput;
    private float verticalInput;

    private void Awake()
    {
        transform = GetComponent<Transform>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        
    }

    private void FixedUpdate()
    {
        // 스로틀 계산
        throttle = 1 + verticalInput;

        // 속도 계산
        speed += (thrust * throttle - (speed / cruiseSpeed)) * Time.fixedDeltaTime;

        // 선회 속도 계산
        turnSpeed = turnRate * horizontalInput;

        // 방향 계산
        direction += turnSpeed * Time.fixedDeltaTime;

        // 방향 적용
        transform.rotation = Quaternion.Euler(0, 0, direction);

        // 이동
        transform.Translate(Vector2.up * speed * Time.fixedDeltaTime);
    }

    private void Update()
    {
        horizontalInput = -Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        animator.SetFloat("HorizontalInput", (1 + horizontalInput) * 0.5f);
    }
}
