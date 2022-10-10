using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage=10;
    public int knockback=10;
    CircleCollider2D aoe;
    // Start is called before the first frame update
    public void Dissipate(){
       Destroy(gameObject);
    }
    void Start(){
        aoe=GetComponent<CircleCollider2D>();
        Invoke("Dissipate",0.5f);
    }
    
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag.Equals("Player")){
            Vector2 dir=(this.transform.position-col.gameObject.transform.position);
            float distance = dir.magnitude;
            float aoeProximityMultiplier=(aoe.radius-distance)*30;
            Helpers.HitPlayer((int)(damage*aoeProximityMultiplier),col.gameObject,-dir.normalized*aoeProximityMultiplier);
            Debug.Log("Exploded player");
        }
    }
}
