using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossTrigger : MonoBehaviour
{
    public GameObject boss;
    public Transform spawnPoint;
    public UnityEvent Ontriggered;

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            GameObject newBoss = Instantiate(boss, spawnPoint.position, spawnPoint.rotation);
            PlayerController.instance.death.boss = newBoss;
            Ontriggered?.Invoke();
        }
    }
}
