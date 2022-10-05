using System.Collections;
using UnityEngine.InputSystem;
public class PlayerData
{
    public string characterPrefabPath;
    public int playerIndex;
    public string controlScheme;
    public InputDevice[] pairWithDevices;
    public PlayerData(string characterPrefab, int playerIndex,string controlScheme,InputDevice[] devices){
        characterPrefabPath=characterPrefab;
        this.playerIndex=playerIndex;
        this.controlScheme=controlScheme;
        this.pairWithDevices=devices;

    }
}
