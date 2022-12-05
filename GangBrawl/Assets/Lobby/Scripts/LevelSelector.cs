using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

using UnityEngine.InputSystem;
public class LevelSelector : MonoBehaviour
{
    public TMP_Text modeLabel;
    // Start is called before the first frame update
    void Start()
    {
        UpdateModeLabel();
    }

    // Update is called once per frame
    int modeCounter = 0;
    public DirectorBehaviour.Gamemode modeChange()
    {
        DirectorBehaviour.Gamemode[] modes = { DirectorBehaviour.Gamemode.DEATHMATCH, DirectorBehaviour.Gamemode.LASTMANSTANDING };
        modeCounter = (modeCounter + 1) % modes.Length;
        DirectorBehaviour.Gamemode mode = modes[modeCounter];
        DirectorBehaviour.gameMode = mode;
        // change the mode button text in the settings
        UpdateModeLabel();
        return mode;
    }

    public void UpdateModeLabel()
    {
        // Not entirely sure why this requires a null check
        if (modeLabel != null)
        {
            Debug.Log("ModeLabel was updated and was not null");
            if (DirectorBehaviour.gameMode == DirectorBehaviour.Gamemode.DEATHMATCH)
            {
                modeLabel.text = "Deathmatch";
            }
            else if (DirectorBehaviour.gameMode == DirectorBehaviour.Gamemode.LASTMANSTANDING)
            {
                modeLabel.text = "Last Man Standing";
            }
        }
    }
    void Update()
    {
    if(Gamepad.current!=null){
       if(Gamepad.current.yButton.wasPressedThisFrame){
            Debug.Log("Map change was requested.");
        }
           if(Gamepad.current.rightTrigger.wasPressedThisFrame){
            Debug.Log("mode: "+modeChange());
        }
    }
    if(Keyboard.current!=null){
       if(Keyboard.current.cKey.wasPressedThisFrame){
            Debug.Log("Map change was requested.");
        }
        if(Keyboard.current.mKey.wasPressedThisFrame){
            Debug.Log("mode: "+modeChange());
        }
    }
    }
}
