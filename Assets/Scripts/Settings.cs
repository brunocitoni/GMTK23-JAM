using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;

    void Start()
    {

    }


    public void SFXVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }


    public void MusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

}
