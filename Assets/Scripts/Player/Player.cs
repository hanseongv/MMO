using UnityEngine;

namespace Player
{
    public class Player : Character
    {
        internal override void Init()
        {
            MoveState = new MyPlayerMoveState(this);
            Animator = GetComponent<Animator>();
            base.Init();
        }

        protected override void Update()
        {
            base.Update();

            var controllerValue = GameManager.Instance.controlManager.GetControllerValue().value;
            ChangeState(0.0f < controllerValue ? State.Move : State.Idle);
        }
    }
}