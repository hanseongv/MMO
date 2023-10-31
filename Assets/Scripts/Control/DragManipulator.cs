using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

namespace _02.Scripts.Control
{
//https://github.com/Unity-Technologies/ui-toolkit-manual-code-examples/tree/master/create-a-drag-and-drop-window-inside-a-custom-editor-window
    public class DragManipulator : PointerManipulator
    {
        public DragManipulator(VisualElement target)
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

        private float _boundRadius;
        private VisualElement _lever;
        private MoveValue _moveValue;

        private Vector2 _targetStartPosition;
        private Vector3 _pointerStartPosition;

        internal void Init(string imageLever)
        {
            _lever = target.Q<VisualElement>(imageLever);
            _boundRadius = target.localBound.width / 2.0f;
        }


        private void PointerDownHandler(PointerDownEvent evt)
        {
            var pos = -(_lever.worldBound.center - (Vector2)evt.position);
            _lever.transform.position = Vector2.ClampMagnitude(pos, _boundRadius);

            _targetStartPosition = pos;
            _pointerStartPosition = evt.position;
            target.CapturePointer(evt.pointerId);


            IsDrag = true;
        }


        private void PointerMoveHandler(PointerMoveEvent evt)
        {
            if (!IsDrag) return;

            var pointerDelta = evt.position - _pointerStartPosition;
            _lever.transform.position = Vector2.ClampMagnitude(
                new Vector2(_targetStartPosition.x + pointerDelta.x, _targetStartPosition.y + pointerDelta.y),
                _boundRadius);
        }


        private void PointerUpHandler(PointerUpEvent evt)
        {
            if (!IsDrag) return;
            IsDrag = false;
            _lever.transform.position = Vector2.zero;
            target.ReleasePointer(evt.pointerId);
        }

        internal MoveValue GetJoystickValue()
        {
            if (_lever == null) return new MoveValue();

            _moveValue.value = _lever.transform.position.magnitude / _boundRadius;
            var pos = _lever.transform.position.normalized;
            _moveValue.vector = new Vector3(pos.x, 0, -pos.y);
            return _moveValue;
        }
    }
}