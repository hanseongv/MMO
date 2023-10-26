using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

//https://github.com/Unity-Technologies/ui-toolkit-manual-code-examples/tree/master/create-a-drag-and-drop-window-inside-a-custom-editor-window
public class DragManipulator : PointerManipulator
{
    float boundRadius;

    public DragManipulator(VisualElement target)
    {
        this.target = target;
        root = target.parent;
    }

    internal void Init()
    {
        boundRadius = root.localBound.width / 2.0f;
    }

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

    private Vector2 targetStartPosition { get; set; }
    private Vector3 pointerStartPosition { get; set; }
    internal bool isDrag { get; set; }
    private float leverPower { get; set; }

    private VisualElement root { get; }

    private void PointerDownHandler(PointerDownEvent evt)
    {
        Debug.Log(root);
        Debug.Log(root.localBound.width);
        Debug.Log(boundRadius);
        var pos = -(target.worldBound.center - (Vector2)evt.position);
        target.transform.position = (Vector2)Vector2.ClampMagnitude(pos, boundRadius);

        targetStartPosition = pos;
        pointerStartPosition = evt.position;
        target.CapturePointer(evt.pointerId);

        isDrag = true;
    }


    private void PointerMoveHandler(PointerMoveEvent evt)
    {
        if (isDrag)
        {
            Vector3 pointerDelta = evt.position - pointerStartPosition;

            target.transform.position = Vector2.ClampMagnitude(
                new Vector2(targetStartPosition.x + pointerDelta.x, targetStartPosition.y + pointerDelta.y),
                boundRadius);

            leverPower = target.transform.position.magnitude / boundRadius;
        }
    }

    internal void PointerUpHandler(PointerUpEvent evt)
    {
        if (isDrag)
        {
            isDrag = false;
            target.transform.position = Vector2.zero;
            target.ReleasePointer(evt.pointerId);
        }
    }

}