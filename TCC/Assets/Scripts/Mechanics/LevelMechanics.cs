using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMechanics : MonoBehaviour
{
    public enum MechanicType
    {
        SLOW,
        SLIDE,
        KILL,
        PUSH
    }

    public MechanicType mechanicType;

    private void OnTriggerEnter(Collider other)
    {
        if (mechanicType == MechanicType.KILL)
        {
            //Kill the character.
        }
        else if (mechanicType == MechanicType.PUSH)
        {
            //Push the character.
        }
        else if (mechanicType == MechanicType.SLIDE)
        {
            //Slide the character.
            float rand = Random.Range(0.0f, 10.0f);
            other.gameObject.GetComponent<Rigidbody>().AddForce(rand, 0, rand, ForceMode.Impulse);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (mechanicType == MechanicType.SLOW)
        {
            //Slow down character's speed.
        }
    }
}
