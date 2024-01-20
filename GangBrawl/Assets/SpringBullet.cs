using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpringBullet : Bullet
{
       public float activationTime = 4;

    public void activate()
    {
        Destroy(gameObject);
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Invoke("activate", activationTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {





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
