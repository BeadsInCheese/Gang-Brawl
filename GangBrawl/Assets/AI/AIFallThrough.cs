using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFallThrough : FallThroughPlatform
{
    // Start is called before the first frame update
    public bool goDown=false;
    void Start()
    {
        body = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_collider!=null&&_playerOnPlarform && goDown)
        {
            Physics2D.IgnoreCollision(_collider, body);
            StartCoroutine(EnableCollider(_collider)); // must reference to same object which has the ignored collision
        }
    }
}
