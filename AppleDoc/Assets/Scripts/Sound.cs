using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(-3f, 3f)]
    public float pitch;
    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}
