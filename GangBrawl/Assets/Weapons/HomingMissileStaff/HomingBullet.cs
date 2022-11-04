using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : Bullet
{
    // Start is called before the first frame update
    GameObject target=null;
    public float max_velocity=5;
    public float max_force=5;
    public float mass=5;
    public float max_speed=5;
    public GameObject SeekTarget(int density){
        for (int i=0; i<density; i++){
        float x=Mathf.Cos(i*2*Mathf.PI/density);
        float y=Mathf.Sin(i*2*Mathf.PI/density);
        var hit=Physics2D.Raycast(new Vector2(x,y),10*new Vector2(x,y));
        if(hit){
        if(hit.collider.gameObject.tag.Equals("Player")){
            return hit.collider.gameObject;
        }}
    }
    return null;
    }
    // Update is called once per frame
    void Update()
    {
        if(target==null){
            target=SeekTarget(100);
        }
        else{
            //Debug.Log(target);
            Vector2 desired_velocity = (target.transform.position - transform.position).normalized * max_velocity;
            Vector2 steering = desired_velocity - rb.velocity;

            steering = Vector2.ClampMagnitude (steering, max_force);
            steering = steering / mass;
 
            rb.velocity = Vector2.ClampMagnitude(rb.velocity + steering , max_speed);

             transform.right = rb.velocity;

        }

        // The if clause is directly taken from diesOutOfBounds 
        if (Helpers.isOutOfArena(gameObject))
        {
            Destroy(gameObject);
        }
    }
}
