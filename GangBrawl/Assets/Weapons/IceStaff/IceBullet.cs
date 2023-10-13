using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceBullet : Bullet
{
    // Start is called before the first frame update
    public GameObject IceWall;
        void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag.Equals("Platform")){
            if(collision.gameObject.name.Contains("IceWall")){
                collision.gameObject.transform.localScale=new Vector3(collision.gameObject.transform.localScale.x,collision.gameObject.transform.localScale.y*1.2f,collision.gameObject.transform.localScale.z);
            }else{
                var wall = Instantiate(IceWall); 
                wall.transform.position=this.transform.position;  
            }
        }
        if(collision.tag.Equals("Player")){
            //Maybe move speed control to funtion and raise it after time with invoke?
            CharacterControl characterController=collision.gameObject.GetComponent<CharacterControl>();
            characterController.speed=Mathf.Clamp(characterController.speed-1,1,characterController.maxMovementSpeed);
        }
        Helpers.HitPlayer(damage,  collision.gameObject,rb.velocity.normalized*knockback, owner);
        if (!collision.gameObject.tag.Equals("Bullet") && !collision.tag.Equals("ObjectSpawner"))
        {
            Destroy(gameObject);
        }



        
    }
    void Start()
    {
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