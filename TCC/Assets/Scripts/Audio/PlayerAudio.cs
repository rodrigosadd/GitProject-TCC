using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Sound events:")]
    [FMODUnity.EventRef] public string[] playerFootsteps;
    [FMODUnity.EventRef] public string[] playerJumpSounds;
    [FMODUnity.EventRef] public string minningSound;
    [FMODUnity.EventRef] public string deathCry;
    [FMODUnity.EventRef] public string boxPushingSound;

    //Character Sound System Functions
    public void PlayFootsteps()
    {
        int rand = Random.Range(1, 4);

        switch (rand)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot(playerFootsteps[0]);
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShot(playerFootsteps[1]);
                break;
            case 3:
                FMODUnity.RuntimeManager.PlayOneShot(playerFootsteps[2]);
                break;
        }
    }

    public void PlayJumpSound()
    {
        int rand = Random.Range(1, 4);

        switch (rand)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot(playerJumpSounds[0]);
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShot(playerJumpSounds[1]);
                break;
            case 3:
                FMODUnity.RuntimeManager.PlayOneShot(playerJumpSounds[2]);
                break;
        }
    }

    public void PlayPickSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(minningSound);
    }

    public void PlayDeathSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(deathCry);
    }

    public void PlayPushingSound()
    {
        FMODUnity.RuntimeManager.PlayOneShot(boxPushingSound);
    }
}
