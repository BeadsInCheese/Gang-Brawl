using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    enum audioChannels
    {
        Master,Ambiant,Music,SFX

    }
    [SerializeField] audioChannels channel;
    [SerializeField] Slider volumeSlider;
    [SerializeField]
    AudioMixer audioMixer;

    void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
    }
    public float DecibelToLinear(float decibel)
    {
        return Mathf.Pow(10f, decibel / 20f);
    }
    public float LinearToDecibel(float linear)
    {
        return linear>0?20f * Mathf.Log10(linear):-80;
    }
    public void ChangeVolume()
    {
        audioMixer.SetFloat( channel.ToString()+"Vol", LinearToDecibel(volumeSlider.value  ));
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetFloat(channel.ToString()+"VolumeValue", volumeSlider.value);
    }

    void Load()
    {
        volumeSlider.value = PlayerPrefs.GetFloat(channel.ToString()+"VolumeValue");
    }
}
