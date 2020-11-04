using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Character : MonoBehaviour
{
     public Transform characterGraphic;
     public Collider characterCollider;
     public Animator animator;


     [Header("Hit variables")]
     public Hit hit;
     private float _countdownResetHitCount;

     [System.Serializable]
     public class Hit
     {
          public Transform currentPoint;
          public int hitCount;
          public int maxHitCount;
          public float timeResetHitCount;
     }

     public void TakeHit()
     {
          hit.hitCount++;
     }

     public void ResetHitCount()
     {
          if (_countdownResetHitCount < 1)
          {
               _countdownResetHitCount += Time.deltaTime / hit.timeResetHitCount;
          }
          else
          {
               _countdownResetHitCount = 0;
               hit.hitCount = 0;
          }
     }
}
