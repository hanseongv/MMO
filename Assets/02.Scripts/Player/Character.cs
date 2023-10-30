using System;
using System.Collections;
using System.Collections.Generic;
using _02.Scripts.Control;
using State;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed = 1.0f;
    public float CurrentSpeed { get; set; }
    private ICharacterState _idleState;
    internal ICharacterState _walkState;
    private ICharacterState _runState;

    private CharacterStateContext _characterStateContext;
    internal Animator _animator;


    protected virtual void Start()
    {
        _characterStateContext = new CharacterStateContext(this);
        _idleState = gameObject.AddComponent<CharacterIdleState>();

        _characterStateContext.Transition(_idleState);
    }

    protected virtual void Update()
    {
        Move();
    }

    void Move()
    {
        var controllerValue = GameManager.Instance.controlManager.currentController.GetControllerValue();
        if (Mathf.Approximately(controllerValue.value, 0.0f))
        {
            _characterStateContext.Transition(_idleState);
        }
        else
        {
            _characterStateContext.Transition(_walkState);
        }
    }
}