using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiesAndRespawnsOnRope : ExplodeOnDeath
{
    // Start is called before the first frame update    override

    public float respawnTime=5;
    public bool linked=true;
    public RopeAnchor anchor;
    
    override
    public void die(){
        var ex = Instantiate(explosion);
        ex.transform.position=new Vector2(transform.position.x,transform.position.y);
        HealToFull();
        anchor.Invoke("DestroyRopeContainer", 0);
        anchor.Invoke("GenerateRope", respawnTime);
        Destroy(gameObject.transform.parent.gameObject);
    }
    void Update(){
    }
}
