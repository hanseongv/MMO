using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform layer;
     private RectTransform rectTransform;
     

     private void Awake()
     {
         rectTransform = GetComponent<RectTransform>();
     }

     public void OnBeginDrag(PointerEventData eventData)
     {
         // var inputDir = eventData.position - rectTransform.anchoredPosition;
         var inputDir = eventData.position;
         layer.anchoredPosition = inputDir;
    }

    public void OnDrag(PointerEventData eventData)
    {
        print("Drag");

    }

    public void OnEndDrag(PointerEventData eventData)
    {
        print("EndDrag");

    }
}
