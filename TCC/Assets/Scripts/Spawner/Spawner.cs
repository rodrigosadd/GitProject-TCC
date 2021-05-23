using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform [] spawnPoints;
    public float impulseForce;

    [ContextMenu("Spawn Projectile")]
    void SpawnProjectile()
    {
        for (int i = 0; i < spawnPoints.Length; i++)
        {
            Transform spawnPoint = spawnPoints[i]; 
            Projectile projectile = GameManager.instance.poolSystem.TryToGetProjectile();
            projectile.rbody.velocity = Vector3.zero;
            projectile.transform.position = spawnPoint.position;
            projectile.transform.rotation = spawnPoint.rotation;
            projectile.rbody.AddForce(projectile.transform.up * impulseForce, ForceMode.Impulse); 
        }                                   
    }

    void SpawnRandomPosition()
    {
        int indexSpanwPoint = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[indexSpanwPoint]; 
        Projectile projectile = GameManager.instance.poolSystem.TryToGetProjectile();
        projectile.rbody.velocity = Vector3.zero;
        projectile.transform.position = spawnPoint.position;
        projectile.transform.rotation = spawnPoint.rotation;
        projectile.rbody.AddForce(projectile.transform.up * impulseForce, ForceMode.Impulse); 
    }
}
