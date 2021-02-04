using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class AudioSettings : MonoBehaviour
{
     [EventRef] public string menuMoveSound;
     [EventRef] public string menuConfirmSound;

     EventInstance SFXVolumeTestEvent;

     Bus Music;
     Bus SFX;
     Bus Master;

     void Awake()
     {
          DontDestroyOnLoad(gameObject);
          SetPath();
     }

     void Update()
     {
          SetVolumes();
     }

     public void SetPath()
     {
          Music = RuntimeManager.GetBus("bus:/Master/Music");
          SFX = RuntimeManager.GetBus("bus:/Master/SFX");
          Master = RuntimeManager.GetBus("bus:/Master");
          SFXVolumeTestEvent = RuntimeManager.CreateInstance("event:/Pickaxe");
     }

     public void SetVolumes()
     {
          Master.setVolume(GameManager.instance.settingsData.masterVolume);
          Music.setVolume(GameManager.instance.settingsData.musicVolume);
          SFX.setVolume(GameManager.instance.settingsData.SFXVolume);
     }

     public void MasterVolumeLevel(float newVolume)
     {
          GameManager.instance.settingsData.masterVolume = newVolume;
          GameManager.instance.settingsData.ApplySettings();
     }

     public void MusicVolumeLevel(float newVolume)
     {
          GameManager.instance.settingsData.musicVolume = newVolume;
          GameManager.instance.settingsData.ApplySettings();
     }

     public void SFXVolumeLevel(float newVolume)
     {
          GameManager.instance.settingsData.SFXVolume = newVolume;
          GameManager.instance.settingsData.ApplySettings();

          //Toca o som durante o ajuste de volume.
          PLAYBACK_STATE pbState;
          SFXVolumeTestEvent.getPlaybackState(out pbState);
          if (pbState != PLAYBACK_STATE.PLAYING)
               SFXVolumeTestEvent.start();
     }

     public void PlayMoveSound()
     {
          RuntimeManager.PlayOneShot(menuMoveSound);
     }

     public void PlayConfirmSound()
     {
          RuntimeManager.PlayOneShot(menuConfirmSound);
     }
}