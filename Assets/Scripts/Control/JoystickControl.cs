using System;
using System.Collections.Generic;
using Abstract;
using UnityEngine;
using UnityEngine.UIElements;

namespace Control
{
    public class JoystickControl : BaseDragManipulator, IController
    {
        const string ImageJoystickBound = "ImageJoystickBound";
        const string ImageJoystickLever = "ImageJoystickLever";

        public void ControllerInit(VisualElement t)
        {
            base.Init(t);

            joystickBound = target.Q<VisualElement>(ImageJoystickBound);
            joystickLever = target.Q<VisualElement>(ImageJoystickLever);
            // _boundRadius = joystickBound.localBound.width / 2.0f;
            _boundRadius = joystickBound.localBound.width;
        }


        #region Properties

        private float _boundRadius;
        private VisualElement joystickLever;
        private VisualElement joystickBound;
        private Vector3 _pointerStartPosition;

        #endregion

        protected override void PointerDownHandler(PointerDownEvent evt)
        {
            base.PointerDownHandler(evt);

            joystickLever.transform.position = Vector2.zero;
            var pos = -(joystickBound.layout.center - (Vector2)evt.position);
            joystickBound.transform.position = pos;
            _pointerStartPosition = evt.position;
        }

        protected override void PointerMoveHandler(PointerMoveEvent evt)
        {
            if (!IsDrag) return;

            if (joystickLever.ClassListContains("joystick--on") == false)
                joystickLever.AddToClassList("joystick--on");

            joystickLever.transform.position = Vector2.ClampMagnitude(evt.position - _pointerStartPosition,
                _boundRadius);
        }

        protected override void PointerUpHandler(PointerUpEvent evt)
        {
            if (!IsDrag) return;

            if (joystickLever.ClassListContains("joystick--on"))
                joystickLever.RemoveFromClassList("joystick--on");
            base.PointerUpHandler(evt);
        }

        public MoveValue GetControllerValue()
        {
            if (joystickLever == null || !IsDrag) return new MoveValue();
            var pos = joystickLever.transform.position.normalized;

            return new MoveValue()
            {
                value = joystickLever.transform.position.magnitude / _boundRadius,
                vector = new Vector3(pos.x, 0, -pos.y)
            };
        }
    }
}