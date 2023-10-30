using System.Collections;
using System.Collections.Generic;
using State.Interface;
using UnityEngine;

enum ControlType
{
    Joystick,
}

public class ControlManager :MonoBehaviour, IManager
{
    [SerializeField] private ControlType _controlType;
    internal IController currentController;

    public void Init()
    {
        switch (_controlType)
        {
            case ControlType.Joystick:
                currentController = GetComponentInChildren<JoystickControl>();
                break;
        }
    }
}