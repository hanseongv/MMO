using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class DragAbstract : PointerManipulator
{
    public DragAbstract(VisualElement target)
    {
        this.target = target;
    }

    internal bool IsDrag;

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

    internal Vector2 DragVector;
    private Vector2 currentPosition;


     protected virtual void PointerDownHandler(PointerDownEvent evt)
    {
   
        target.CapturePointer(evt.pointerId);
        IsDrag = true;
    }


    protected virtual void PointerMoveHandler(PointerMoveEvent evt)
    {
        if (!IsDrag) return;

    }


   protected virtual void PointerUpHandler(PointerUpEvent evt)
    {
        if (!IsDrag) return;

        IsDrag = false;
        target.ReleasePointer(evt.pointerId);
    }


}