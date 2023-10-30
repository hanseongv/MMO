using System;
using System.Collections;
using System.Collections.Generic;
using _02.Scripts.State;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    protected override void Start()
    {
        MoveState = new MyPlayerMoveState(this);
        Animator = GetComponent<Animator>();
        base.Start();
    }

    protected override void Update()
    {
        if (0.0f < GameManager.Instance.controlManager.currentController.GetControllerValue().value)
        {
            ChangeState(State.Move);
        }
        else
        {
            ChangeState(State.Idle);
        }

        base.Update();


        // SetMoveSpeedValue();
    }

    // void SetMoveSpeedValue()
    // {
    //     moveSpeedValue = GameManager.Instance.controlManager.currentController.GetControllerValue().value;
    // }
}