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
    public CapsuleCollider2D collider;

    // GroundCheck ==========================================================================================================
    public float heightOffset;
    [SerializeField] float laycastDistance;
    [SerializeField] LayerMask floorLayerMask;
    [SerializeField] LayerMask wallLayerMask;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(Physics2D.Raycast((Vector2)transform.position + new Vector2(0, collider.size.y), Vector2.left, laycastDistance + collider.size.x * 0.5f, wallLayerMask)
        || Physics2D.Raycast((Vector2)transform.position + new Vector2(0, collider.size.y), Vector2.right, laycastDistance + collider.size.x * 0.5f, wallLayerMask))
        {
            animator.SetBool("isWall", true);
        }
        else if (Physics2D.Raycast((Vector2)transform.position + new Vector2(0, collider.size.y * 0.5f), Vector2.down, laycastDistance + collider.size.y * 0.5f, floorLayerMask))
        {
            animator.SetBool("isGround", true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        animator.SetBool("isGround", false);
    }

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
    }

    protected virtual void SetInputDirection()
    {
        animator.SetFloat("inputDirection", (int)moveDirection.x);
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
        animator.SetInteger("state", (int)CharacterState.jump);
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