using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public Transform portalEntrance;
    public Transform portalExit;
    public float rangeTeleport = 1f;
    public bool seeRangeTeleport = false;

    private float _distanceBetween = 0f;

    void Update()
    {
        PlayerTeleport();
    }

    public void PlayerTeleport()
    {
        _distanceBetween = Vector3.Distance(portalEntrance.position, PlayerController.instance.transform.position);

        if (_distanceBetween <= rangeTeleport)
        {
            PlayerController.instance.transform.position = portalExit.position + portalExit.forward;
            PlayerController.instance.currentJump = 0;
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
        if (seeRangeTeleport)
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(portalEntrance.position, rangeTeleport);
        }
    }
#endif
}
