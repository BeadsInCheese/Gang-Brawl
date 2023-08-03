
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatAnim : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 basepos;
    public float amount = 2;
    void Start()
    {
        basepos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition =new Vector3(transform.localPosition.x,basepos.y + amount*Mathf.Sin(Time.time),transform.localPosition.z);
    }
}
