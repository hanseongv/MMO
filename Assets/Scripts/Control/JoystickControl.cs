using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Control
{
    public class JoystickControl : PointerManipulator, IController
    {
        #region Init

        // set target을 할 때 레지스터가 등록. (Manipulator.cs 38줄 참고)
        private void RegisterCallbackOnTarget<T>(EventCallback<T> callback, bool unregister = false)
            where T : PointerEventBase<T>, new()
        {
            if (unregister)
                target.UnregisterCallback<T>(callback);
            else
                target.RegisterCallback<T>(callback);
        }

        protected override void RegisterCallbacksOnTarget()
        {
            RegisterCallbackOnTarget<PointerDownEvent>(PointerDownHandler);
            RegisterCallbackOnTarget<PointerMoveEvent>(PointerMoveHandler);
            RegisterCallbackOnTarget<PointerUpEvent>(PointerUpHandler);
        }

        protected override void UnregisterCallbacksFromTarget()
        {
            RegisterCallbackOnTarget<PointerDownEvent>(PointerDownHandler, true);
            RegisterCallbackOnTarget<PointerMoveEvent>(PointerMoveHandler, true);
            RegisterCallbackOnTarget<PointerUpEvent>(PointerUpHandler, true);
        }

        const string ImageJoystickBound = "ImageJoystickBound";
        const string ImageJoystickLever = "ImageJoystickLever";

        public void Init(VisualElement t)
        {
            target = t;
            joystickBound = target.Q<VisualElement>(ImageJoystickBound);
            joystickLever = target.Q<VisualElement>(ImageJoystickLever);
            // _boundRadius = joystickBound.localBound.width / 2.0f;
            _boundRadius = joystickBound.localBound.width;
        }

        #endregion

        #region Properties

        private bool _isDrag;
        private float _boundRadius;
        private VisualElement joystickLever;
        private VisualElement joystickBound;
        private Vector3 _pointerStartPosition;

        #endregion

        private void PointerDownHandler(PointerDownEvent evt)
        {
            joystickLever.transform.position = Vector2.zero;

            _isDrag = true;
            var pos = -(joystickBound.layout.center - (Vector2)evt.position);
            joystickBound.transform.position = pos;
            _pointerStartPosition = evt.position;
            target.CapturePointer(evt.pointerId);
        }

        private void PointerMoveHandler(PointerMoveEvent evt)
        {
            if (!_isDrag) return;

            if (joystickLever.ClassListContains("joystick--on") == false)
                joystickLever.AddToClassList("joystick--on");

            joystickLever.transform.position = Vector2.ClampMagnitude(evt.position - _pointerStartPosition,
                _boundRadius);
        }

        private void PointerUpHandler(PointerUpEvent evt)
        {
            if (!_isDrag) return;

            _isDrag = false;
            target.ReleasePointer(evt.pointerId);

            if (joystickLever.ClassListContains("joystick--on"))
                joystickLever.RemoveFromClassList("joystick--on");

            // joystickLever.transform.position = Vector2.zero;
        }

        public MoveValue GetControllerValue()
        {
            if (joystickLever == null || !_isDrag) return new MoveValue();
            var pos = joystickLever.transform.position.normalized;

            return new MoveValue()
            {
                value = joystickLever.transform.position.magnitude / _boundRadius,
                vector = new Vector3(pos.x, 0, -pos.y)
            };
        }
    }
}