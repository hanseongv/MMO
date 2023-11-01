using Control;
using State.Interface;
using UnityEngine;
using UnityEngine.UIElements;

//todo 시네마틱으로 전환 시키는 방법 추가 
namespace Manager
{
    public class CameraManager : MonoBehaviour, IManager
    {
        internal GameObject mainCamera;
        internal GameObject _currentCameraObject;
        internal Camera CurrentCamera;


        private bool _isFollowing;
        private const string ContainerRotation = "ContainerRotation";


        public void Init()
        {
            mainCamera = GameObject.FindWithTag("MainCamera");
            ChangeCamera(mainCamera);
        }

        void ChangeCamera(GameObject useCamera)
        {
            _currentCameraObject = useCamera;
            CurrentCamera = _currentCameraObject.GetComponentInChildren<Camera>();
        }


        internal Vector3 GetCameraDirection()
        {
            return _currentCameraObject.transform.TransformDirection(
                GameManager.Instance.controlManager.GetControllerValue().vector);
        }
    }
}