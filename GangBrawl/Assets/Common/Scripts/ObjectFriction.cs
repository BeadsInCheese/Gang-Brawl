using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFriction : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D rigidbody2d;
    public float friction=1;
    bool grounded=true;
    void Start()
    {
        rigidbody2d=this.GetComponent<Rigidbody2D>();
    }
        void OnCollisionEnter2D(Collision2D theCollision)
    {

        grounded=true;
    }
    void OnCollisionExit2D(Collision2D theCollision)
    {
        grounded=false;
    }
    // Update is called once per frame
    void Update()
    {
        Vector2 vel=rigidbody2d.velocity;
        if(grounded){        
        if(Mathf.Abs(vel.x)<1){
            vel.x=0;
            }else{
                vel.x-=Mathf.Sign(vel.x)*friction;
            }

                rigidbody2d.velocity=vel;
        }
    }
}
