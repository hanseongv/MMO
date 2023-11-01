
using UnityEngine;
using UnityEngine.UIElements;

public struct MoveValue
{
    public Vector3 vector;
    public float value;
}

public interface IController
{
    public MoveValue GetControllerValue();
    // public void Init(VisualElement target);
}