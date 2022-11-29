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
        transform.position=new Vector3(0,0,transform.position.z);
            selectedPlayerPrefab=(GameObject)Resources.Load("/Characters/ProgrammerArtMan/Character.prefab", typeof(GameObject));
            Invoke("removeJoinText",0.1f);
            input=GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    public float speed=1;
    void Update()
    {
        Vector2 dir=input.actions["Aim"].ReadValue<Vector2>();
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
        Vector2 bounds=Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
        
        transform.position=new Vector3(Mathf.Clamp(transform.position.x+dir.x*speed*Time.deltaTime,-bounds.x,bounds.x),Mathf.Clamp(transform.position.y+dir.y*speed*Time.deltaTime,-bounds.y,bounds.y),transform.position.z);
    }
}
