using System;
using System.Collections;
using System.Collections.Generic;
using _02.Scripts.State;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    internal override void Init()
    {
        MoveState = new MyPlayerMoveState(this);
        Animator = GetComponent<Animator>();
        base.Init();
    }
    // protected override void Start()
    // {
    //     MoveState = new MyPlayerMoveState(this);
    //     Animator = GetComponent<Animator>();
    //     base.Start();
    // }

    protected override void Update()
    {
        base.Update();

        var controllerValue = GameManager.Instance.controlManager.CurrentController.GetControllerValue().value;
        if (0.0f < controllerValue)
        {
            ChangeState(State.Move);
        }
        else
        {
            ChangeState(State.Idle);
        }
    }
}