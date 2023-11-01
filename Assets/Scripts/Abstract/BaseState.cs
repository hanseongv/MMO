
using Characters;

namespace Abstract
{
    public abstract class BaseState 
    {
        internal readonly Character Character;

        protected BaseState(Character character)
        {
            Character = character;
        }

        public abstract void OnStateEnter();
        public abstract void OnStateUpdate();
        public abstract void OnStateExit();
    }
}
