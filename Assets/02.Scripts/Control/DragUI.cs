using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragUI : PointerManipulator
{
    public DragUI(VisualElement target)
    {
        this.target = target;
    }

    internal Vector2 dragVector;



    private Vector2 _targetStartPosition;
    private Vector3 _pointerStartPosition;
    private Vector2 currentPosition;
    internal bool _isDrag;

    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
        target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
        target.RegisterCallback<PointerUpEvent>(PointerUpHandler);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
        target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
        target.UnregisterCallback<PointerUpEvent>(PointerUpHandler);
    }

    private void PointerDownHandler(PointerDownEvent evt)
    {
        dragVector = Vector2.zero;
        _pointerStartPosition = evt.position;
        currentPosition = evt.position;
        target.CapturePointer(evt.pointerId);
        _isDrag = true;
    }


    private void PointerMoveHandler(PointerMoveEvent evt)
    {
        if (!_isDrag) return;

        var pointerDelta = currentPosition - (Vector2)evt.position;
        currentPosition = evt.position;

        dragVector = pointerDelta;
    }


    private void PointerUpHandler(PointerUpEvent evt)
    {
        if (!_isDrag) return;

        _isDrag = false;
        // _lever.transform.position = Vector2.zero;
        target.ReleasePointer(evt.pointerId);
    }
}