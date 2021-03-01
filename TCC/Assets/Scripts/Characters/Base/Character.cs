using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
     public Transform characterGraphic;
     public Animator animator;

     [Header("Hit variables")]
     public Hit hit;
     private float _countdownResetHitCount;

     [System.Serializable]
     public class Hit
     {
          public Coroutine reset;
          public int hitCount;
          public int maxHitCount;
          public float timeResetHitCount;
     }

     public void TakeHit()
     {
          hit.hitCount++;

          if (hit.reset != null)
          {
               StopCoroutine(ResetHitCount());
          }
          hit.reset = StartCoroutine(ResetHitCount());
     }

     public IEnumerator ResetHitCount()
     {
          yield return new WaitForSeconds(hit.timeResetHitCount);
          hit.hitCount = 0;
          hit.reset = null;
     }
}