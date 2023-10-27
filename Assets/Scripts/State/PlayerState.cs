using System;
using UnityEngine;

namespace State
{
    public class PlayerState
    {
    }

    public class PlayerIdleState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;

        public void Handle(PlayerController playerController)
        {
            if (!_playerController)
                _playerController = playerController;
            playerController.CurrentSpeed = 0;
            _playerController._animator.SetBool("isWalk", false);
            _playerController._animator.SetBool("isRun", false);
        }
    }

    public class PlayerWalkState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;

        public void Handle(PlayerController playerController)
        {
            if (!_playerController)
                _playerController = playerController;
            _playerController.CurrentSpeed = _playerController.moveSpeed;

            _playerController._animator.SetBool("isWalk", true);

            var speed = _playerController.JoystickValues.LeverPower;
        }

        private void Update()
        {
            if (_playerController)
            {
                if (_playerController.CurrentSpeed > 0)
                {
                    MoveVector();
                }
            }
        }

        void MoveVector()
        {
            // 카메라가 바라보는 기준으로 조이스틱 포지션 정의
            var vector =
                _playerController.cameraTransform.TransformDirection(_playerController.JoystickValues.LeverPos);
            var speed = _playerController.JoystickValues.LeverPower;
            float y = Quaternion.LookRotation(vector).eulerAngles.y;

            // 회전
            _playerController.transform.rotation = Quaternion.Euler(new Vector3(0, y, 0));
            // 이동
            _playerController.transform.Translate(
                Vector3.forward * (_playerController.CurrentSpeed *
                          speed *
                          Time.deltaTime));
            
            var isRun = 0.35f < speed;
            _playerController._animator.SetBool("isRun", isRun);
        }
    }
}