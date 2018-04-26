using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSound : MonoBehaviour
{
    AudioSource Sound;
    public AudioClip[] objectPlaceSounds;
    public AudioClip[] concreteSounds;
    public AudioClip[] glassSounds;
    public AudioClip[] wireSounds;
    public AudioClip[] woodSounds;
    public AudioClip[] buildingSounds;

    private static RandomSound instance = null;
    public static RandomSound GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
        Sound = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    public void PlaceSoundPlay()
    {
        if (!Sound.isPlaying)
        {
            Sound.clip = objectPlaceSounds[Random.Range(0, objectPlaceSounds.Length)];
            Sound.Play();
        }
    }

        // Update is called once per frame
        public void PlaceConcreteSoundPlay()
        {

            Sound.clip = concreteSounds[Random.Range(0, concreteSounds.Length)];
            Sound.Play();

        }
        public void PlaceGlassSoundPlay()
        {

            Sound.clip = glassSounds[Random.Range(0, glassSounds.Length)];
            Sound.Play();

        }
        public void PlaceWireSoundPlay()
        {

            Sound.clip = wireSounds[Random.Range(0, wireSounds.Length)];
            Sound.Play();

        }
        public void PlaceWoodenSoundPlay()
        {

            Sound.clip = woodSounds[Random.Range(0, woodSounds.Length)];
            Sound.Play();

        }
        public void PlaceBuildSoundPlay(int num) //0 to 2 (only 3 sounds)
        {
        Sound.clip = buildingSounds[num];
        Sound.Play();
        Debug.Log(num);
        }
}

