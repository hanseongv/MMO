using Manager;
using State;
using UnityEngine;

namespace Characters
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

            var controllerValue = GameManager.Instance.controlManager.GetControllerValue().Value;
            ChangeState(0.0f < controllerValue ? CharacterStateType.Move : CharacterStateType.Idle);
        }
    }
}