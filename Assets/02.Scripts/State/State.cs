using UnityEngine;

namespace _02.Scripts.State
{
    public class IdleState : BaseState
    {
        public IdleState(Character character) : base(character)
        {
        }

        public override void OnStateEnter()
        {
            _character.Animator.SetBool("isWalk", false);
            _character.Animator.SetBool("isRun", false);
        }

        public override void OnStateUpdate()
        {
        }

        public override void OnStateExit()
        {
        }
    }

    public class MyPlayerMoveState : BaseState
    {
        public MyPlayerMoveState(Character character) : base(character)
        {
        }

        public override void OnStateEnter()
        {
            _character.Animator.SetBool("isWalk", true);
        }

        public override void OnStateUpdate()
        {
            // 카메라가 바라보는 방향
            var cameraDirection = GameManager.Instance.cameraManager.GetCameraDirection();

            var moveSpeedValue = GameManager.Instance.controlManager.currentController.GetControllerValue().value;

            float y = Quaternion.LookRotation(cameraDirection).eulerAngles.y;

            // 회전
            if (_character == null)
            {
                return;
            }

            _character.transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
            // 이동
            _character.transform.Translate(
                Vector3.forward * (_character.moveSpeed *
                                   moveSpeedValue *
                                   Time.deltaTime));

            var isRun = 0.5f < moveSpeedValue;
            _character.Animator.SetBool("isRun", isRun);
        }

        public override void OnStateExit()
        {
        }
    }
}