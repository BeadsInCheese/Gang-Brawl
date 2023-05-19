using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiesOnOutOfBounds : MonoBehaviour
{
    // Start is called before the first frame update

    HPSystem hpSystem;
    public AudioClip sound;
    void Start()
    {
        hpSystem = GetComponent<HPSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Helpers.isOutOfArena(gameObject))
        {
            hpSystem.die();
            if(sound!=null){
                AudioManager.instance.playSoundAtPoint(sound,transform.position);
            }
        }
    }
}
