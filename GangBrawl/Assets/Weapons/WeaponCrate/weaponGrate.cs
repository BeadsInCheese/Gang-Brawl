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
    [ClientRpc]
    public void GunPickedUpClientRpc(int index,string target)
    {
        Debug.Log(GameObject.Find(target+ "/ FactionDownscaled"));/// bone_1 / bone_2 / bone_3 / ShootingPoint");
        var p= GameObject.Find(target).transform.Find("FactionDownscaled / bone_1 / bone_2 / bone_3 /ShootinPoint");
        var g = p.GetComponent<Shoot>();
        g.gun=Instantiate(guntemplates[index], p.transform.position, p.transform.rotation);
        g.newGunSetup();

    }
    void OnCollisionEnter2D(Collision2D col){
        if(col.gameObject.tag.Equals("Player") &&  NetworkManager.Singleton.IsHost)
        {
            if (crunchSounds.Count > 0)
            {
                AudioManager.instance.playSoundAtPoint(crunchSounds[(int)Random.Range(0, crunchSounds.Count)], transform.position);
                var x=Instantiate(exp);
                x.transform.position = transform.position;
            }
            Shoot s= col.gameObject.GetComponentInChildren<Shoot>();
            if(s.gun!=null){
                
                s.gun.transform.parent.GetComponent<NetworkObject>().Despawn();
            }

            int index = (int)Random.Range(0, guntemplates.Count);
            s.gun=Instantiate(guntemplates[index],s.shootingArm.position,s.shootingArm.rotation);
            s.gun.GetComponent<NetworkObject>().SpawnWithOwnership(col.gameObject.GetComponent<NetworkObject>().OwnerClientId);
            s.newGunSetup();


            //GunPickedUpClientRpc(index,s.body.parent.parent.gameObject.name);
            transform.parent.gameObject.GetComponent<NetworkObject>().Despawn();
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
