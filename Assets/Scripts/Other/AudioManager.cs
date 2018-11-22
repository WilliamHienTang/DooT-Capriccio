﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    public static AudioManager instance;
    public AudioMixer audioMixer;
    string currentBGM;

    // Use this for initialization
    void Awake () {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

		foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.outputAudioMixerGroup = s.group;
        }
	}

    // Init audio mixer
    void Start()
    {
        audioMixer.SetFloat(Constants.BGMVolume, PlayerPrefs.GetFloat(Constants.BGMVolume));
        audioMixer.SetFloat(Constants.SFXVolume, PlayerPrefs.GetFloat(Constants.SFXVolume));
        audioMixer.SetFloat(Constants.songVolume, PlayerPrefs.GetFloat(Constants.songVolume));
        audioMixer.SetFloat(Constants.gameSFXVolume, PlayerPrefs.GetFloat(Constants.gameSFXVolume));
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.Play();

        if (s.loop)
        {
            currentBGM = name;
        }
    }

    public void PlayScheduled(string name, float time)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;
        }
        s.source.PlayScheduled(time);

        if (s.loop)
        {
            currentBGM = name;
        }
    }

    public float GetClipLength(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return 0.0f;
        }
        return s.clip.length;
    }

    public void Pause(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Audio: " + name + " not found");
            return;
        }
        s.source.Pause();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Audio: " + name + " not found");
            return;
        }
        s.source.Stop();
        currentBGM = null;
    }

    public string GetCurrentBGM()
    {
        return currentBGM;
    }
}
