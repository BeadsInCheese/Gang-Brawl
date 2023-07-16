using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBullet : Bullet
{
    public float lifetime = 0.5f;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(owner!=collision.gameObject.name){
        if (collision.gameObject.tag.Equals("Player")){
            var p=collision.gameObject;
            if(p.GetComponent<HPSystem>().currentHp-damage<=0){
                DirectorBehaviour.PlayerKills[owner]+=1;
                //Debug.Log(owner+ " has "+DirectorBehaviour.PlayerKills[owner]+" kills.");
            }
        
        
        Helpers.HitPlayer(damage,  collision.gameObject,rb.velocity.normalized*knockback);

        
        if (!collision.gameObject.tag.Equals("Bullet") && !collision.tag.Equals("ObjectSpawner"))
        {
            Destroy(gameObject);
        }


    }}}
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject,lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Helpers.isOutOfArena(gameObject))
        {
            Destroy(gameObject);
        }
        
    }
}
