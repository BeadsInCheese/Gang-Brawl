using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }
    void OnTriggerEnter2D(Collider2D col){
        if( col.gameObject.tag.Equals("Bullet")){
            Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
