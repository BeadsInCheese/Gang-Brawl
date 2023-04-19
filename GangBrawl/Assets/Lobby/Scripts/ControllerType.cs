using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;

/// <summary>
/// There are currently two controllertypes, a gamepad (e.g. ps5 controller) called gamepad, and Keyboard for Keyboard.
/// </summary>
public enum ControllerType
{
    Gamepad,
    Keyboard,

    Not_Recognized
}