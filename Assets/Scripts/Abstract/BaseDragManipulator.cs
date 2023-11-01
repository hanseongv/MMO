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


        internal Vector2 DragVector;
        private Vector2 currentPosition;


        protected virtual void PointerDownHandler(PointerDownEvent evt)
        {
            target.CapturePointer(evt.pointerId);
            IsDrag = true;
        }


        protected virtual void PointerMoveHandler(PointerMoveEvent evt)
        {
            if (!IsDrag) return;
        }


        protected virtual void PointerUpHandler(PointerUpEvent evt)
        {
            if (!IsDrag) return;

            IsDrag = false;
            target.ReleasePointer(evt.pointerId);
        }
    }
}