using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSong : MonoBehaviour
{
    AudioSource SoundTrack;
    public AudioClip[] soundtracks;

    // Use this for initialization
    void Start()
    {
        SoundTrack = GetComponent<AudioSource>();
        if (!SoundTrack.playOnAwake)
        {
            SoundTrack.clip = soundtracks[Random.Range(0, soundtracks.Length)];
            SoundTrack.Play();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!SoundTrack.isPlaying)
        {
            SoundTrack.clip = soundtracks[Random.Range(0, soundtracks.Length)];
            SoundTrack.Play();
        }
    }
}
