using UnityEngine;

namespace _02.Scripts.State
{
    public abstract class BaseState 
    {
        internal Character _character;

        protected BaseState(Character character)
        {
            _character = character;
        }

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();
    }
}