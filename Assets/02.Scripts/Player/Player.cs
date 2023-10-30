using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : Character
{
    protected override void Start()
    {
        _animator = GetComponent<Animator>();
        _walkState = gameObject.AddComponent<MyPlayerMoveState>();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}