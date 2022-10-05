using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerSpawner : MonoBehaviour
{
    PlayerInputManager playerInputManager;
    public void spawnPlayers(){
        int playerNo=0;
        foreach(PlayerData i in LobbyManager.playerData){

            playerInputManager.JoinPlayer(playerNo,-1,null,i.pairWithDevices);
            playerNo=playerNo+1;
            
        }
        LobbyManager.playerData=new List<PlayerData>();
        
    }

    // Start is called before the first frame update
    void Start()
    {
        playerInputManager=GetComponent<PlayerInputManager>();
        spawnPlayers();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
