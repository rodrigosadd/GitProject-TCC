using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Character : MonoBehaviour
{
     public Transform characterGraphic;
     public Collider characterCollider;
     public Animator animator;
     public int stunCount;

     public void TakeDamage()
     {
          stunCount++;
     }
}
