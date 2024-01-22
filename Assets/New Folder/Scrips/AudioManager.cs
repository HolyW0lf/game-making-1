using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);

        if (s != null && s.source != null)
        {
            s.source.Play();
            Debug.Log("Playing sound: " + sound);
        }
        else
        {
            Debug.LogWarning("Sound with name " + sound + " not found or AudioSource is null.");
        }
    }

    public void Stop(string sound)
    {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s != null && s.source != null)
        {
            s.source.Stop();
            Debug.Log("Stopping sound: " + sound);
        }
        else
        {
            Debug.LogWarning("Sound with name " + sound + " not found or AudioSource is null.");
        }
    }
}
