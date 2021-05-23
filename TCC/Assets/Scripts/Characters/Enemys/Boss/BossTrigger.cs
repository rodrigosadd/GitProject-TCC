using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossTrigger : MonoBehaviour
{
    public UnityEvent Ontriggered;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Ontriggered?.Invoke();
        }
    }
}
