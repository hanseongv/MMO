using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStateContext : MonoBehaviour
{
    public ICharacterState CurrentState { get; set; }

    private readonly Character _character;

    public CharacterStateContext(Character character)
    {
        _character = character;
    }
    public void Transition(ICharacterState state)
    {
        if (CurrentState == state)
            return;
        
        CurrentState = state;
        CurrentState.Handle(_character);
    }
}