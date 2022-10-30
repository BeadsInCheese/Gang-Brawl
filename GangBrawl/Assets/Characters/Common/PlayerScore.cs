using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScore : MonoBehaviour
{
    // Start is called before the first frame update
    public DeathCounter death;
    public int MaxLives=5;
     int lives=5;
    void Start()
    {
        this.gameObject.name="player"+DirectorBehaviour.PlayersAlive.Count;
        DirectorBehaviour.PlayersAlive.Add(this.gameObject.name,lives);
    }
    
    // Update is called once per frame
    void Update()
    {
        lives=MaxLives-death.deathCount;
        DirectorBehaviour.PlayersAlive[this.gameObject.name]=lives;
        if(lives<=0){
            this.gameObject.transform.position=new Vector2(0,100);
            
        }
    }
}
