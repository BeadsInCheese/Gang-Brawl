using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DirectorBehaviour : MonoBehaviour
{
    BTTree tree;
    float intensity=11;
    public float intensityTreshold=10;
    public static Dictionary<string,int> PlayersAlive=new Dictionary<string, int>();
    float countdown=5;
    public bool debugMode=false;
    //Behaviour delegate methods
    public TMPro.TextMeshProUGUI gameOverText;
    public TMPro.TextMeshProUGUI countdownText;
    public Node.Status IsLowIntensity(){
        if(intensity>intensityTreshold){
            return Node.Status.SUCCESS;


        }
        return Node.Status.FAILURE;

    }
    public Node.Status GameEnded(){
        int alive=0;
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
    public Node.Status AnnounceVictor(){
        countdown-=Time.deltaTime;
        string playername="";
        foreach(var i in PlayersAlive.Keys){
            if(PlayersAlive[i]>0){
                playername=i;
                break;
            }
        }
        if(countdown<=0){
            SceneManager.LoadScene("Lobby");
            return Node.Status.SUCCESS;
        }
        gameOverText.text=playername+" wins";
        countdownText.text="Returning to\n Lobby in\n"+Mathf.Ceil(countdown);

        return Node.Status.RUNNING;

    }
    public Node.Status debugTest(){
        Debug.Log("The intensity is low");
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
        countdownText.text="";
        gameOverText.text="";
        tree=CreateTree();
    }

    // Update is called once per frame
    void Update()
    {
        tree.Process();
    }
}