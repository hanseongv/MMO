using System;
using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 1.0f;
    public float runSpeed = 1.5f;
    public float CurrentSpeed { get; set; }
    private IPlayerState _idleState, _walkState, _runState;
    private PlayerStateContext _playerStateContext;

    protected virtual void Start()
    {
        _playerStateContext = new PlayerStateContext(this);
        _idleState = gameObject.AddComponent<PlayerIdleState>();
        _walkState = gameObject.AddComponent<PlayerWalkState>();
        // _runState = gameObject.AddComponent<PlayerRunState>();
        _playerStateContext.Transition(_idleState);
    }

    protected virtual void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        
    }
}