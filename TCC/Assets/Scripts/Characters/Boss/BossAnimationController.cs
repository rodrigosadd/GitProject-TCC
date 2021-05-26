using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Animations.Rigging;

public class BossAnimationController : MonoBehaviour
{
    public static BossAnimationController instance;
    public Animator anim;
    public float delayToFirstAttack;
    public float delayToSecondAttack;
    public float delayToThirdAttack;
    public float delayToExitStunIdle;
    private bool _alreadyStartedAnimation;

    public UnityEvent OnStunIdle;
    public UnityEvent OnExitStunIdle;
    public UnityEvent OnEnraged;
    public UnityEvent OnEnragedFinal;

    void Awake()
    {
        instance = this;
    }

    public void TimeToFirstAttack()
    {
        if(!_alreadyStartedAnimation)
        {
            _alreadyStartedAnimation = true;
            StopCoroutine("DelayToFirstAttack");
            StartCoroutine("DelayToFirstAttack");
        }
    }

    IEnumerator DelayToFirstAttack()
    {
        yield return new WaitForSeconds(delayToFirstAttack);
        anim.SetBool("Attack 1", true);
        anim.SetBool("Attack 2", false);
        anim.SetBool("Attack 3", false);
        _alreadyStartedAnimation = false;
    }

    public void TimeToSecondAttack()
    {
        if(!_alreadyStartedAnimation)
        {
            OnExitStunIdle?.Invoke();
            _alreadyStartedAnimation = true;           
            StopCoroutine("DelayToSecondAttack");
            StartCoroutine("DelayToSecondAttack");
        }
    }

    IEnumerator DelayToSecondAttack()
    {
        yield return new WaitForSeconds(delayToSecondAttack);
        anim.SetBool("Attack 1", false);
        anim.SetBool("Attack 2", true);
        anim.SetBool("Attack 3", false);
        _alreadyStartedAnimation = false;
    }

    public void TimeToThirdAttack()
    {
        if(!_alreadyStartedAnimation)
        {
            _alreadyStartedAnimation = true;
            StopCoroutine("DelayToThirdAttack");
            StartCoroutine("DelayToThirdAttack");
        }
    }

    IEnumerator DelayToThirdAttack()
    {
        yield return new WaitForSeconds(delayToThirdAttack);
        anim.SetBool("Attack 1", false);
        anim.SetBool("Attack 2", false);
        anim.SetBool("Attack 3", true);
        _alreadyStartedAnimation = false;
    }

    public void TimeToExitStunIdle()
    {
        if(!_alreadyStartedAnimation)
        {
            _alreadyStartedAnimation = true;
            StopCoroutine("DelayToExitStunIdle");
            StartCoroutine("DelayToExitStunIdle");
        }
    }

    IEnumerator DelayToExitStunIdle()
    {
        yield return new WaitForSeconds(delayToExitStunIdle);
        anim.SetBool("Attack 1", true);
        anim.SetBool("Attack 2", false);
        anim.SetBool("Attack 3", false);
        anim.SetBool("Stun Idle", false);
        _alreadyStartedAnimation = false;
    }

    public void SetStunIdle()
    {
        anim.SetBool("Attack 1", false);
        anim.SetBool("Attack 2", false);
        anim.SetBool("Attack 3", false);
        anim.SetBool("Stun Idle", true);
        OnStunIdle?.Invoke();
    }

    public void CheckLife()
    {
        if(BossController.instance.life == BossController.instance.enragedLife)
        {
            anim.SetBool("Enraged", true); 
            anim.SetBool("Attack 1", false);
            anim.SetBool("Attack 2", false);
            anim.SetBool("Attack 3", false);
            anim.SetBool("Stun Idle", false);
            OnEnraged?.Invoke();
        }

        if(BossController.instance.life == BossController.instance.enragedFinalLife)
        {
            anim.SetBool("Enraged Final", true);
            anim.SetBool("Enraged", false); 
            anim.SetBool("Attack 1", false);
            anim.SetBool("Attack 2", false);
            anim.SetBool("Attack 3", false);
            anim.SetBool("Stun Idle", false);
            OnEnragedFinal?.Invoke();
        }
    }

    public void SetCameraShake(float durationShake)
    {
        Camera3rdPerson.instance.CameraShake(durationShake);
    }
}
