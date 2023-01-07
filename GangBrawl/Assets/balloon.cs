using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class balloon : MonoBehaviour
{
    public GameObject Pop;
    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D col){
        if( col.gameObject.tag.Equals("Bullet")){
            Instantiate(Pop).transform.position=this.transform.position;
            Destroy(gameObject);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
