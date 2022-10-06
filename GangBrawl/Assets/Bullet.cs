using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        speed = 5;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y < ArenaDataManager.instance.arenaLowerBound || Mathf.Abs(this.transform.position.x) > ArenaDataManager.instance.arenaSideBound)
        {
            Destroy(gameObject);
        }
    }
}
