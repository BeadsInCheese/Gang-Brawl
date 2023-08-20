using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasGrenade : Bullet
{
    // Start is called before the first frame update
    public float max_velocity = 5;
    public float max_force = 5;
    public float mass = 5;
    public float max_speed = 5;
    public float activateTime = 0;
    bool active = false;
    public GameObject gas;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        StartCoroutine(activate(activateTime));
        Destroy(gameObject, 10);
    }
    IEnumerator activate(float time)
    {
        yield return new WaitForSeconds(time);
        active = true;
    }
    float gasTimer=0;
    private void Update()
    {
        if (gasTimer <= 0)
        {
            var gasInstance = Instantiate(gas);
            gasInstance.transform.position = this.transform.position;
            gasInstance.GetComponent<PoisonGas>().owner = owner;
            gasTimer = 1;
        }
        gasTimer -= Time.deltaTime;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
