using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetComponents(animator, stateInfo);
        ResetEnter();

        MoveStart();
        AdvanceEnter();
        JumpEnter();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MoveUpdate();
        AdvanceUpdate();
        JumpUpdate();
        GroundUpdate();

        PhysicsUpdate();
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetExit();
    }

    [Header("Get Components")] //======================================================================================================================================================
    protected Animator _animator;
    protected CharacterController _characterController;
    protected Transform _transform;
    protected float _stateLength;

    private void GetComponents(Animator animator, AnimatorStateInfo stateInfo)
    {
        _animator = animator;
        _characterController = animator.GetComponentInParent<CharacterController>();
        _transform = _characterController.transform;
        _stateLength = stateInfo.length;

        
    }

    [Header("Reset Timing")] //========================================================================================================================================================
    [SerializeField] bool _resetStart;
    [SerializeField] bool _resetEnd;
    [SerializeField] bool _resetDelayed;
    [SerializeField] float _stateResetTime;
    private void ResetEnter()
    {
        if (_resetStart)
            _characterController.Idle();

        if (_resetDelayed)
            _characterController.Invoke("Idle", _stateResetTime * _stateLength);
    }
    private void ResetExit()
    {
        if (_resetEnd)
            _characterController.Idle();
    }

    [Header("Move")] //================================================================================================================================================================
    [SerializeField] bool _isMove;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _moveAccel;
    [SerializeField] float _moveDeccel;
    [SerializeField] float _currentSpeed;
    private void MoveStart()
    {
        if (_isMove == false)
        {
            _animator.SetFloat("currentSpeedX", 0);
        }
    }

    private void MoveUpdate()
    {
        if (_isMove)
        {
            if (_animator.GetFloat("inputX") != 0)
            {
                if (_animator.GetFloat("inputX") == _animator.GetFloat("direction"))
                {
                    _animator.SetFloat("currentSpeedX", Mathf.Clamp(_animator.GetFloat("currentSpeedX") + _moveAccel * Time.fixedDeltaTime, 0, _moveSpeed));
                }
                else //(_animator.GetFloat("inputX") == _animator.GetFloat("direction"))
                {
                    _animator.SetFloat("currentSpeedX", Mathf.Clamp(_animator.GetFloat("currentSpeedX") - _moveDeccel * Time.fixedDeltaTime, 0, _moveSpeed));
                }
            }
            else // (_animator.GetFloat("inputX") == 0)
            {
                _animator.SetFloat("currentSpeedX", Mathf.Clamp(_animator.GetFloat("currentSpeedX") - _moveDeccel * Time.fixedDeltaTime, 0, _moveSpeed));
            }

            if (_animator.GetFloat("currentSpeedX") == 0 && _animator.GetFloat("inputX") != 0)
            {
                _animator.SetFloat("direction", _animator.GetFloat("inputX"));
            }
        }
    }

    [Header("Advance")] //=============================================================================================================================================================
    [SerializeField] bool _isAdvance;
    [SerializeField] bool _useInputDirection;
    [SerializeField] float _advanceSpeed;
    [SerializeField] float _advanceSpeedReduce;
    [SerializeField] float _inputDirection;
    private float _advanceSpeedLeft;

    private void AdvanceEnter()
    {
        if (_isAdvance)
        {
            _inputDirection = _animator.GetFloat("inputDirection");
            _animator.SetFloat("currentSpeedX", 0);
            _advanceSpeedLeft = _advanceSpeed * Mathf.Abs(_inputDirection);
        }
    }
    private void AdvanceUpdate()
    {
        if (_isAdvance)
        {
            _animator.SetFloat("currentSpeedX", _advanceSpeedLeft -= _advanceSpeedReduce * Mathf.Abs(_inputDirection) * Time.fixedDeltaTime / _stateLength);
        }
    }

    [Header("Jump")] //=============================================================================================================================================================
    [SerializeField] bool _isJump;
    [SerializeField] float _jumpSpeed;
    
    private void JumpEnter()
    {
        if(_isJump)
        {
            _animator.SetBool("isGround", false);
            _animator.SetFloat("currentSpeedY", _jumpSpeed * _gravity);
        }
    }

    private void JumpUpdate() 
    {
        //_transform.position += new Vector3(0, 1, 0) * _animator.GetFloat("currentSpeedY") * Time.fixedDeltaTime;
    }

    [Header("Ground")] //====================================================================================================
    private bool _isGround;
    [SerializeField] float _gravity = 10;
    [SerializeField] float _fallSpeedMax;

    private void GroundUpdate()
    {
        // 바닥이랑 양옆 감지
        // 바닥 감지할경우 _isGround = true, 못할경우 _isGround = false,
        // 양 옆 감지할경우 매달리기

        if (_animator.GetBool("isGround"))
        {
            _animator.SetFloat("currentSpeedY", 0);
        }
        else //(_animator.GetBool("isGround") == false)
        {
            _animator.SetFloat("currentSpeedY", _animator.GetFloat("currentSpeedY") - _gravity * Time.fixedDeltaTime);
            _transform.position += new Vector3(0, 1, 0) * _animator.GetFloat("currentSpeedY") * Time.fixedDeltaTime;
        }
    }

    // Physics ==============================================================================================================
    private void PhysicsUpdate()
    {
        _transform.position += new Vector3(1, 0, 0) * _animator.GetFloat("currentSpeedX") * _animator.GetFloat("direction") * _characterController.speed * Time.fixedDeltaTime;
        _characterController.transform.position += new Vector3(_animator.GetFloat("inputDirection"), 0, 0) * _characterController.speed * _advanceSpeedLeft * Time.fixedDeltaTime;

    }
    // 속도 관련 업데이트 묶기
}
