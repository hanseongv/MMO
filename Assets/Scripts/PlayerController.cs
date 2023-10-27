using System;
using System.Collections;
using System.Collections.Generic;
using Control;
using State;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float CurrentSpeed { get; set; }
    private IPlayerState _idleState, _walkState, _runState;

    private PlayerStateContext _playerStateContext;
    internal Animator _animator;

    //joystick 싱글턴으로 바꾸기
    [SerializeField] private JoystickControl _joystickControl;
    internal JoystickValues JoystickValues;
    private JoystickValues _curJoystickValues = new JoystickValues();

    internal Transform cameraTransform;
    protected virtual void Start()
    {
        _playerStateContext = new PlayerStateContext(this);
        _idleState = gameObject.AddComponent<PlayerIdleState>();
        _walkState = gameObject.AddComponent<PlayerWalkState>();
        JoystickValues = _joystickControl.GetJoystickValue();

        _playerStateContext.Transition(_idleState);
    }

    protected virtual void Update()
    {
        Move();
    }

    void Move()
    {
        JoystickValues = _joystickControl.GetJoystickValue();

        if (Mathf.Approximately(JoystickValues.LeverPower, 0.0f))
        {
            _playerStateContext.Transition(_idleState);
        }
        else
        {
            _playerStateContext.Transition(_walkState);
        }
  
    }
}