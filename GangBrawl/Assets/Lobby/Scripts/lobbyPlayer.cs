using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class lobbyPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    Image characterImage;
    public GameObject LobbyObject;
    PlayerInput input;
    public GameObject selectedPlayerPrefab;
    private bool ready=false;
    TMPro.TextMeshProUGUI tex;

    private void removeJoinText(){

            tex=this.LobbyObject.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            tex.text="";
    }
    void Start()
    {
            selectedPlayerPrefab=(GameObject)Resources.Load("/Characters/ProgrammerArtMan/Character.prefab", typeof(GameObject));
            Invoke("removeJoinText",0.1f);
            input=GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    
    void Update()
    {
        if(input.actions["LightAttack"].triggered){
            //Debug.Log("Change Hero requested");
        }
        if(input.actions["LightAttack"].triggered){
            LobbyManager.instance.AddAI();
        }
        if(input.actions["Jump"].triggered){
            if(ready){
                ready=false;
                LobbyManager.instance.playerPressedUnready();
            }else{
                ready=true;
                LobbyManager.instance.playerPressedReady();
            }
        }
        if(tex!=null){
            if(ready==false){
                tex.text="Not Ready";

            }else{
                tex.text="Ready";
            }
        }
    }
}
