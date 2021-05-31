using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    public static BossController instance;
    public Animator anim;
    public Transform antlersAttackPoint;
    public GameObject antlersAttackLaser;
    [HideInInspector]
    public Vector3 playerDirection;
    public float rangeAntlersAttack;
    public int life;
    public int enragedLife;
    public int enragedFinalLife;
    public float timeToDeactivateMesh;
    public float timeToResetSlowMotion;
    private bool _canActivateAntlersAttack;
    private bool _invunerable;
    private bool _alreadyStartedCoroutine;

// #if UNITY_EDITOR
//     public bool seeRangeantlersAttack;
// #endif

    [Header("See Object variables")]
    public Transform targetCam;
    public float timeToReturnPlayerTarget = 2f;
    public bool seeObject;
    private float countdownToReturnPlayerTarget;
    private bool canChangeTargetCam;


    public UnityEvent OnAntlersAttack;
    public UnityEvent OnTakedamage;
    public UnityEvent IsDead;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        CheckCurrentLife();
        Aim();
        AntlersAttack();
        CountdownToReturnPlayerTarget();
    }

    public void TakeDamage()
    {
        if(!_invunerable)
        {
            _invunerable = true;
            life--;
            anim.SetBool("Hit", true);
            OnTakedamage?.Invoke();
            StopCoroutine("ResetHit");
            StartCoroutine("ResetHit");
        }
    }

    public void CheckCurrentLife()
    {   
        if(life <= 0)
        {
            anim.SetBool("Dying", true);

            if(!_alreadyStartedCoroutine)
            {
                _alreadyStartedCoroutine = true;
                Time.timeScale = 0.5f;
                StartCoroutine(ResetSlowMotion());
                StartCoroutine(DelayToFinishAnimation());
            }
        }
    }

    IEnumerator ResetSlowMotion()
    {
        yield return new WaitForSeconds(timeToResetSlowMotion);
        Time.timeScale = 1f;
    }
    IEnumerator DelayToFinishAnimation()
    {
        yield return new WaitForSeconds(timeToDeactivateMesh);
        SeeObjectDrop();
        IsDead?.Invoke();
    }

    IEnumerator ResetHit()
    {
        yield return new WaitForSeconds(1f);
        anim.SetBool("Hit", false);
        _invunerable = false;
    }
    
    public void Aim()
    {
        playerDirection = (PlayerController.instance.movement.targetCam.position - transform.position).normalized;
    }

    public void ActivateAntlersAttack()
    {
        _canActivateAntlersAttack = true;
        antlersAttackLaser.SetActive(true);
        OnAntlersAttack?.Invoke();
    }

    public void DeactivateAntlersAttack()
    {
        _canActivateAntlersAttack = false;
        antlersAttackLaser.SetActive(false);
    }

    public void AntlersAttack()
    {   
        if(_canActivateAntlersAttack)
        {
            RaycastHit _hitInfo;

            if(Physics.Raycast(antlersAttackPoint.position, antlersAttackPoint.up, out _hitInfo, rangeAntlersAttack))
            {               
                if(_hitInfo.transform.tag == "Player" && !PlayerController.instance.death.isInvincible)
                {
                    PlayerController.instance.hit.hitCount = PlayerController.instance.hit.maxHitCount;
                    StopCoroutine("DelayDeactivateBoss");
                    StartCoroutine("DelayDeactivateBoss");
                }
            }
        }
    }

    IEnumerator DelayDeactivateBoss()
    {
        yield return new WaitForSeconds(1.8f);
        
        if( PlayerController.instance.death.boss != null)
        {
            PlayerController.instance.death.boss.SetActive(false);
            PlayerController.instance.death.boss = null;
        }
        PlayerController.instance.death.bossTrigger.SetActive(true);
    }   

    public void SeeObjectDrop()
     {
          if(seeObject)
          {
               canChangeTargetCam = true;
               Camera3rdPerson.instance.targetCamera = targetCam;
               Camera3rdPerson.instance.ConfigToShowObject();
               PlayerController.instance.movement.canMove = false;
          }
     }

     public void CountdownToReturnPlayerTarget()
     {
          if(canChangeTargetCam)
          {
               if(countdownToReturnPlayerTarget < 1)
               {
                    countdownToReturnPlayerTarget += Time.deltaTime / timeToReturnPlayerTarget;
               }
               else
               {
                    canChangeTargetCam = false;
                    Camera3rdPerson.instance.targetCamera = PlayerController.instance.movement.targetCam;
                    Camera3rdPerson.instance.ResetConfig();
                    PlayerController.instance.movement.canMove = true;
                    seeObject = false;
               }
          }
     }

// #if UNITY_EDITOR
//      void OnDrawGizmos()
//      {
//           if (seeRangeantlersAttack)
//           {
//                Gizmos.color = Color.red;
//                Gizmos.DrawLine(antlersAttackPoint.position, antlersAttackPoint.position + antlersAttackPoint.up * rangeAntlersAttack);
//           }
//      }
// #endif
}
