using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    protected override void Start()
    {
        Animator = GetComponent<Animator>();
        _moveState = gameObject.AddComponent<MyPlayerMoveState>();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        SetMoveSpeedValue();
    }

    void SetMoveSpeedValue()
    {
        moveSpeedValue = GameManager.Instance.controlManager.currentController.GetControllerValue().value;
    }
}