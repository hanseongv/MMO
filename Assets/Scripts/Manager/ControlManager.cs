
using Control;
using State.Interface;
using UnityEngine;
using UnityEngine.UIElements;

namespace Manager
{
    internal enum ControlType
    {
        Joystick,
    }

    public class ControlManager : MonoBehaviour, IManager
    {
        [SerializeField] private ControlType controlType;
        private const string ControlContainer = "ControlContainer";

        internal IController CurrentController;
        private VisualElement root;

        public void Init()
        {
            // UI 도큐먼트에 있는 최상위 비주얼 엘리먼트를 참조.
            root = GetComponent<UIDocument>().rootVisualElement;


            switch (controlType)
            {
                case ControlType.Joystick:
                    CurrentController = new JoystickControl();
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
            CurrentController.Init(root.Q<VisualElement>(ControlContainer));
        }

        public MoveValue GetControllerValue()
        {
            return CurrentController.GetControllerValue();
        }
    }
}