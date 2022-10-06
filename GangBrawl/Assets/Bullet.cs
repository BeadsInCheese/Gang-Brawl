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
        GameObject col = collision.gameObject;
        if (col.tag.Equals("Player"))
        {
            col.GetComponent<HPSystem>().takeDamage(damage);
            col.GetComponent<Rigidbody2D>().AddForce(this.transform.rotation.eulerAngles.y != 0 ? new Vector2(knockback, knockback) : new Vector2(-knockback, knockback));
        }
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
        if (this.transform.position.y < ArenaDataManager.instance.arenaLowerBound || Mathf.Abs(this.transform.position.x) > ArenaDataManager.instance.arenaSideBound)
        {
            Destroy(gameObject);
        }
    }
}
