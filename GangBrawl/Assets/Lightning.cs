using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour
{
    // Start is called before the first frame update
    float delay = 10;
    public AudioClip thunderSound;
    public IEnumerator playThunder()
    {
        yield return new WaitForSeconds(delay);
        AudioManager.instance.playSoundAtPoint(thunderSound, transform.position);
        StartCoroutine(playThunder());
    }
    void Start()
    {
        StartCoroutine(playThunder());    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
