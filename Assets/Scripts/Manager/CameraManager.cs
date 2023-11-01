using Interface;
using UnityEngine;

//todo 시네마틱으로 전환 시키는 방법 추가 
namespace Manager
{
    public class CameraManager : MonoBehaviour, IManager
    {
        private GameObject mainCamera;
        internal GameObject CurrentCameraObject;
        internal Camera CurrentCamera;

        public void Init()
        {
            mainCamera = GameObject.FindWithTag("MainCamera");
            ChangeCamera(mainCamera);
        }

        private void ChangeCamera(GameObject useCamera)
        {
            CurrentCameraObject = useCamera;
            CurrentCamera = CurrentCameraObject.GetComponentInChildren<Camera>();
        }

        internal Vector3 GetCameraDirection()
        {
            return CurrentCameraObject.transform.TransformDirection(
                GameManager.Instance.controlManager.GetControllerValue().Vector);
        }
    }
}