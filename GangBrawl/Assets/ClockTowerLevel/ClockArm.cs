using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClockArm : MonoBehaviour
{
    public Vector3 spinSpeed;
    private Vector3 CurrentSpinSpeed;
    // Start is called before the first frame update
     void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ArmCollided");
        if(collision.gameObject.tag=="Bullet"){
            CurrentSpinSpeed.z=-Vector2.Dot(transform.up,collision.gameObject.transform.up)*100;
        }
    }

    void Start()
    {
        CurrentSpinSpeed=spinSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        CurrentSpinSpeed.z=Mathf.Lerp(CurrentSpinSpeed.z,spinSpeed.z,0.003f);
        transform.Rotate(CurrentSpinSpeed*Time.deltaTime);
    }
}
