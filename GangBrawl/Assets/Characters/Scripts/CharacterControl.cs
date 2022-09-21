using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterControl : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody2D physicsBody;

    float speed=3f;
    float jumpHeight=400;
    Vector2 vel=new Vector2(0,0);
    PlayerInput playerInput;

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
        vel.x=playerInput.actions["Walk"].ReadValue<float>()*speed;
        physicsBody.AddRelativeForce(vel);
        

    }
}
