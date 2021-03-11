using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour {
    [Header ("Sound events:")]
    [FMODUnity.EventRef] public string[] playerFootsteps;
    [FMODUnity.EventRef] public string[] playerJumpSounds;
    [FMODUnity.EventRef] public string[] playerAttackSounds;
    [FMODUnity.EventRef] public string minningSound;
    [FMODUnity.EventRef] public string deathCry;
    [FMODUnity.EventRef] public string boxPushingSound;

    //Character Sound System Functions
    public void PlayFootsteps () {
        int rand = Random.Range (1, 5);

        switch (rand) {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot (playerFootsteps[0], transform.position);
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShot (playerFootsteps[1], transform.position);
                break;
            case 3:
                FMODUnity.RuntimeManager.PlayOneShot (playerFootsteps[2], transform.position);
                break;
            case 4:
                FMODUnity.RuntimeManager.PlayOneShot (playerFootsteps[3], transform.position);
                break;
        }
    }

    public void PlayAttack () {
        int rand = Random.Range (1, 4);

        switch (rand) {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot (playerAttackSounds[0], transform.position);
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShot (playerAttackSounds[1], transform.position);
                break;
            case 3:
                FMODUnity.RuntimeManager.PlayOneShot (playerAttackSounds[2], transform.position);
                break;
        }
    }

    public void PlayJumpSound () {
        int rand = Random.Range (1, 4);

        switch (rand) {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot (playerJumpSounds[0], transform.position);
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShot (playerJumpSounds[1], transform.position);
                break;
            case 3:
                FMODUnity.RuntimeManager.PlayOneShot (playerJumpSounds[2], transform.position);
                break;
        }
    }

    public void PlayPickSound () {
        FMODUnity.RuntimeManager.PlayOneShot (minningSound, transform.position);
    }

    public void PlayDeathSound () {
        FMODUnity.RuntimeManager.PlayOneShot (deathCry, transform.position);
    }

    public void PlayPushingSound () {
        FMODUnity.RuntimeManager.PlayOneShot (boxPushingSound, transform.position);
    }
}