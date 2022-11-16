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
    public static int CPUCount=0;
    public static List<PlayerData> playerData=new List<PlayerData>();
    public void playerPressedReady(){
        playersReady+=1;
    }
    public void playerPressedUnready(){
        playersReady-=1;
    }
    public static LobbyManager instance;
    public void AddAI(){
        if(!player1.tag.Equals("Player")){
            //player1.transform.SetParent(playerInput.transform,false);
            player1.tag="Player";
            player1.GetComponentInChildren<TMPro.TextMeshProUGUI>().text="Ready";
        }else if(!player2.tag.Equals("Player")){
            //player2.transform.SetParent(playerInput.transform,false);
            player2.tag="Player";
            player2.GetComponentInChildren<TMPro.TextMeshProUGUI>().text="Ready";
        }else if(!player3.tag.Equals("Player")){
            player3.tag="Player";
            player3.GetComponentInChildren<TMPro.TextMeshProUGUI>().text="Ready";
        }else if(!player4.tag.Equals("Player")){
            player4.tag="Player";
            player4.GetComponentInChildren<TMPro.TextMeshProUGUI>().text="Ready";
        }else{
            return;
        }

        LobbyManager.CPUCount+=1;
        LobbyManager.instance.playerPressedReady();

    }
    public void OnPlayerJoined(PlayerInput playerInput){
        InputDevice[] d;
        d=playerInput.devices.ToArray();
        playerData.Add(new PlayerData("",-1,playerInput.currentControlScheme,d));
        playersInGame+=1;
        //playerInput.transform.SetParent(row1.transform,false);
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
        }else if(!player4.tag.Equals("Player")){
            playerInput.transform.gameObject.GetComponent<lobbyPlayer>().LobbyObject=player4;
            player4.tag="Player";
        }
        

    }
        void Awake()
    {
        CPUCount=0;
        if(instance!=null && instance!=this){
            Destroy(instance);
            instance=this;
        }else{
            instance=this;
        }
    }
    void Start()
    {
        DirectorBehaviour.PlayersAlive=new Dictionary<string, int>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playersReady>=playersInGame+CPUCount&&playersInGame+CPUCount>1){
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
