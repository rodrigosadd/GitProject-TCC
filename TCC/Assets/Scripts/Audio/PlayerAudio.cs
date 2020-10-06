using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [Header("Sound events:")]
    [FMODUnity.EventRef] public string playerFootsteps1;
    [FMODUnity.EventRef] public string playerFootsteps2;
    [FMODUnity.EventRef] public string playerJumpSound;
    [FMODUnity.EventRef] public string playerDoubleJumpSound;
    [FMODUnity.EventRef] public string minningSound;
    [FMODUnity.EventRef] public string deathCry;

    //Character Sound System Functions
    public void PlayFootsteps()
    {
        int rand = Random.Range(1, 2);

        switch (rand)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot(playerFootsteps1);
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShot(playerFootsteps2);
                break;
        }
    }

    public void PlayJumpSound()
    {
        int rand = Random.Range(1, 2);

        switch (rand)
        {
            case 1:
                FMODUnity.RuntimeManager.PlayOneShot(playerJumpSound);
                break;
            case 2:
                FMODUnity.RuntimeManager.PlayOneShot(playerDoubleJumpSound);
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
}
