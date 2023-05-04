using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicLoader : MonoBehaviour
{
    // Start is called before the first frame update
    public int key = 0;
    public AudioClip ambiant;
    void Start()
    {
        AudioManager.instance.changeMusic(key);
        if (ambiant != null)
        {
            AudioManager.instance.StartAmbiant(ambiant);
        }
        else
        {
            AudioManager.instance.StopAmbiant();
        }
    }


}
