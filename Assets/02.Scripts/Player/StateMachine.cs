using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public ICharacterState CurrentState { get; set; }

    private readonly Character _character;

    public StateMachine(Character character)
    {
        _character = character;
    }
    public void Transition(ICharacterState state)
    {
        // 같은면 변경 없음.
        if (CurrentState == state)
            return;
        CurrentState = state;
        CurrentState.Handle(_character);
    }
}