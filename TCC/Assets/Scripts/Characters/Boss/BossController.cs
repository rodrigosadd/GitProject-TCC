using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossController : MonoBehaviour
{
    public static BossController instance;
    public Animator anim;
    public Transform antlersAttackPoint;
    public GameObject temporaryObj;
    [HideInInspector]
    public Vector3 playerDirection;
    public float rangeAntlersAttack;
    public int life;
    public int enragedLife;
    public int enragedFinalLife;
    private bool _canActivateAntlersAttack;
    private bool _invunerable;

#if UNITY_EDITOR
    public bool seeRangeantlersAttack;
#endif

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
            IsDead?.Invoke();
        }
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
        seeRangeantlersAttack = true;
        temporaryObj.SetActive(true);
        OnAntlersAttack?.Invoke();
    }

    public void DeactivateAntlersAttack()
    {
        _canActivateAntlersAttack = false;
        seeRangeantlersAttack = false;
        temporaryObj.SetActive(false);
    }

    public void AntlersAttack()
    {   
        if(_canActivateAntlersAttack)
        {
            RaycastHit _hitInfo;

            if(Physics.Raycast(antlersAttackPoint.position, antlersAttackPoint.up, out _hitInfo, rangeAntlersAttack))
            {
                if(_hitInfo.transform.tag == "Player")
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
        PlayerController.instance.death.boss.SetActive(false);
        PlayerController.instance.death.boss = null;
        PlayerController.instance.death.bossTrigger.SetActive(true);
    }

#if UNITY_EDITOR
     void OnDrawGizmos()
     {
          if (seeRangeantlersAttack)
          {
               Gizmos.color = Color.red;
               Gizmos.DrawLine(antlersAttackPoint.position, antlersAttackPoint.position + antlersAttackPoint.up * rangeAntlersAttack);
          }
     }
#endif
}
