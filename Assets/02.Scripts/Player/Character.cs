using System;
using System.Collections;
using System.Collections.Generic;
using _02.Scripts.Control;
using _02.Scripts.State;
using State;
using UnityEngine;
using MyPlayerMoveState = _02.Scripts.State.MyPlayerMoveState;

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

    // private ICharacterState _idleState;
    // protected ICharacterState _moveState;

    // private StateMachine _stateMachine;
    internal Animator Animator;

    protected void ChangeState(State state)
    {
        // if (currentState == state)
        //     return;

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
        // _stateMachine = new StateMachine(this);
        //
        // _idleState = gameObject.AddComponent<CharacterIdleState>();
        //
        // _stateMachine.Transition(_idleState);
    }

    protected virtual void Update()
    {
        _stateMachine.UpdateState();
    }
    //
    // void Move()
    // {
    //
    // }
}