using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlaySoundOnselect : MonoBehaviour, ISelectHandler
{
    public AudioClip s;
    public  AudioClip Click;
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener( playclick);
        Click = AudioManager.instance.clickAudio;
    }
    public void playclick()
    {
        AudioManager.instance.playSoundAtPoint(Click, new Vector3(0, 0, 0));
    }
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(this.gameObject.name + " was selected");
        AudioManager.instance.playSoundAtPoint(s,new Vector3(0,0,0));
    }
    }
