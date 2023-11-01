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

    // Base
    partial class ControlManager : MonoBehaviour, IManager
    {
        private VisualElement root;


        public void Init()
        {
            root = GetComponent<UIDocument>().rootVisualElement;
            _currentMoveMoveController = moveControlType switch
            {
                MoveControlType.Joystick => new JoystickControl(),
                _ => new JoystickControl()
            };

            /* RegisterCallback<GeometryChangedEvent>
            RegisterCallback<GeometryChangedEvent> : 레이아웃이 변경되었을 때(GeometryChangedEvent) GeometryChangedEventCallback() 함수를 실행.
            사용 이유 : 초기에 레이아웃 값이 Null로 잡히고 한 프레임정도 뒤에 값이 제대로 들어가기 때문에
            레이아웃이 초기 널 값에서 값이 제대로 들어갔을 때 == 레이아웃이 변경되었을 때(GeometryChangedEvent)로 체크 후에 필요한 값을 등록
            */
            root.RegisterCallback<GeometryChangedEvent>(GeometryChangedEventCallback);

            _isFollowing = true;
        }


        private void GeometryChangedEventCallback(GeometryChangedEvent evt)
        {
            _currentMoveMoveController.ControllerInit(root.Q<VisualElement>(MoveControlContainer));
            cameraRotation.Init(root.Q<VisualElement>(ImageCameraRotationEye));
        }
    }

    // Move Control
    partial class ControlManager
    {
        [SerializeField] private MoveControlType moveControlType;
        private const string MoveControlContainer = "MoveControlContainer";
        private IMoveController _currentMoveMoveController;

        internal MoveValue GetControllerValue()
        {
            return _currentMoveMoveController.GetControllerValue();
        }
    }

    // Camera Rotation Control
    partial class ControlManager
    {
        [SerializeField] private float rotateSpeed = 10.0f;
        [SerializeField] private float followSmooth = 10.0f;
        private readonly CameraRotation cameraRotation = new();
        private const string ImageCameraRotationEye = "ImageCameraRotationEye";

        private bool _isFollowing;
        private float xRotate, yRotate;

        private void Update()
        {
            FollowCamera();

            if (cameraRotation == null)
                return;

            if (cameraRotation.IsDrag)
            {
                RotationCamera();
            }
        }

        private void FollowCamera()
        {
            var currentCameraObject = GameManager.Instance.cameraManager._currentCameraObject;
            if (currentCameraObject == null || !_isFollowing) return;

            var player = GameManager.Instance.player;

            if (player == null) return;

            var playerPosition = player.transform.position;
            var currentPosition = currentCameraObject.transform.position;
            var newPosition = new Vector3(playerPosition.x, currentPosition.y,
                playerPosition.z);

            currentCameraObject.transform.position = Vector3.Lerp(currentPosition,
                newPosition,
                Time.deltaTime * followSmooth);
        }


        private void RotationCamera()
        {
            if (cameraRotation.DragVector == Vector2.zero)
                return;

            yRotate += -cameraRotation.DragVector.x * Time.deltaTime * rotateSpeed;
            xRotate += cameraRotation.DragVector.y * Time.deltaTime * rotateSpeed;

            xRotate = Mathf.Clamp(xRotate, -70, 35);

            GameManager.Instance.cameraManager._currentCameraObject.transform.eulerAngles =
                new Vector3(-xRotate, yRotate, 0);

            cameraRotation.DragVector = Vector2.zero;
        }
    }
}