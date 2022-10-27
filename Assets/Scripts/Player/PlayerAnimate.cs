using System;
using Mirror;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimate : NetworkBehaviour
{
    private Animator _animator;

    private void Awake()
    {
            _animator = GetComponent<Animator>();
            PlayerMove.OnPlayerMoved += RunningState;
            PlayerMove.OnPlayerSprinted += SprintState;
    }

    private void SetBoolState(string animName, bool state)
    {
            _animator.SetBool(animName, state);
    }

    private void SetTrigger(string triggerName)
    {
            _animator.SetTrigger(triggerName);
    }

    private void RunningState(Vector3 moveVector)
    {
            if (!isLocalPlayer)
            { 
                    return;
            }
            if (moveVector != Vector3.zero)
            {
                SetBoolState("IsRunning", true);
            }
            else
            {
                SetBoolState("IsRunning", false); 
            }
    }

    private void SprintState()
    { 
            if (!isLocalPlayer)
            { 
                    return;
            }
            SetTrigger("IsPushing");
    }

    private void OnDestroy()
    {
            PlayerMove.OnPlayerMoved -= RunningState; 
            PlayerMove.OnPlayerSprinted -= SprintState;
    }
}
