using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
     AudioMixer audioMixer;
    public AudioSource music;
    public AudioSource Sounds;
    public AudioSource Dialogue;
    public AudioSource Ambiant;
    public static AudioManager instance;
    public List<AudioClip> SoundTrack;
    void Awake(){
        if(instance!=null){
            Destroy(this.gameObject);
        }else{
            instance=this;
        }
    }
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        //music=GetComponent<AudioSource>();
    }
    public void playSoundAtPoint(AudioClip audio,Vector3 pos){
        Sounds.transform.position=pos;
        Sounds.PlayOneShot(audio);
    }
    public void playDialogueAtPoint(AudioClip audio,Vector3 pos){
      Dialogue.transform.position=pos;
        Dialogue.PlayOneShot(audio);
    }
    public void changeMusic(int key){
        music.clip=SoundTrack[key];
        music.Play();
        music.loop=true;
    }
    public void StartAmbiant(AudioClip ambiant)
    {
        Ambiant.clip = ambiant;
        Ambiant.Play();
        Ambiant.loop = true;
    }
    public void StopAmbiant()
    {
        Ambiant.Stop();
    }
        // Update is called once per frame
        void Update()
    {
        
    }
}
