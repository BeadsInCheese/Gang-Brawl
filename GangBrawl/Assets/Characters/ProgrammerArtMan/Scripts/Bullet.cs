using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public int damage = 10;
    public float knockback = 10;

    // Directly taken from meleeAttack.cs Should probably be changed to use the same code instead of copy paste
    void OnTriggerEnter2D(Collider2D collision)
    {
        Helpers.HitPlayer(damage, gameObject, collision.gameObject, knockback);
        if (!collision.gameObject.tag.Equals("Bullet") && !collision.tag.Equals("ObjectSpawner"))
        {
            Destroy(gameObject);
        }


    }

    // Start is called before the first frame update
    void Start()
    {
        speed = 10;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void Update()
    {
        // The if clause is directly taken from diesOutOfBounds 
        if (Helpers.isOutOfArena(gameObject))
        {
            Destroy(gameObject);
        }
    }
}
