
using UnityEngine;
using UnityEngine.UIElements;

public struct MoveValue
{
    public Vector3 vector;
    public float value;
}

public interface IMoveController
{
    public MoveValue GetControllerValue();
    public void ControllerInit(VisualElement target);
}