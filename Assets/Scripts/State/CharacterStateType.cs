using Abstract;
using Characters;
using Manager;
using UnityEngine;

namespace State
{
    public enum CharacterStateType
    {
        Idle,
        Move,
    }

    public class IdleState : BaseState
    {
        public IdleState(Character character) : base(character)
        {
        }

        public override void OnStateEnter()
        {
            Character.Animator.SetBool("isWalk", false);
            Character.Animator.SetBool("isRun", false);
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
            Character.Animator.SetBool("isWalk", true);
        }

        public override void OnStateUpdate()
        {
            var moveSpeedValue = GameManager.Instance.controlManager.GetControllerValue().Value;

            // 카메라가 바라보는 방향
            var cameraDirection = GameManager.Instance.cameraManager.GetCameraDirection();

            if (cameraDirection == Vector3.zero)
                return;

            float y = Quaternion.LookRotation(cameraDirection).eulerAngles.y;

            // 회전
            if (Character == null)
            {
                return;
            }

            Character.transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
            // 이동
            Character.transform.Translate(
                Vector3.forward * (Character.moveSpeed *
                                   moveSpeedValue *
                                   Time.deltaTime));

            var isRun = 0.5f < moveSpeedValue;
            Character.Animator.SetBool("isRun", isRun);
        }

        public override void OnStateExit()
        {
        }
    }
}