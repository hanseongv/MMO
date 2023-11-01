using UnityEngine;
using UnityEngine.UIElements;

namespace Abstract
{
    public abstract class BaseDragManipulator : PointerManipulator
    {
        #region Init
        internal virtual void Init(VisualElement t)
        {
            target = t;
        }

        internal bool IsDrag;
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
        #endregion





        protected virtual void PointerDownHandler(PointerDownEvent evt)
        {
            target.CapturePointer(evt.pointerId);
            IsDrag = true;
        }


        protected virtual void PointerMoveHandler(PointerMoveEvent evt)
        {
        }


        protected virtual void PointerUpHandler(PointerUpEvent evt)
        {

            IsDrag = false;
            target.ReleasePointer(evt.pointerId);
        }
    }
}