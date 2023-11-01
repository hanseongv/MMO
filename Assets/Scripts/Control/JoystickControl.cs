using Abstract;
using Interface;
using UnityEngine;
using UnityEngine.UIElements;

namespace Control
{
    public class JoystickControl : BaseDragManipulator, IMoveController
    {
        #region Properties

        private float _boundRadius;
        private VisualElement joystickLever;
        private VisualElement joystickBound;
        private Vector3 _pointerStartPosition;
        const string ImageJoystickBound = "ImageJoystickBound";
        const string ImageJoystickLever = "ImageJoystickLever";

        #endregion

        public void ControllerInit(VisualElement t)
        {
            Init(t);

            joystickBound = target.Q<VisualElement>(ImageJoystickBound);
            joystickLever = target.Q<VisualElement>(ImageJoystickLever);
            //todo 핸드폰 사이즈 비례로 할지...
            _boundRadius = joystickBound.localBound.width / 1.5f;
            // _boundRadius = joystickBound.localBound.width;
        }


        protected override void PointerDownHandler(PointerDownEvent evt)
        {
            if (IsDrag) return;

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
                Value = joystickLever.transform.position.magnitude / _boundRadius,
                Vector = new Vector3(pos.x, 0, -pos.y)
            };
        }
    }
}