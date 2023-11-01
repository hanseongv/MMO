using Manager;
using UnityEngine;

namespace Control
{
    public class SelectObject
    {
        private Vector3 pointPos;

        internal RaycastHit? SelectObjectUpdate()
        {
            // 마우스 클릭 시
            if (SelectCheck())
            {
                pointPos = GetSelectPosition();
                
                // https://docs.unity3d.com/ScriptReference/Touch-phase.html
                // if (Input.GetTouch(0).phase != TouchPhase.Began)
                //     return null;

                var ray = GameManager.Instance.cameraManager.CurrentCamera.ScreenPointToRay(pointPos);

                if (Physics.Raycast(ray, out var hit))
                {
                    return hit;
                }
            }

            return null;
        }

        private bool SelectCheck()
        {
#if UNITY_EDITOR
            return Input.GetMouseButtonDown(0);
#else
            return Input.touchCount > 0;
#endif
        }

        private Vector3 GetSelectPosition()
        {
#if UNITY_EDITOR
            return Input.mousePosition;
#else
            return Input.GetTouch(0).position;
#endif
        }
    }
}