using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnDeath : HPSystem
{
    public GameObject explosion;
    // Start is called before the first frame update
    override    
    public void die(){
        var ex = Instantiate(explosion);
        ex.transform.position=new Vector2(transform.position.x,transform.position.y);
        HealToFull();
        this.transform.position=new Vector2(0,5);
    }
}
