using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeVolume : MonoBehaviour {

    public Slider MusicVolume;
    public AudioSource backGroundMusic;
    public Slider effectVolume;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        backGroundMusic.volume = MusicVolume.value;
	}
}
