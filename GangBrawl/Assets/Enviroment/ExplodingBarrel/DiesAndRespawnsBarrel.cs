using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiesAndRespawnsBarrel : ExplodeOnDeath
{
    // Start is called before the first frame update    override
    public Transform spawnPoint;
    public float respawnTime=5;
    private void respawn(){
        this.transform.position=spawnPoint.position;
        this.transform.rotation=spawnPoint.rotation;
        this.gameObject.SetActive(true);
    }
    override
    public void die(){
        var ex = Instantiate(explosion);
        ex.transform.position=new Vector2(transform.position.x,transform.position.y);
        HealToFull();
        this.gameObject.SetActive(false);
        Invoke("respawn",respawnTime);
    }
}
