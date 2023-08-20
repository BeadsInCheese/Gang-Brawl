using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    public int damage = 10;
    public float knockback = 10;
    // Start is called before the first frame update
    public AudioClip punchSound;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player")){
            AudioManager.instance.Sounds.pitch = Random.Range(0.8f, 1.2f);
            AudioManager.instance.playSoundAtPoint(punchSound, this.transform.position);
            AudioManager.instance.Sounds.pitch = 1;
            var p=collision.gameObject;
            if(p.GetComponent<HPSystem>().currentHp-damage<=0&&!p.GetComponent<HPSystem>().dead){
                DirectorBehaviour.PlayerKills[transform.parent.gameObject.name]+=1;
                //Debug.Log(transform.parent.gameObject.name+" has "+DirectorBehaviour.PlayerKills[transform.parent.gameObject.name]+ " kills.");
            }
        
        }
        Helpers.HitPlayer(damage, gameObject, collision.gameObject, knockback);
    }
}
