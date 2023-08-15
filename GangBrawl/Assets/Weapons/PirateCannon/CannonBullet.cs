using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : Bullet
{
    public GameObject explosion;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            var p = collision.gameObject;
            if (p.GetComponent<HPSystem>().currentHp - damage <= 0)
            {
                DirectorBehaviour.PlayerKills[owner] += 1;
                //Debug.Log(owner+ " has "+DirectorBehaviour.PlayerKills[owner]+" kills.");
            }

        }
        Helpers.HitPlayer(damage, collision.gameObject, rb.velocity.normalized * knockback);


        if (collision.gameObject.tag.Equals("Platform") )
        {
            //Destroy(gameObject);
            Vector2 velNormal;
            
            var col = Physics2D.Raycast(transform.position , rb.velocity, Mathf.Infinity);
            if (col)
            {
                 velNormal = (col.normal);
                rb.velocity = -(2 * Vector2.Dot(rb.velocity, (velNormal)) * velNormal - rb.velocity);
            }
           
            var x = Instantiate(explosion);
            x.transform.position = transform.position;
        }


    }
}
