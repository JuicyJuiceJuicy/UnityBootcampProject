using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMBehaviour : StateMachineBehaviour
{
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        GetComponents(animator, stateInfo);
        AdvanceEnter();
    }
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        MoveUpdate();
        AdvanceUpdate();
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    [Header("Get Components")] //======================================================================================================================================================
    protected CharacterController _characterController;
    protected Transform _transform;
    protected float _stateLength;

    private void GetComponents(Animator animator, AnimatorStateInfo stateInfo)
    {
        _characterController = animator.GetComponentInParent<CharacterController>();
        _transform = _characterController.transform;
        _stateLength = stateInfo.length;

        
    }

    [Header("Move")] //================================================================================================================================================================
    [SerializeField] bool _canMove;
    [SerializeField] float _moveSpeed;

    private void MoveUpdate()
    {
        if (_canMove)
        {
            _transform.position += _characterController.moveDirection * _moveSpeed * _characterController.speed * Time.deltaTime;
        }
    }

    [Header("Advance")] //=============================================================================================================================================================
    [SerializeField] bool _isAdvance;
    [SerializeField] float _advanceSpeed;
    [SerializeField] float _advanceSpeedReduce;
    private float _advanceSpeedLeft;

    private void AdvanceEnter()
    {
        if (_isAdvance)
        {
            _advanceSpeedLeft = _advanceSpeed;
        }
    }
    private void AdvanceUpdate()
    {
        if (_isAdvance)
        {
            _characterController.transform.position += _advanceSpeedLeft * _characterController.transform.right * Time.deltaTime;
            _advanceSpeedLeft -= _advanceSpeedReduce * Time.deltaTime / _stateLength;
            Debug.Log(_advanceSpeedReduce * Time.deltaTime / _stateLength);
        }
    }
}
