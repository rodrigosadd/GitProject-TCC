using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsData : MonoBehaviour
{
    public int maxJump;
    public int canAttack;
    public int canSeeTeleport;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void ApplySettings()
     {
        PlayerController.instance.jump.maxJump = maxJump;

        PlayerAttackController.instance.canAttack = (canAttack == 1) ? true : false;

        PlayerController.instance.levelMechanics.canSeeTeleport = (canSeeTeleport == 1) ? true : false;         
     }
}
