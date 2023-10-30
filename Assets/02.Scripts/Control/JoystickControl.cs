using System;
using System.Collections;
using System.Collections.Generic;
using _02.Scripts.Control;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class JoystickControl : MonoBehaviour, IController
{
    // UXML 비주얼 엘리먼트 이름
    [SerializeField] private const string ImageJoystick = "ImageJoystick";
    [SerializeField] private const string ImageLever = "ImageLever";

    private DragManipulator _manipulator;

    private void Start()
    {
        // UI 도큐먼트에 있는 최상위 비주얼 엘리먼트를 참조.
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        // 값을 불러온 뒤 콜백
        root.RegisterCallback<GeometryChangedEvent>(StartInit);

        // 조이스틱에 드래그 기능 넣기
        _manipulator =
            new(root.Q<VisualElement>(ImageJoystick));
    }

    private void StartInit(GeometryChangedEvent evt)
    {
        _manipulator.Init(ImageLever);
    }

    public MoveValue GetControllerValue()
    {
        return _manipulator.GetJoystickValue();
    }
}