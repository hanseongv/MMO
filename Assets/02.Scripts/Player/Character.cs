using System;
using System.Collections;
using System.Collections.Generic;
using _02.Scripts.Control;
using State;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    internal float moveSpeedValue = 0.0f;
    protected internal float CurrentSpeed { get; set; }

    private ICharacterState _idleState;
    protected ICharacterState _moveState;

    private StateMachine _stateMachine;
    internal Animator Animator;


    protected virtual void Start()
    {
        _stateMachine = new StateMachine(this);
        _idleState = gameObject.AddComponent<CharacterIdleState>();

        _stateMachine.Transition(_idleState);
    }

    protected virtual void Update()
    {
        Move();
    }

    void Move()
    {
        if (Mathf.Approximately(CurrentSpeed, 0.0f))
        {
            _stateMachine.Transition(_idleState);
        }
        else
        {
            _stateMachine.Transition(_moveState);
        }
    }
}