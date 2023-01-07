using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterAnimationEnd : MonoBehaviour
{
    // Start is called before the first frame update
    public float animationLen;
    void Start()
    {
        Destroy(transform.gameObject,animationLen);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
