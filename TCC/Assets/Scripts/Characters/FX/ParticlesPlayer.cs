using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesPlayer : MonoBehaviour
{
    [Header("ParticleSystem variables")]
    public ParticleSystem fallingDust;
    public ParticleSystem stun;
    public ParticleSystem die;
    public ParticleSystem liquidSplash;

    public void FallingDust()
    {
        fallingDust.Play();
    }

    public void Stun()
    {
        stun.Play();
    }

    public void Die()
    {
        die.Play();
    }

    public void LiquidSplashe()
    {
        liquidSplash.Play();
    }
}
