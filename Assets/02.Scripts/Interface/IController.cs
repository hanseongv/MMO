using System.Collections;
using System.Collections.Generic;
using _02.Scripts.Control;
using UnityEngine;

public struct ControllerValue
{
    public Vector3 vector;
    public float value;
}

public interface IController
{
    public ControllerValue GetControllerValue();
}