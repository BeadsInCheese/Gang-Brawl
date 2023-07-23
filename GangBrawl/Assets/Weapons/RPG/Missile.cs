using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Bullet
{
    // Start is called before the first frame update
    GameObject target = null;
    public float max_velocity = 5;
    public float max_force = 5;
    public float mass = 5;
    public float max_speed = 5;
    public float activateTime=0;
    bool active = false;
    public GameObject explosion;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        StartCoroutine(activate(activateTime));
    }
    IEnumerator activate(float time)
    {
        yield return new WaitForSeconds(time);
        active = true;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!active) { return; }
        if (collision.gameObject.tag.Equals("Player") || collision.gameObject.tag.Equals("ExplosivesBarrel"))
        {
            var ex = Instantiate(explosion);
            ex.transform.position = new Vector2(transform.position.x, transform.position.y);
        }
        else if (!collision.tag.Equals("ObjectSpawner"))
        {

            var ex = Instantiate(explosion);
            ex.transform.position = new Vector2(transform.position.x, transform.position.y);
            Destroy(gameObject);
        }
        Helpers.HitPlayer(damage, collision.gameObject, rb.velocity.normalized * knockback);



    }


}
