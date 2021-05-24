using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public static BossController instance;
    public Animator anim;
    public int life;
    public int enragedLife;
    public int enragedFinalLife;

    [HideInInspector]
    public Vector3 playerDirection;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        CheckCurrentLife();
        Aim();
    }

    public void TakeDamage()
    {
        life--;
        anim.SetBool("Hit", true);
    }

    public void CheckCurrentLife()
    {   
        if(life <= 0)
        {
            anim.SetBool("Dying", true);
        }
    }

    public void Aim()
    {
        playerDirection = (PlayerController.instance.transform.position - transform.position).normalized;
    }
}
