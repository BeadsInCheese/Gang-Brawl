using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloon : MonoBehaviour
{
    public GameObject Pop;
    public GameObject basket;
    public GameObject ball;

    private bool dead=false;
    private Vector2 basketOriginalPos=new Vector2(0,0);
        // Start is called before the first frame update
    public void ReSpawn(){
        dead=false;
        ball.SetActive(true);
        basket.transform.position=basketOriginalPos;
    }
    void OnTriggerEnter2D(Collider2D col){
        if( col.gameObject.tag.Equals("Bullet")){
            Instantiate(Pop).transform.position=this.transform.position;
            dead=true;
            ball.SetActive(false); 
            Invoke("ReSpawn",10);
        }
    }
    void Start()
    {
 basketOriginalPos=basket.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(dead){
        basket.transform.position=new Vector2(basket.transform.position.x,basket.transform.position.y+-10*Time.deltaTime);
    }
    }
}
