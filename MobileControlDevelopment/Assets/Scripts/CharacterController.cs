using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    private void Awake()
    {

    }

    void Start()
    {
        MoveDirectionStart();
    }

    void Update()
    {
        MoveDirectionUpdate();
    }


    public float speed;
    public Vector3 moveDirection;
    public int moveDirectionX;
    public int moveDirectionY;

    // GetComponent =========================================================================================================
    public Animator animator;

    // MoveDirection ========================================================================================================
    protected virtual void MoveDirectionStart()
    {
        animator.SetFloat("direction", 1);
        animator.SetFloat("inputDirection", 1);
    }


    protected virtual void MoveDirectionUpdate()
    {
        animator.SetFloat("inputX", (int)moveDirection.x);
        animator.SetFloat("inputY", (int)moveDirection.y);

        if (moveDirection.x != 0)
        {
            animator.SetFloat("inputDirection", (int)moveDirection.x);
        }
    }

    // States ===============================================================================================================
    public enum CharacterState
    {
        idle,
        move,
        jump,
        fall,
        dodge,
        attack,
        jumpAttack,
        chargeAttack,
        hit,
        down,
        //stun, etc.
    }

    // Idle =================================================================================================================
    public void Idle()
    {
        animator.SetInteger("state", (int)CharacterState.idle);
    }

    // move =================================================================================================================
    protected void Move()
    {
        animator.SetInteger("state", (int)CharacterState.move);
    }

    // jump =================================================================================================================
    protected void Jump()
    {

    }

    // fall =================================================================================================================

    // dodge ================================================================================================================
    protected void Dodge()
    {
        animator.SetInteger("state", (int)CharacterState.dodge);
    }

    // attack ===============================================================================================================
    protected void Attack()
    {
        animator.SetInteger("state", (int)CharacterState.attack);
    }
}