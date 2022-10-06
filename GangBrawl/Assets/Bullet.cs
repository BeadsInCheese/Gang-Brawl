using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public int damage = 10;
    public float knockback = 10;
    // Start is called before the first frame update

    // Directly taken from meleeAttack.cs Should probably be changed to use the same code instead of copy paste
    void OnTriggerEnter2D(Collider2D collision)
    {
        Helpers.HitPlayer(damage, collision.gameObject, gameObject, knockback);
        Destroy(gameObject);

    }

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
        // The if clause is directly taken from diesOutOfBounds 
        if (this.transform.position.y < ArenaDataManager.instance.arenaLowerBound || Mathf.Abs(this.transform.position.x) > ArenaDataManager.instance.arenaSideBound)
        {
            Destroy(gameObject);
        }
    }
}
