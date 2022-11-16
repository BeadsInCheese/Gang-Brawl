using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float spread=0;
    public float cooldown=2;
    public int ammo=5;
    public Transform barrel;
    public int bulletsShotAtOnce=5; 
    public GameObject BulletPrefab;
    public bool automatic=false;
    public float recoil=5;
    public float damage=17;
    public float bulletKnockback=5;
    public GameObject muzzleflash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
