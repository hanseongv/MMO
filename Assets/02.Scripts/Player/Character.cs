using System;
using System.Collections;
using System.Collections.Generic;
using _02.Scripts.Control;
using _02.Scripts.State;
using State;
using UnityEngine;

public class Character : MonoBehaviour
{
    protected enum State
    {
        Idle,
        Move,
    }

    private State currentState;
    private StateMachine _stateMachine;
    [SerializeField]
    private BaseState IdleState;
    [SerializeField]
    protected BaseState MoveState;

    public float moveSpeed = 1.0f;
    internal float moveSpeedValue = 1.0f;
    protected internal float CurrentSpeed { get; set; }

    internal Animator Animator;

    protected void ChangeState(State state)
    {
        if (currentState == state)
            return;

        currentState = state;
        switch (currentState)
        {
            case State.Idle:
                _stateMachine.ChangeState(IdleState);
                break;

            case State.Move:
                _stateMachine.ChangeState(MoveState);
                break;
        }
    }

    protected virtual void Start()
    {
        IdleState = new IdleState(this);
        currentState = State.Idle;
        _stateMachine = new StateMachine(IdleState);
    }

    protected virtual void Update()
    {
        _stateMachine.UpdateState();
    }
}