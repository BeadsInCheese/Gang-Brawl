using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D physicsBody;

    float speed=6f;
    float jumpHeight=400;
    Vector2 vel=new Vector2(0,0);
    PlayerInput playerInput;
    bool spriteFlipped=false;


    //jump Control
   bool isgrounded=false;
   bool doubleJump=true;

 void OnCollisionEnter2D(Collision2D theCollision)
 {
     if (theCollision.gameObject.tag == "Platform")
     {
        doubleJump=true;
         isgrounded = true;
     }
 }
 
 void OnCollisionExit2D(Collision2D theCollision)
 {
     if (theCollision.gameObject.tag == "Platform")
     {
         isgrounded = false;
     }
 }

    void Start()
    {
        playerInput=GetComponent<PlayerInput>();
        physicsBody=GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInput.actions["Jump"].triggered){
            if(isgrounded){
                physicsBody.AddForce(new Vector2(0,jumpHeight));
            }else if(doubleJump){
                physicsBody.AddForce(new Vector2(0,jumpHeight));
                doubleJump=false;
            }
            

        }
        
        vel.y=physicsBody.velocity.y;
        if(isgrounded){
            vel.x=playerInput.actions["Walk"].ReadValue<float>()*speed;
        }else{
            vel.x=Mathf.Max(Mathf.Min(vel.x+playerInput.actions["Walk"].ReadValue<float>(),speed),-speed);
        }
        if(vel.x!=0){
            spriteFlipped = vel.x>0;
        }
        transform.rotation=Quaternion.Euler(new Vector3(0f,spriteFlipped ? 180:0f,0f));
        physicsBody.velocity=vel;
    }
}
