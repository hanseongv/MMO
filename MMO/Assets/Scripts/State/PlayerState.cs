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
        }
    }

    public class PlayerWalkState : MonoBehaviour, IPlayerState
    {
        private PlayerController _playerController;

        public void Handle(PlayerController playerController)
        {
            if (!_playerController)
                _playerController = playerController;
            _playerController.CurrentSpeed = _playerController.walkSpeed;
        }

        private void Update()
        {
            if (_playerController)
            {
                if (_playerController.CurrentSpeed > 0)
                {
                    _playerController.transform.Translate(Vector3.forward *
                                                          (_playerController.CurrentSpeed * Time.deltaTime));
                }
            }
        }
    }
}