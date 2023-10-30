using System;
using System.Collections;
using System.Collections.Generic;
using State.Interface;
using UnityEngine;

//todo 시네마틱으로 전환 시키는 방법 추가 
public class CameraManager : MonoBehaviour, IManager
{
    [SerializeField] private float followSmooth = 10.0f;

    private GameObject mainCamera;
    private GameObject _currentCamera;
    private bool _isFollowing;

    private void Update()
    {
        FollowCamera();
    }

    public void Init()
    {
        _isFollowing = true;
        mainCamera = GameObject.FindWithTag("MainCamera");
        ChangeCamera(mainCamera);
    }

    void ChangeCamera(GameObject useCamera)
    {
        _currentCamera = useCamera;
    }

    private void FollowCamera()
    {
        if (_currentCamera == null || !_isFollowing) return;

        var player = GameManager.Instance.player;

        if (player == null) return;

        _currentCamera.transform.position =
            Vector3.Lerp(_currentCamera.transform.position, player.transform.position,
                Time.deltaTime * followSmooth);
    }


    internal Vector3 GetCameraDirection()
    {
        return _currentCamera.transform.TransformDirection(
            GameManager.Instance.controlManager.currentController.GetControllerValue().vector);
    }
}