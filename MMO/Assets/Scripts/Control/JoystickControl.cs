using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class JoystickControl : MonoBehaviour
{
    // UXML 비주얼 엘리먼트 이름
    [SerializeField] private const string ImageLever = "ImageLever";

    
    //델리게이트 확인해보고 코드 정리해서 깃허브 푸시
    delegate void Init();
    Init init;

    private void Start()
    {
        // UI 도큐먼트에 있는 최상위 비주얼 엘리먼트를 참조.
        VisualElement root = GetComponent<UIDocument>().rootVisualElement;
        
        // 값을 불러온 뒤 콜백
        root.RegisterCallback<GeometryChangedEvent>(StartVisualElement);

        // 조이스틱에 드래그 기능 넣기
        DragManipulator manipulator =
            new(root.Q<VisualElement>(ImageLever));
        init += manipulator.Init;
    }

    private void StartVisualElement(GeometryChangedEvent evt)
    {
        init();
    }
}