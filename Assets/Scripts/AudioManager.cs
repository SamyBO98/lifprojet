﻿using UnityEngine.Audio;
using System;
using UnityEngine;

//Script permettant de gérer le son. On créer un tableau d'AudioSource et grâce à certains tags de musique on peut jouer la musique souhaitée

public class AudioManager : MonoBehaviour
{
   public Sound[] sounds;
    public static AudioManager instance;
    
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
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
   

    void Start ()
    {   
        Play("bgmcalme");
        Play("jeu");
        Play("night");     
    }


    public void Play (string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            return;

        }
        s.source.Play();

    }
  
    public void StopPlaying(string sound)
   {
        Sound s = Array.Find(sounds, item => item.name == sound);
        if (s == null)
        {
            return;
       }

        s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
        s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

        s.source.Stop();
    }


}
