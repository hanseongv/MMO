using UnityEngine;
using UnityEngine.UIElements;

namespace Interface
{
    public struct MoveValue
    {
        public Vector3 Vector;
        public float Value;
    }

    public interface IMoveController
    {
        public MoveValue GetControllerValue();
        public void ControllerInit(VisualElement target);
    }
}