using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : PlayerController
{
    [SerializeField] private GameObject camera;

    protected override void Start()
    {
        cameraTransform = Camera.main.transform;
        _animator = GetComponent<Animator>();
        base.Start();
    }

    private void FixedUpdate()
    {
    }

    protected override void Update()
    {
        camera.transform.position =
            Vector3.Lerp(camera.transform.position, transform.position, Time.deltaTime * 10.0f);
        base.Update();
    }
}