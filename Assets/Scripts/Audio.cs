using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Audio
{
    public string _name;
    public AudioClip _clip;

    public bool _playOnAwake;
    public bool _loop;

    [Range(0f, 3f)]
    public float _volume = 1;

    [HideInInspector]
    public AudioSource audioSource;
}  
