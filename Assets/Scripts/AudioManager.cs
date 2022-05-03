using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Audio[] audios;

    public static AudioManager audioManager;

    private void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        // 3)
        foreach (Audio audio in audios)
        {
            audio.audioSource = gameObject.AddComponent<AudioSource>();

            // 4)
            audio.audioSource.name = audio._name;
            audio.audioSource.clip = audio._clip;
            audio.audioSource.volume = audio._volume;
            audio.audioSource.loop = audio._loop;
            audio.audioSource.playOnAwake = audio._playOnAwake;
        }
    }

    private void Start()
    {
        // 5)
        PlayAudio("Main");
    }

    public void PlayAudio(string name)
    {
        // 6) 
        foreach (Audio audio in audios)
        {
            if (audio._name == name)
            {
                if (!audio.audioSource.isPlaying)
                {
                    audio.audioSource.Play();
                }
            }
        }
    }

    public void Mute()
    {
        foreach (Audio audio in audios)
        {
            audio.audioSource.volume = 0;
        }
    }

}