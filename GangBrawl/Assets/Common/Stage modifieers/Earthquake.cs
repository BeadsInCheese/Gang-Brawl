using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earthquake : MonoBehaviour
{
    // Start is called before the first frame update
    public static bool shaking=false;
    float amplitude=2;
    void Start()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collider){
        
        if(shaking){
            collider.collider.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-100*amplitude,100*amplitude),100*amplitude));
        }
    }
    // Update is called once per frame

}
