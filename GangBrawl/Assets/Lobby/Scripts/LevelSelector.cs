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
    void Update()
    {
    if(Gamepad.current!=null){
       if(Gamepad.current.yButton.wasPressedThisFrame){
            Debug.Log("Map change was requested.");
        }
    }
    if(Keyboard.current!=null){
       if(Keyboard.current.cKey.wasPressedThisFrame){
            Debug.Log("Map change was requested.");
        }
    }
    }
}
