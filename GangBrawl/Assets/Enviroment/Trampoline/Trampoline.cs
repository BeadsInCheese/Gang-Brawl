using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    // Start is called before the first frame update   
     private void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("Jump");
        if(col.gameObject.tag=="Player"){
            col.gameObject.GetComponent<CharacterControl>().jump();
        }
    }
}
