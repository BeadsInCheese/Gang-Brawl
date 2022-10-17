using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponGrate : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> guntemplates=new List<GameObject>();
    void Start()
    {
    }
     void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag.Equals("Player")){
            Shoot s= col.gameObject.GetComponentInChildren<Shoot>();
            if(s.gun!=null){
                Destroy(s.gun);
            }
            s.gun=Instantiate(guntemplates[(int)Random.Range(0,guntemplates.Count)],s.shootingArm.position,s.shootingArm.rotation);
            s.newGunSetup();
            Destroy(transform.parent.gameObject);
        }

     }
    // Update is called once per frame
    void Update()
    {
        
    }
}
