using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
public class GameStarCountDown : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource audioSource;
    public TMPro.TextMeshProUGUI tex;
    private long getMS(){
        return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;


    } 
    public AudioClip countdown;
    private AudioClip theme;
    bool loaded=false;
    bool secondFrame=true;
    void Start()
    {
        audioSource=Camera.main.gameObject.GetComponent<AudioSource>();
        theme=audioSource.clip;
        audioSource.clip=countdown;

    }

    // Update is called once per frame
    long t1=0;
    long t2=0;
    public 
    void Update()
    {
        if(loaded){
            tex.text=""+(3-(MathF.Round(((t2-t1)*1.0f/1000)/1.7f)));
            t2=getMS();
                if(secondFrame){
                    secondFrame=false;
                    audioSource.Play();
                    Time.timeScale=0;
                    t1=getMS();
                }
            if(t2-t1>5000){

                audioSource.clip=theme;
                audioSource.loop=true;
                audioSource.Play();
                Time.timeScale=1;
                Destroy(gameObject);
            }
        }else{
            loaded=true;
        }
        
    }
}
