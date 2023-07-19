using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineBullet :  Bullet
{
    public GameObject explosion;
    // Start is called before the first frame update
    public float activationTime = 4;
    bool activated=false;
    public void activate()
    {
        activated = true;
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("activate", activationTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if ((collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("ExplosivesBarrel"))&&activated)
        {
            var ex = Instantiate(explosion);
            Helpers.HitPlayer(damage, collision.gameObject, rb.velocity.normalized * knockback);
            ex.transform.position = new Vector2(transform.position.x, transform.position.y);
            Destroy(gameObject);
        }




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
