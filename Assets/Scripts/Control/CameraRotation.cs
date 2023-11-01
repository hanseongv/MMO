using Abstract;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotation : BaseDragManipulator
{
    internal override void Init(VisualElement t)
    {
        base.Init(t);
    }

    internal Vector2 dragVector;
    private Vector2 _targetStartPosition;
    private Vector2 currentPosition;

    protected override void PointerDownHandler(PointerDownEvent evt)
    {
        base.PointerDownHandler(evt);

        dragVector = Vector2.zero;
        currentPosition = evt.position;
    }

    protected override void PointerMoveHandler(PointerMoveEvent evt)
    {
        base.PointerMoveHandler(evt);

        var pointerDelta = currentPosition - (Vector2)evt.position;
        currentPosition = evt.position;

        dragVector = pointerDelta;
    }

    protected override void PointerUpHandler(PointerUpEvent evt)
    {
        base.PointerUpHandler(evt);
    }
}