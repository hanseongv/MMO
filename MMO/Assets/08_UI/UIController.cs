using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    //바텀 시트 부모
    private VisualElement _bottomContainer;

    //열기 버튼
    private Button _openButton;

    //닫기 버튼
    private Button _closeButton;

    //바텀시트
    private VisualElement _bottomSheet;

    //가림막
    private VisualElement _scrim;

    private void Start()
    {
        // UI 도큐먼트에 있는 최상위 비주얼 엘리먼트를 참조.
        var root = GetComponent<UIDocument>().rootVisualElement;
        //바텀 시트의 부모
        _bottomContainer = root.Q<VisualElement>("Container_Bottom");
        _openButton = root.Q<Button>("Button_Open");
        _closeButton = root.Q<Button>("Button_Close");
        //바텀 시트와 가림막
        _bottomSheet = root.Q<VisualElement>("BottomSheet");
        _scrim = root.Q<VisualElement>("Scrim");
        _bottomContainer.style.display = DisplayStyle.None;

        _openButton.RegisterCallback<ClickEvent>(OnOpenButtonClick);
        _closeButton.RegisterCallback<ClickEvent>(OnCloseButtonClick);

        //바텀 시트가 내려오면 그룹을 끈다.
        _bottomSheet.RegisterCallback<TransitionEndEvent>(OnBottomSheetDown);
    }


    private void OnOpenButtonClick(ClickEvent evt)
    {
        _bottomContainer.style.display = DisplayStyle.Flex;
        _bottomSheet.AddToClassList("bottomsheet--up");
        _scrim.AddToClassList("scrim--fadein");
    }

    void OnCloseButtonClick(ClickEvent evt)
    {
        _bottomSheet.RemoveFromClassList("bottomsheet--up");
        _scrim.RemoveFromClassList("scrim--fadein");
    }

    private void OnBottomSheetDown(TransitionEndEvent evt)
    {
        if (_bottomSheet.ClassListContains("bottomsheet--up"))
            return;
        
        _bottomContainer.style.display = DisplayStyle.None;
    }
}