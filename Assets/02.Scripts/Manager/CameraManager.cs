using System;
using System.Collections;
using System.Collections.Generic;
using _02.Scripts.Control;
using State.Interface;
using UnityEngine;
using UnityEngine.UIElements;

//todo 시네마틱으로 전환 시키는 방법 추가 
public class CameraManager : MonoBehaviour, IManager
{
    // UXML 비주얼 엘리먼트 이름
    [SerializeField] private const string ContainerRotation = "ContainerRotation";
    [SerializeField] private GameObject containerRotation;
    private DragUI dragUI;
    [SerializeField] private float rotateSpeed = 10.0f;


    [SerializeField] private float followSmooth = 10.0f;

    private GameObject mainCamera;
    private GameObject _currentCamera;
    private bool _isFollowing;

    private bool drag;
    private float xRotate, yRotate;

    private void Update()
    {
        FollowCamera();


        if (dragUI._isDrag)
        {
            RotationCamera();
        }
    }

    public void Init()
    {
        _isFollowing = true;
        mainCamera = GameObject.FindWithTag("MainCamera");
        ChangeCamera(mainCamera);


        VisualElement root = containerRotation.GetComponent<UIDocument>().rootVisualElement;
        root.RegisterCallback<GeometryChangedEvent>(RegisterCallback);
        dragUI =
            new(root.Q<VisualElement>(ContainerRotation));
    }

    private void RegisterCallback(GeometryChangedEvent evt)
    {
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

        var playerPosition = player.transform.position;
        var currentPosition = _currentCamera.transform.position;
        var newPosition = new Vector3(playerPosition.x, currentPosition.y,
            playerPosition.z);

        _currentCamera.transform.position = Vector3.Lerp(currentPosition, newPosition,
            Time.deltaTime * followSmooth);
    }


    private void RotationCamera()
    {
        if (dragUI.dragVector == Vector2.zero)
            return;

        yRotate += -dragUI.dragVector.x * Time.deltaTime * rotateSpeed;
        xRotate += dragUI.dragVector.y * Time.deltaTime * rotateSpeed;

        xRotate = Mathf.Clamp(xRotate, -70, 35);

        _currentCamera.transform.eulerAngles = new Vector3(-xRotate, yRotate, 0);
        
        dragUI.dragVector = Vector2.zero;
    }

    internal Vector3 GetCameraDirection()
    {
        return _currentCamera.transform.TransformDirection(
            GameManager.Instance.controlManager.currentController.GetControllerValue().vector);
    }
}