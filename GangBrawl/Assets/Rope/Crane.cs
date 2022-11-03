using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour
{
    // Start is called before the first frame update
    public float xDistance=20;
    private float pos=0;
    public float speed=0.01f;
    public float step=1;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        pos+=speed;
        pos=pos%xDistance;
        this.transform.position=new Vector2(this.transform.position.x+Mathf.Sign((pos-(xDistance/2)))*step,transform.position.y);
        
    }
}
