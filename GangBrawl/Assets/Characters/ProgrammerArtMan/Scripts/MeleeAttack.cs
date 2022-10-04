using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public int damage=10;
    public float knockback=10;
    // Start is called before the first frame update

    void OnTriggerEnter2D(Collider2D collision){
        GameObject col = collision.gameObject;
        if(col.tag.Equals("Player")){
            col.GetComponent<HPSystem>().takeDamage(damage);
            col.GetComponent<Rigidbody2D>().AddForce(this.transform.rotation.eulerAngles.y!=0?new Vector2(knockback,knockback) : new Vector2(-knockback,knockback));
        }


    }
}
