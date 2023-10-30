using _02.Scripts.State;
using UnityEngine;

public class StateMachine
{
    private BaseState currentState;

    public StateMachine(BaseState state)
    {
        currentState = state;
        ChangeState(currentState);
    }

    public void ChangeState(BaseState state)
    {
        if (currentState == state) return;

        if (currentState != null)
            currentState.OnStateExit();

        currentState = state;
        currentState.OnStateEnter();
    }

    public void UpdateState()
    {
        if (currentState == null) return;

        currentState.OnStateUpdate();
    }
}