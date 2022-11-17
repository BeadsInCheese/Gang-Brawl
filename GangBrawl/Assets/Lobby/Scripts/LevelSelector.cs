using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;
public class LevelSelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    int modeCounter=0;
    private DirectorBehaviour.Gamemode modeChange(){
        DirectorBehaviour.Gamemode[] modes={DirectorBehaviour.Gamemode.DEATHMATCH,DirectorBehaviour.Gamemode.LASTMANSTANDING};
        modeCounter=(modeCounter+1)%modes.Length;
        DirectorBehaviour.Gamemode mode=modes[modeCounter];
        DirectorBehaviour.gameMode=mode;
        return mode;
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
