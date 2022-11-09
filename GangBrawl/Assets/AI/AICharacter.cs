using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacter : CharacterControl
{
    Vector2 target;
    public float proximityTreshold;
    private float xin=0;
    public int index=3;
    private BTTree AI=new BTTree();
    Node.Status GoToTarget(){
        if(Mathf.Abs(transform.position.x-target.x)>proximityTreshold){
            if(this.physicsBody.velocity.y<-20 &&target.y>transform.position.y){
                TryToJump();
            }
            xin=Mathf.Sign(target.x-transform.position.x)*speed;
            return Node.Status.RUNNING;
        }else if(Mathf.Abs(transform.position.y-target.y)>0.4f){
            xin=0;
            TryToJump();
            return Node.Status.RUNNING;
        }else{
            xin=0;
            return Node.Status.SUCCESS;
        }

    }
    Node.Status HeavyAttack(){
            if (ConcurrentAttackCancelTime < -0.5)
            {

                animationControl.SetBool("HeavyAttacking", true);
                ConcurrentAttackCancelTime = HeavyAttackCancelTime;
                HeavyAttackHitbox.SetActive(true);
                attackBuffer = 0;
            }
            else
            {
                attackBuffer = 1;
            }
            return Node.Status.SUCCESS;
    }
    Node.Status LightAttack(){
            if (ConcurrentAttackCancelTime < -0.5)
            {

                animationControl.SetBool("Attacking", true);
                ConcurrentAttackCancelTime = LightAttackCancelTime;
                LightAttackHitbox.SetActive(true);
                attackBuffer = 0;
            }
            else
            {
                attackBuffer = 2;
            }
            return Node.Status.SUCCESS;

    }
    Node.Status TryToJump(){
        
            if (isgrounded)
            {
                jump();
            }
            else if (doubleJump)
            {
                jump();
                doubleJump = false;
            }
            else
            {
                return Node.Status.FAILURE;
            }
            return Node.Status.SUCCESS;
        
    }
    // Start is called before the first frame update
     void Start()
    {
        animationControl = this.transform.GetChild(0).GetComponent<Animator>();
        physicsBody = GetComponent<Rigidbody2D>();
        transform.position = GameObject.Find("Player-" + index + "-SpawnPoint").transform.position;
        colliderDims=GetComponent<BoxCollider2D>().size;
        SequenceNode seekAndDestroy=new SequenceNode("seekAndDestroy");
        LeafNode moveTotarget=new LeafNode("Move to target",GoToTarget);
        LeafNode Attack=new LeafNode("Attack",LightAttack);
        seekAndDestroy.AddChild(moveTotarget);
        seekAndDestroy.AddChild(Attack);

        AI.AddChild(seekAndDestroy);

    }

    // Update is called once per frame
    private void updateTarget(){

        foreach(var i in DirectorBehaviour.PlayersAlive.Keys){
            if(!i.Equals(this.gameObject.name)&&DirectorBehaviour.PlayersAlive[i]>0){
                target=GameObject.Find(i).transform.position;
                break;
            }
        }
        
    }
    float targetUpdateCooldown=2;
    void Update()
    {
    updateTarget();
    
    targetUpdateCooldown-=Time.deltaTime;
     AI.Process();
        if (ConcurrentAttackCancelTime <= 0)
        {
            animationControl.SetBool("Attacking", false);
            animationControl.SetBool("HeavyAttacking", false);
            HeavyAttackHitbox.SetActive(false);
            LightAttackHitbox.SetActive(false);
            ConcurrentAttackCancelTime = -1;
        }
        else
        {
            ConcurrentAttackCancelTime -= Time.deltaTime;
        }



        

        //if (playerInput.actions["Jump"].WasReleasedThisFrame())
        //{
        //    if (physicsBody.velocity.y > 0)
        //    {
        //        applyJumpReleasedEarlyModifier();
        //    }
            //jumpButtonDown = false;
        //}
        vel.y = physicsBody.velocity.y;
        //float xin= playerInput.actions["Walk"].ReadValue<float>() * speed;
            vel.x=physicsBody.velocity.x;
            if(vel.x-speed<=0 && xin>=0){
                vel.x+=xin*Time.deltaTime*speed;
            }
            else if(vel.x+speed>=0 && xin<=0){
                vel.x+=xin*Time.deltaTime*speed;
            }
            if(vel.x!=0&&xin==0){
                if(Mathf.Abs(vel.x)<=1){
                    vel.x=0;
                }else{
                    vel.x-=Mathf.Sign(vel.x)*friction;
                }
            }
        if (isgrounded)
        {
            animationControl.SetBool("OnGround", true);
            
        }
        else
        {
            animationControl.SetBool("OnGround", false);

            float apexPoint = Mathf.InverseLerp(jumpApexThreshold, 0, Mathf.Abs(vel.y));
            float apexBonus = Mathf.Abs(xin) > 0 ? Mathf.Sign(xin) * _apexBonus * apexPoint : 0;
            
            //vel.x = speed * xin + apexBonus * Time.deltaTime;
            vel.y += Mathf.Sign(vel.y) * fallSpeedAtApex * (0.1f + apexPoint) * Time.deltaTime;
            vel.y = Mathf.Clamp(vel.y, maxFallSpeed, minFallSpeed);
        }
        animationControl.SetBool("Moving", xin != 0);
        //Debug.Log(spriteFlipped);
        // Character is moving right
        if (xin > 0 )
        {
            // Change sprite to right'
            spriteFlipped = true;
        }
        if (xin < 0)
        {
            // Character is moving left
            spriteFlipped = false;

        }
        else
        {
            // DO NOTHING, because
            // character is still, keep the orientation of the character

        }

        transform.rotation = Quaternion.Euler(new Vector3(0f, spriteFlipped ? 180 : 0f, 0f));
        physicsBody.velocity = vel;
       
    }
}
