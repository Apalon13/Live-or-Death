using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public string currentBackgroundTheme = "Theme";

    public static AudioManager instance;

    private void Awake()
    {
        SingletonInit();

        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
        }

        Play("Theme");
    }

    public void Play(string name)
    {
        Sound selectedSound = Array.Find(sounds, sound => sound.name == name);

        if (selectedSound == null)
        {
            Debug.Log("Warning: Sound" + name + " not found!");

            return;
        }

        if (selectedSound.loop)
        {
            currentBackgroundTheme = selectedSound.name;
        }

        selectedSound.source.Play();

        Debug.Log(selectedSound.name);
    }

    public void Stop(string name)
    {
        Sound selectedSound = Array.Find(sounds, sound => sound.name == name);

        if (selectedSound == null)
        {
            Debug.Log("Warning: Sound" + name + " not found!");

            return;
        }

        selectedSound.source.Stop();
    }

    public void TurnOffTheSounds()
    {
        foreach (Sound sound in sounds)
        {
            if (!sound.loop)
            {
                sound.source.volume = 0;
            }
        }
    }

    public void TurnOffTheTheme()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.loop)
            {
                sound.source.volume = 0;
            }
        }
    }

    public void TurnOnTheSounds()
    {
        foreach (Sound sound in sounds)
        {
            if (!sound.loop)
            {
                sound.source.volume = sound.volume;
            }
        }
    }

    public void TurnOnTheTheme()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.loop)
            {
                sound.source.volume = sound.volume;
            }
        }
    }

    private void SingletonInit()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
}
