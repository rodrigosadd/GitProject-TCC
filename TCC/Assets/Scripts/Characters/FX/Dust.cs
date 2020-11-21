using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dust : MonoBehaviour
{
     public ParticleSystem dust;

     public void PlayParticleDust()
     {
          dust.Play();
     }
}
