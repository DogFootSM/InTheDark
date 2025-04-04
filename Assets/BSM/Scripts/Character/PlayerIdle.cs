using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class PlayerIdle : PlayerState
{
    private Coroutine _recoverHealthCo;
    private Coroutine _recoverHealthWaitCo;
    
    public PlayerIdle(PlayerController controller) : base(controller) {}

    public override void Enter()
    { 
        RecoverStamina(); 
        _controller.BehaviourAnimation(_idleAniHash, true);
        
        //TODO: 체력 회복 임시 조건
        if (_controller.PlayerStats.CurHP <= 40f)
        { 
            _recoverHealthWaitCo = _controller.StartCoroutine(RecoverHealthWaitRoutine());
        }
        
    }

    public override void OnTrigger()
    {
        _controller.ChangeState(PState.HURT);
    }
    
    public override void Update()
    {
        if (_controller.PlayerStats.Stamina >= 100f && _staminaRecoverCo != null)
        {
            _controller.StopCoroutine(_staminaRecoverCo);
            _staminaRecoverCo = null;
        }
        
        if (_controller.MoveDir != Vector3.zero && !Input.GetKey(KeyCode.LeftShift) || _controller.MoveDir.z < 0 && Input.GetKey(KeyCode.LeftShift))
        {
            _controller.ChangeState(PState.WALK);
        }
        else if (_controller.MoveDir.z > 0  && Input.GetKey(KeyCode.LeftShift))
        {
            _controller.ChangeState(PState.RUN);
        }
        else if (_controller.CanJump && Input.GetKeyDown(KeyCode.Space))
        {
            _controller.ChangeState(PState.JUMP);
        }
        else if (Input.GetMouseButtonDown(0) && _controller.CurCarryItem != null)
        {
            _controller.ChangeState(PState.ATTACK);
        } 
    }

    public override void Exit()
    { 
        _controller.BehaviourAnimation(_idleAniHash, false);
        
        if (_recoverHealthWaitCo != null)
        {
            _controller.StopCoroutine(_recoverHealthWaitCo);
            _recoverHealthWaitCo = null;
        }

        if (_recoverHealthCo != null)
        {
            _controller.StopCoroutine(_recoverHealthCo);
            _recoverHealthCo = null;
        } 
    }
    
    /// <summary>
    /// 체력 회복 전 대기 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator RecoverHealthWaitRoutine()
    {
        //TODO: 체력 회복 임시 조건
        yield return new WaitForSeconds(3f);
        _recoverHealthCo = _controller.StartCoroutine(RecoverHealthRoutine()); 
    }
    
}
