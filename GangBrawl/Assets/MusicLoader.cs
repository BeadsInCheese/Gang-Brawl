using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public int key = 0;
    void Start()
    {
        AudioManager.instance.changeMusic(key);        
    }


}
