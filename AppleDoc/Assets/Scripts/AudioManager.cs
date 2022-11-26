using UnityEngine;
using System;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public Sound[] audioClips;
    public bool playBgm = true;

    private void Awake()
    {
        foreach(Sound i in audioClips)
        {
            i.source = gameObject.AddComponent<AudioSource>();
            i.source.clip = i.clip;
            i.source.volume = i.volume;
            i.source.pitch = i.pitch;
        }
    }

    private void Start()
    {
        if (playBgm)
        {
            Play("bg1", true);
        }
    }

    public void Play(string name)
    {
        Sound sound = Array.Find(audioClips, i => i.name == name);
        sound.source.Play();
    }

    public void Play(string name, bool loop)
    {
        Sound sound = Array.Find(audioClips, i => i.name == name);
        sound.source.loop = loop;
        sound.source.Play();
    }

    public void Play(string name, float pitch)
    {
        Sound sound = Array.Find(audioClips, i => i.name == name);
        sound.source.pitch = pitch;
        sound.source.Play();
    }

    public void Play(string name, float pitch, AudioClip clip)
    {
        Sound sound = Array.Find(audioClips, i => i.name == name);
        sound.source.clip = clip;
        sound.source.pitch = pitch;
        sound.source.volume = 0.5f;
        sound.source.Play();
    }
}
