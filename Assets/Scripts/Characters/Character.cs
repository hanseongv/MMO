using Abstract;
using State;
using UnityEngine;

namespace Characters
{
    public class Character : MonoBehaviour
    {
        private CharacterStateType _currentCharacterStateType;
        private StateMachine _stateMachine;
        private BaseState _idleState;
        internal BaseState MoveState;

        public float moveSpeed = 1.0f;

        internal Animator Animator;

        protected void ChangeState(CharacterStateType characterStateType)
        {
            if (_currentCharacterStateType == characterStateType)
                return;

            _currentCharacterStateType = characterStateType;
            switch (_currentCharacterStateType)
            {
                case CharacterStateType.Idle:
                    _stateMachine.ChangeState(_idleState);
                    break;

                case CharacterStateType.Move:
                    _stateMachine.ChangeState(MoveState);
                    break;
                default:
                    _stateMachine.ChangeState(_idleState);
                    break;
            }
        }

        internal virtual void Init()
        {
            _idleState = new IdleState(this);
            _currentCharacterStateType = CharacterStateType.Idle;
            _stateMachine = new StateMachine(_idleState);
        }


        protected virtual void Update()
        {
            _stateMachine.UpdateState();
        }
    }
}