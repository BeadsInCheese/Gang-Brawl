using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
[SerializeField]
public class weaponGrate : NetworkBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> guntemplates=new List<GameObject>();
    public List<AudioClip> crunchSounds;
    public GameObject exp;
    void Start()
    {
        DirectorBehaviour.items.Add(this.gameObject);
    }
     void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag.Equals("Player")){
            if (crunchSounds.Count > 0)
            {
                AudioManager.instance.playSoundAtPoint(crunchSounds[(int)Random.Range(0, crunchSounds.Count)], transform.position);
                var x=Instantiate(exp);
                x.transform.position = transform.position;
            }
            Shoot s= col.gameObject.GetComponentInChildren<Shoot>();
            if(s.gun!=null){
                Destroy(s.gun);
            }
            s.gun=Instantiate(guntemplates[(int)Random.Range(0,guntemplates.Count)],s.shootingArm.position,s.shootingArm.rotation);
            s.newGunSetup();
            Destroy(transform.parent.gameObject);
        }

     }
    override
    public void OnDestroy()
    {

        DirectorBehaviour.items.Remove(this.gameObject);
        base.OnDestroy();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
