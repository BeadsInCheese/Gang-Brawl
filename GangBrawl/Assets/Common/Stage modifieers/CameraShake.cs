using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 position;
    public float shakeAmount=1;
    void Start()
    {
        position=transform.position;
    }
    // Update is called once per frame
    void Update()
    {
     if(Earthquake.shaking){
        this.transform.position=position+new Vector3(UnityEngine.Random.Range(-100*shakeAmount,100*shakeAmount),Random.Range(-100*shakeAmount,100*shakeAmount));
     }else{
        transform.position=position;
     }
    }
}
