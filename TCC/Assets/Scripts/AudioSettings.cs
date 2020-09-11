using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class AudioSettings : MonoBehaviour
{
    EventInstance SFXVolumeTestEvent;

    Bus Music;
    Bus SFX;
    Bus Master;
    float musicVolume;
    float SFXVolume;
    float masterVolume;
    // Start is called before the first frame update
    void Awake()
    {
        Music = RuntimeManager.GetBus("bus:Master/Music");
        SFX = RuntimeManager.GetBus("bus:Master/SFX");
        Master = RuntimeManager.GetBus("bus:Master");
        SFXVolumeTestEvent = RuntimeManager.CreateInstance("event:Pickaxe");
    }

    // Update is called once per frame
    void Update()
    {
        Music.setVolume(musicVolume);
        SFX.setVolume(SFXVolume);
        Master.setVolume(masterVolume);
    }

    public void MusicVolumeLevel(float newVolume)
    {
        musicVolume = newVolume;
    }
    public void SFXVolumeLevel(float newVolume)
    {
        SFXVolume = newVolume;

        //Toca o som durante o ajuste de volume.
        PLAYBACK_STATE pbState;
        SFXVolumeTestEvent.getPlaybackState(out pbState);
        if (pbState != PLAYBACK_STATE.PLAYING)
            SFXVolumeTestEvent.start();
    }
    public void MasterVolumeLevel(float newVolume)
    {
        masterVolume = newVolume;
    }
}
