using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine;
public class PlayerData
{
    public string characterPrefabPath;
    public int playerIndex;
    public string controlScheme;
    public Sprite hat;
    public InputDevice[] pairWithDevices;

    // public ControllerType controllerType;
    public PlayerData(string characterPrefab, int playerIndex, string controlScheme, InputDevice[] devices)
    {
        characterPrefabPath = characterPrefab;
        this.playerIndex = playerIndex;
        this.controlScheme = controlScheme;
        this.pairWithDevices = devices;
    }

    public static ControllerType recogniseControllerType(string controlScheme)
    {
        // I could swear that my ps5 controller gave one time it's type as "wireless controller", not sure why
        if (controlScheme.ToLower().Contains("controller") || controlScheme.ToLower().Contains("gamepad"))
        {
            // this.controllerType = ControllerType.Gamepad;
            return ControllerType.Gamepad;
        }
        else if (controlScheme.ToLower().Contains("keyboard"))
        {
            return ControllerType.Keyboard;
        }
        else
        {
            return ControllerType.Not_Recognized;
        }

    }
}


