using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {

    public Slider MusicVolume;
    public AudioSource backGroundMusic;
    public Slider effectVolume;
    public AudioSource[] sfxAudioSource;


    void Start()
    {
        MusicVolume.value = PlayerPrefs.GetFloat("MusicVolume", 1);
        effectVolume.value = PlayerPrefs.GetFloat("SFXVolume", 1);
    }
    // Update is called once per frame
    void Update ()
    {
        backGroundMusic.volume = MusicVolume.value;

        for (int i = 0; i < sfxAudioSource.Length; i++)
        {
            sfxAudioSource[i].volume = effectVolume.value;
        }
	}

    public void Low()
    {
        QualitySettings.SetQualityLevel(0);
    }
    public void Medium()
    {
        QualitySettings.SetQualityLevel(2); //2 or 3
    }
    public void High()
    {
        QualitySettings.SetQualityLevel(5); // 4 for beautiful and 5 for fantastic
    }

    public void saveOptions()
    {
        //called when close options button is pressed
        PlayerPrefs.SetFloat("MusicVolume", MusicVolume.value);
        PlayerPrefs.SetFloat("SFXVolume", effectVolume.value);
        // PlayerPrefs.SetInt("quality", QualitySettings.GetQualityLevel());
    }
}
