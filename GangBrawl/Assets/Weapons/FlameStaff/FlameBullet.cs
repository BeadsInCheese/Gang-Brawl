using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBullet : Bullet
{
    // Start is called before the first frame update
    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        Destroy(gameObject,0.5f);
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
