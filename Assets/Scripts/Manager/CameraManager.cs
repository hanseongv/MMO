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
    private CameraRotation cameraRotation;
    [SerializeField] private float rotateSpeed = 10.0f;


    [SerializeField] private float followSmooth = 10.0f;

    private GameObject mainCamera;
    private GameObject _currentCameraObject;
    internal Camera CurrentCamera;
    private bool _isFollowing;

    private bool drag;
    private float xRotate, yRotate;

    private void Update()
    {
        FollowCamera();


        if (cameraRotation.IsDrag)
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
        cameraRotation =
            new(root.Q<VisualElement>(ContainerRotation));
    }

    private void RegisterCallback(GeometryChangedEvent evt)
    {
    }


    void ChangeCamera(GameObject useCamera)
    {
        _currentCameraObject = useCamera;
        CurrentCamera = _currentCameraObject.GetComponentInChildren<Camera>();
    }

    private void FollowCamera()
    {
        if (_currentCameraObject == null || !_isFollowing) return;

        var player = GameManager.Instance.player;

        if (player == null) return;

        var playerPosition = player.transform.position;
        var currentPosition = _currentCameraObject.transform.position;
        var newPosition = new Vector3(playerPosition.x, currentPosition.y,
            playerPosition.z);

        _currentCameraObject.transform.position = Vector3.Lerp(currentPosition, newPosition,
            Time.deltaTime * followSmooth);
    }


    private void RotationCamera()
    {
        if (cameraRotation.DragVector == Vector2.zero)
            return;

        yRotate += -cameraRotation.DragVector.x * Time.deltaTime * rotateSpeed;
        xRotate += cameraRotation.DragVector.y * Time.deltaTime * rotateSpeed;

        xRotate = Mathf.Clamp(xRotate, -70, 35);

        _currentCameraObject.transform.eulerAngles = new Vector3(-xRotate, yRotate, 0);

        cameraRotation.DragVector = Vector2.zero;
    }

    internal Vector3 GetCameraDirection()
    {
        return _currentCameraObject.transform.TransformDirection(
            GameManager.Instance.controlManager.CurrentController.GetControllerValue().vector);
    }
}