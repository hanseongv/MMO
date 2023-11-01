using Control;
using State.Interface;
using UnityEngine;
using UnityEngine.UIElements;

namespace Manager
{
    internal enum MoveControlType
    {
        Joystick,
    }

    public class ControlManager : MonoBehaviour, IManager
    {
        private VisualElement root;

        [SerializeField] private MoveControlType moveControlType;
        private const string MoveControlContainer = "ControlContainer";
        internal IMoveController CurrentMoveMoveController;

        public void Init()
        {
            // UI 도큐먼트에 있는 최상위 비주얼 엘리먼트를 참조.
            root = GetComponent<UIDocument>().rootVisualElement;

            switch (moveControlType)
            {
                case MoveControlType.Joystick:
                    CurrentMoveMoveController = new JoystickControl();
                    break;
            }

            /* RegisterCallback<GeometryChangedEvent>
            RegisterCallback<GeometryChangedEvent> : 레이아웃이 변경되었을 때(GeometryChangedEvent) GeometryChangedEventCallback() 함수를 실행.
            
            사용 이유 : 초기에 레이아웃 값이 Null로 잡히고 한 프레임정도 뒤에 값이 제대로 들어가기 때문에
            레이아웃이 초기 널 값에서 값이 제대로 들어갔을 때 == 레이아웃이 변경되었을 때(GeometryChangedEvent)로 체크 후에 필요한 값을 등록
            */
            root.RegisterCallback<GeometryChangedEvent>(GeometryChangedEventCallback);
        }

        private void GeometryChangedEventCallback(GeometryChangedEvent evt)
        {
            CurrentMoveMoveController.ControllerInit(root.Q<VisualElement>(MoveControlContainer));
        }

        internal MoveValue GetControllerValue()
        {
            return CurrentMoveMoveController.GetControllerValue();
        }
    }
}