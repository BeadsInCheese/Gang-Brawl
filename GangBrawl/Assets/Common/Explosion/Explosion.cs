using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    public int damage=10;
    public int knockback=10;
    CircleCollider2D aoe;
    public string owner="";
    // Start is called before the first frame update
    public void Dissipate(){
       Destroy(gameObject);
    }
    public AudioClip sound;
    void Start(){
        aoe=GetComponent<CircleCollider2D>();
        Invoke("Dissipate",0.5f);
        if(Time.timeScale!=0){
        AudioManager.instance.playSoundAtPoint(sound,transform.position);
        StartCoroutine(CameraShake.Instance.ScreenShake(0.3f,0.3f));
    }}
    
    public void OnTriggerEnter2D(Collider2D col){
        if(col.gameObject.tag.Equals("Player")){
            if (owner.Length>0&&col.gameObject.GetComponent<HPSystem>().currentHp - damage <= 0 && !col.gameObject.GetComponent<HPSystem>().dead)
            {
                DirectorBehaviour.PlayerKills[owner] += 1;
                DirectorBehaviour.TestAndSetGoldenSpiritLead(col.gameObject.name, owner);
            }
            Vector2 dir=(this.transform.position-col.gameObject.transform.position);
            Helpers.HitPlayer((int)(damage),col.gameObject,knockback*-dir.normalized);
            
        }
    }
}
