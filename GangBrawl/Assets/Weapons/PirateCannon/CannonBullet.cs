using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBullet : Bullet
{
    public GameObject explosion;
    int bounces = 3;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            var p = collision.gameObject;
            if (p.GetComponent<HPSystem>().currentHp - damage <= 0)
            {
                DirectorBehaviour.PlayerKills[owner] += 1;
                DirectorBehaviour.TestAndSetGoldenSpiritLead(collision.gameObject.name, owner);
                //Debug.Log(owner+ " has "+DirectorBehaviour.PlayerKills[owner]+" kills.");
            }

        }
        Helpers.HitPlayer(damage, collision.gameObject, rb.velocity.normalized * knockback, owner);


        if (collision.gameObject.tag.Equals("Platform") )
        {
            //Destroy(gameObject);
            Vector2 velNormal;
            
            var col = Physics2D.Raycast(transform.position , rb.velocity, Mathf.Infinity);
            if (col)
            {
                 velNormal = (col.normal);
                rb.velocity = -(2 * Vector2.Dot(rb.velocity, (velNormal)) * velNormal - rb.velocity);
                bounces--;
                if (bounces < 0)
                {
                    Destroy(gameObject);
                }
            }
           
            var x = Instantiate(explosion);
            x.transform.position = transform.position;
        }


    }
}
