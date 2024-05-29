using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetComponents(animator, stateInfo);
        ResetEnter();
        AdvanceEnter();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MoveUpdate();
        AdvanceUpdate();
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
    [SerializeField] bool _canMove;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _moveAccel;
    [SerializeField] float _moveDeccel;
    [SerializeField] float _currentSpeed;

    private void MoveUpdate()
    {
        if (_canMove)
        {
            if (_animator.GetFloat("inputX") != 0)
            {
                if (_animator.GetFloat("inputX") == _animator.GetFloat("direction"))
                {
                    _animator.SetFloat("currentSpeed", Mathf.Clamp(_animator.GetFloat("currentSpeed") + _moveAccel * Time.fixedDeltaTime, 0, _moveSpeed));
                }
                else //(_animator.GetFloat("inputX") == _animator.GetFloat("direction"))
                {
                    _animator.SetFloat("currentSpeed", Mathf.Clamp(_animator.GetFloat("currentSpeed") - _moveDeccel * Time.fixedDeltaTime, 0, _moveSpeed));
                }
            }
            else // (_animator.GetFloat("inputX") == 0)
            {
                _animator.SetFloat("currentSpeed", Mathf.Clamp(_animator.GetFloat("currentSpeed") - _moveDeccel * Time.fixedDeltaTime, 0, _moveSpeed));
            }

            if (_animator.GetFloat("currentSpeed") == 0 && _animator.GetFloat("inputX") != 0)
            {
                _animator.SetFloat("direction", _animator.GetFloat("inputX"));
                
            }

            _transform.position += new Vector3 (1, 0, 0) * _animator.GetFloat("currentSpeed") * _animator.GetFloat("direction") * _characterController.speed * Time.fixedDeltaTime;
        }
    }

    [Header("Advance")] //=============================================================================================================================================================
    [SerializeField] bool _isAdvance;
    private float _advanceDirection;
    [SerializeField] float _advanceSpeed;
    [SerializeField] float _advanceSpeedReduce;
    private float _advanceSpeedLeft;

    private void AdvanceEnter()
    {
        _animator.SetFloat("currentSpeed", 0);
        if (_isAdvance)
        {
            _advanceDirection = _animator.GetFloat("inputX");
            _advanceSpeedLeft = _advanceSpeed;
        }
    }
    private void AdvanceUpdate()
    {
        if (_isAdvance)
        {
            _characterController.transform.position += new Vector3(_animator.GetFloat("inputDirection"), 0, 0) * _characterController.speed * _advanceSpeedLeft * Time.fixedDeltaTime;
            _advanceSpeedLeft -= _advanceSpeedReduce * Time.fixedDeltaTime / _stateLength;
        }
    }
}
