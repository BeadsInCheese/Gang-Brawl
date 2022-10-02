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
    public float LightAttackCancelTime=0;
    public float HeavyAttackCancelTime=0.3f;
    private float ConcurrentAttackCancelTime=0;
    private int attackBuffer=0;

    public GameObject HeavyAttackHitbox;
    public GameObject LightAttackHitbox;
    public float maxFallSpeed=30;
    public float minFallSpeed=-300;



    Vector2 vel=new Vector2(0,0);
    PlayerInput playerInput;
    bool spriteFlipped=false;
    Animator animationControl;

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
        animationControl=this.transform.GetChild(0).GetComponent<Animator>();
        playerInput=GetComponent<PlayerInput>();
        physicsBody=GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    void Update()
    {

        //Attack Control

        if(ConcurrentAttackCancelTime<=0){
                animationControl.SetBool("Attacking",false);
                animationControl.SetBool("HeavyAttacking",false);
                HeavyAttackHitbox.SetActive(false);
                LightAttackHitbox.SetActive(false);
                ConcurrentAttackCancelTime=-1;
        }else{
            ConcurrentAttackCancelTime-=Time.deltaTime;
        }

        if(playerInput.actions["HeavyAttack"].triggered||attackBuffer==1){
            if(ConcurrentAttackCancelTime<-0.5){

                animationControl.SetBool("HeavyAttacking",true);
                ConcurrentAttackCancelTime=HeavyAttackCancelTime;
                HeavyAttackHitbox.SetActive(true);
                attackBuffer=0;
            }else{
                attackBuffer=1;
            }
            }
        if(playerInput.actions["LightAttack"].triggered||attackBuffer==2){
            if(ConcurrentAttackCancelTime<-0.5){
                
                animationControl.SetBool("Attacking",true);
                ConcurrentAttackCancelTime=LightAttackCancelTime;
                LightAttackHitbox.SetActive(true);
                attackBuffer=0;
            }else{
                attackBuffer=2;
            }
        }






        if(playerInput.actions["Jump"].triggered){
            if(isgrounded){
                physicsBody.AddForce(new Vector2(0,jumpHeight));
            }else if(doubleJump){
                physicsBody.AddForce(new Vector2(0,jumpHeight));
                doubleJump=false;
            }
            

        }
        
        vel.y=Mathf.Clamp(physicsBody.velocity.y,minFallSpeed,maxFallSpeed);
        if(isgrounded){
            animationControl.SetBool("OnGround",true);
            vel.x=playerInput.actions["Walk"].ReadValue<float>()*speed;
        }else{
            animationControl.SetBool("OnGround",false);
            vel.x=Mathf.Max(Mathf.Min(vel.x+playerInput.actions["Walk"].ReadValue<float>(),speed),-speed);
            
        }
        animationControl.SetBool("Moving",vel.x!=0);
        if(vel.x!=0){
            spriteFlipped = vel.x>0;
        }
        transform.rotation=Quaternion.Euler(new Vector3(0f,spriteFlipped ? 180:0f,0f));
        physicsBody.velocity=vel;
    }
}
