using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGas : MonoBehaviour
{
    public string owner = "";
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {

            Debug.Log("poison");
            var target = collision.gameObject.GetComponent<HPSystem>();
            target.poisoned=true;
            target.poisoner = owner;
        }
    }
}
