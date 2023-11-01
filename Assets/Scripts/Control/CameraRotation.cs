using Abstract;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraRotation : BaseDragManipulator
{
    private Vector2 _targetStartPosition;

    internal Vector2 DragVector;
    private Vector2 currentPosition;

    protected override void PointerDownHandler(PointerDownEvent evt)
    {
        base.PointerDownHandler(evt);

        DragVector = Vector2.zero;
        currentPosition = evt.position;
    }

    protected override void PointerMoveHandler(PointerMoveEvent evt)
    {
        if (!IsDrag) return;

        var pointerDelta = currentPosition - (Vector2)evt.position;
        currentPosition = evt.position;
        DragVector = pointerDelta;
    }

    protected override void PointerUpHandler(PointerUpEvent evt)
    {
        if (!IsDrag) return;

        base.PointerUpHandler(evt);
    }
}