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
        PUSH,
        FLOOR,
        FAN
    }

    public MechanicType mechanicType;
    bool OffFloor = false;
    float time;

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

    private void OnCollisionEnter(Collision collision)
    {
        if (mechanicType == MechanicType.FLOOR)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                Debug.Log("Colidiu");
                OffFloor = true;
            }
        }
    }

    private void Update()
    {
        if(OffFloor == true)
        {
            Debug.Log("Entrou no true");
            time = time +1 * Time.deltaTime;
            if (time >= 2)
            {
                this.gameObject.GetComponent<MeshRenderer>().enabled = false;
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }

            if (time >= 4)
            {
                this.gameObject.GetComponent<MeshRenderer>().enabled = true;
                this.gameObject.GetComponent<BoxCollider>().enabled = true;
                OffFloor = false;
                time = 0;
            }
        }

        if (mechanicType == MechanicType.FAN)
        {
            this.gameObject.transform.Rotate (0,0,3);
        }
    }
}
