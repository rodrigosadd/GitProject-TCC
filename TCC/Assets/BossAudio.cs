using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class BossAudio : MonoBehaviour
{
    [Header ("Sound events:")]
    [FMODUnity.EventRef] public string[] speechSentences;
    [FMODUnity.EventRef] public string[] hurtSounds;
    [FMODUnity.EventRef] public string antlersAttack;
    [FMODUnity.EventRef] public string projectile;
    [FMODUnity.EventRef] public string spawn;
    [FMODUnity.EventRef] public string thorn;
    [FMODUnity.EventRef] public string death;
    
    /// <summary>
    /// Triggers the play function for a random speech.
    /// </summary>
    public void PlaySpeech () {
        int rand = Random.Range (1, 5);

        switch (rand) {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShotAttached(speechSentences[0], this.gameObject);
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShotAttached(speechSentences[1], this.gameObject);
                break;
            case 3:
                FMODUnity.RuntimeManager.PlayOneShotAttached(speechSentences[2], this.gameObject);
                break;
            case 4:
                break;
        }
    }
    /// <summary>
    /// Triggers the play function for a random hurt scream.
    /// </summary>
    public void PlayHurt () {
        int rand = Random.Range (1, 4);

        switch (rand) {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShotAttached(hurtSounds[0], this.gameObject);
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShotAttached(hurtSounds[1], this.gameObject);
                break;
            case 3:
                FMODUnity.RuntimeManager.PlayOneShotAttached(hurtSounds[2], this.gameObject);
                break;
        }
    }
    /// <summary>
    /// Triggers the play function for the laser sound.
    /// </summary>
    public void PlayAntlersAttackSound() {
        RuntimeManager.PlayOneShotAttached(antlersAttack, this.gameObject);
    }
    /// <summary>
    /// Triggers the play function for the projectile sound.
    /// </summary>
    public void PlayProjectileAttackSound() {
        RuntimeManager.PlayOneShotAttached(projectile, this.gameObject);
    }
    /// <summary>
    /// Triggers the play function for the spawn sound.
    /// </summary>
    public void PlaySpawnAttackSound() {
        RuntimeManager.PlayOneShotAttached(spawn, this.gameObject);
    }
    /// <summary>
    /// Triggers the play function for the spawn sound.
    /// </summary>
    public void PlaySpawnThornSound() {
        RuntimeManager.PlayOneShotAttached(thorn, this.gameObject);
    }
    /// <summary>
    /// Triggers the play function for the spawn sound.
    /// </summary>
    public void PlayDeathSound() {
        RuntimeManager.PlayOneShotAttached(death, this.gameObject);
    }
}
