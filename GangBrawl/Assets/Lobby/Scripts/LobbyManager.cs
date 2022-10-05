using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject row1;
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;
    int playersInGame=0;
    int playersReady=0;
    float countdown=5;

    public static List<PlayerData> playerData=new List<PlayerData>();
    public void playerPressedReady(){
        playersReady+=1;
    }
    public void playerPressedUnready(){
        playersReady-=1;
    }
    public static LobbyManager instance;
    public void OnPlayerJoined(PlayerInput playerInput){
        InputDevice[] d;
        d=playerInput.devices.ToArray();
        Debug.Log(d[0]);
        playerData.Add(new PlayerData("",-1,playerInput.currentControlScheme,d));
        playersInGame+=1;
        playerInput.transform.SetParent(row1.transform,false);
        if(player1.transform.parent==row1.transform){
            player1.transform.SetParent(playerInput.transform,false);
        }else if(player2.transform.parent==row1.transform){
            player2.transform.SetParent(playerInput.transform,false);
        }else if(player3.transform.parent==row1.transform){
            player3.transform.parent.SetParent(playerInput.transform,false);
        }else if(player4.transform.parent==row1.transform){
            player4.transform.parent.SetParent(playerInput.transform,false);
        }

    }
        void Awake()
    {
        if(instance!=null && instance!=this){
            Destroy(this);
        }else{
            instance=this;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playersReady>=playersInGame&&playersInGame>=1){
            countdown-=Time.deltaTime;
            if(countdown<=0){
                SceneManager.LoadScene("TestSandbox");
            }
        }else{
            countdown=5;
        }
    }
}
