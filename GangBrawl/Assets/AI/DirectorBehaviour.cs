using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectorBehaviour : MonoBehaviour
{
    BTTree tree;
    float intensity=11;
    public static int MaxLives = 5;
    public float intensityTreshold=10;
    public static Dictionary<string,int> PlayersAlive=new Dictionary<string, int>();
    public static Dictionary<string,int> PlayerKills=new Dictionary<string, int>();
    float countdown=5;
    public bool debugMode=false;
    //Behaviour delegate methods
    public TMPro.TextMeshProUGUI gameOverText;
    public TMPro.TextMeshProUGUI countdownText;
    public TMPro.TextMeshProUGUI roundCountDownText;
    public enum Gamemode{LASTMANSTANDING,DEATHMATCH};
    public static Gamemode gameMode=Gamemode.DEATHMATCH;
    public static float gameTime = 120;
    public GameObject gameStartCountDown;
    public Node.Status IsLowIntensity(){
        if(intensity>intensityTreshold){
            Earthquake.shaking=false;
            return Node.Status.SUCCESS;



        }
        return Node.Status.FAILURE;

    }
    
    public Node.Status GameEnded(){
        if(gameMode==Gamemode.LASTMANSTANDING){
        int alive=0;
        if(PlayersAlive.Count<=1){
            return Node.Status.FAILURE;
        }
        foreach(var i in PlayersAlive.Keys){
            if(PlayersAlive[i]>0){
                alive++;
            }
        }
        if(alive<=1&&!debugMode){
            return Node.Status.SUCCESS;
        }
        return Node.Status.FAILURE;
        }
        else if(gameMode==Gamemode.DEATHMATCH){

            if(gameTime<=0){
                return Node.Status.SUCCESS;
            }else{
                return Node.Status.FAILURE;
            }
        }
        return Node.Status.FAILURE;
    }
    bool VictorFound=false;
    public Node.Status AnnounceVictor(){
        countdown-=Time.deltaTime;
        bool draw=false;
        string playername="";
        if(gameMode== Gamemode.LASTMANSTANDING){
        foreach(var i in PlayersAlive.Keys){
            if(PlayersAlive[i]>0){
                playername=i;
                break;
            }
        }
        }else{
            if(gameMode==Gamemode.DEATHMATCH){
                int mostplayerkills=-1;
                 foreach(var i in PlayersAlive.Keys){
                   if(PlayerKills[i]>mostplayerkills){
                        playername=i;
                        mostplayerkills=PlayerKills[i];
                    }else if(PlayerKills[i]==mostplayerkills){
                        playername+=" & "+i;
                        draw=true;
                    }
                }
                       
            }
        }
        if(countdown<=0){
            SceneManager.LoadScene("Lobby");
            return Node.Status.SUCCESS;
        }
        if(!VictorFound){

            VictorFound=true;
            if(draw){ gameOverText.text=playername+" draw";} else{ gameOverText.text=playername+" wins";}
        }
            countdownText.text="Returning to\n Lobby in\n"+Mathf.Ceil(countdown);
        
        return Node.Status.RUNNING;

    }
    public Node.Status debugTest(){
        Earthquake.shaking=true;
        return Node.Status.SUCCESS;
    }
    
    //init tree
    public BTTree CreateTree(){
        BTTree root=new BTTree();
        SelectorNode selectFromActions=new SelectorNode("selectFromActions");
        SequenceNode GameOverSequence=new SequenceNode("Game Over");
        LeafNode GameOverCondition=new LeafNode("Game Over Condition",GameEnded);
        LeafNode announceVictor=new LeafNode("Announcing Victor",AnnounceVictor);
        root.children.Add(selectFromActions);
        selectFromActions.AddChild(GameOverSequence);
        GameOverSequence.AddChild(GameOverCondition);
        GameOverSequence.AddChild(announceVictor);

        LeafNode LowIntensity=new LeafNode("LowIntensity",IsLowIntensity);
        selectFromActions.AddChild(LowIntensity);
        selectFromActions.AddChild(new LeafNode("debug",debugTest));

        
        return root;
    }
    void Start()
    {
        Instantiate(gameStartCountDown);
        countdownText.text="";
        gameOverText.text="";
        tree=CreateTree();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameMode==Gamemode.DEATHMATCH){
            roundCountDownText.text=""+Mathf.Ceil(gameTime);
        }else{

            roundCountDownText.text="";
        }
        gameTime-=Time.deltaTime;
        gameTime=Mathf.Max(0,gameTime);
        tree.Process();
    }
}
