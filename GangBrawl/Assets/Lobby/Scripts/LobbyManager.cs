using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
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
    public TMPro.TextMeshProUGUI countdownText;
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
        playerData.Add(new PlayerData("",-1,playerInput.currentControlScheme,d));
        playersInGame+=1;
        playerInput.transform.SetParent(row1.transform,false);
        if(!player1.tag.Equals("Player")){
            //player1.transform.SetParent(playerInput.transform,false);
            playerInput.transform.gameObject.GetComponent<lobbyPlayer>().LobbyObject=player1;
            player1.tag="Player";
        }else if(!player2.tag.Equals("Player")){
            //player2.transform.SetParent(playerInput.transform,false);
            playerInput.transform.gameObject.GetComponent<lobbyPlayer>().LobbyObject=player2;
            player2.tag="Player";
        }else if(!player3.tag.Equals("Player")){
            playerInput.transform.gameObject.GetComponent<lobbyPlayer>().LobbyObject=player3;
            player3.tag="Player";
        }else if(player4.tag.Equals("Player")){
            playerInput.transform.gameObject.GetComponent<lobbyPlayer>().LobbyObject=player4;
            player4.tag="Player";
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
        if(playersReady>=playersInGame&&playersInGame>1){
            countdown-=Time.deltaTime;
            countdownText.text=Mathf.Ceil(countdown).ToString();
            if(countdown<=0){
                SceneManager.LoadScene("TestSandbox");
            }
        }else{
            countdown=5;
            countdownText.text="";
        }
    }
}
