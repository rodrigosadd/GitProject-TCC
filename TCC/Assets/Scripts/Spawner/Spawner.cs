using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Transform [] spawnPointsProjectileForward;
    public Transform [] spawnPointsProjectileAbove;
    public Transform spawnPointProjectileInDir;
    public float impulseForceProjectileForward;
    public float impulseForceProjectileAbove;
    public float impulseForceInDirection;
    private bool _canSpawnProjectileAbove;

    void Update()
    {
        SetDirection();
    }

    void SpawnProjectile()
    {
        for (int i = 0; i < spawnPointsProjectileForward.Length; i++)
        {
            Transform spawnPoint = spawnPointsProjectileForward[i]; 
            Projectile projectile = GameManager.instance.poolSystem.TryToGetProjectile();
            projectile.rbody.velocity = Vector3.zero;
            projectile.transform.position = spawnPoint.position;
            projectile.transform.rotation = spawnPoint.rotation;
            projectile.rbody.AddForce(projectile.transform.up * impulseForceProjectileForward, ForceMode.Impulse); 
        }                                   
    }

    void SpawnRandomPosition(int amountProjectileAbove)
    {   
        _canSpawnProjectileAbove = true;
        StartCoroutine(DelaySpawnRandomPosition(amountProjectileAbove));
    }

    IEnumerator DelaySpawnRandomPosition(int amountProjectileAbove)
    {
        while(_canSpawnProjectileAbove)
        {
            int indexSpanwPoint = Random.Range(0, spawnPointsProjectileAbove.Length);
            Transform spawnPoint = spawnPointsProjectileAbove[indexSpanwPoint]; 
            Projectile projectile = GameManager.instance.poolSystem.TryToGetProjectileAbove();
            projectile.rbody.velocity = Vector3.zero;
            projectile.transform.position = spawnPoint.position;
            projectile.transform.rotation = spawnPoint.rotation;
            projectile.rbody.AddForce(projectile.transform.up * impulseForceProjectileAbove, ForceMode.Impulse); 
            amountProjectileAbove--;

            if(amountProjectileAbove <= 0)
            {
                _canSpawnProjectileAbove = false;
            }
            yield return new WaitForSeconds(1f);
        }
    }

    public void SetDirection()
    {
        Vector3 _direction = (PlayerController.instance.transform.position - spawnPointProjectileInDir.position).normalized;
        spawnPointProjectileInDir.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
    }

    void SpawnProjectileInDirection()
    {
        Projectile projectile = GameManager.instance.poolSystem.TryToGetProjectileAbove();
        projectile.rbody.velocity = Vector3.zero;
        projectile.transform.position = spawnPointProjectileInDir.position;
        projectile.transform.rotation = spawnPointProjectileInDir.rotation;
        projectile.rbody.AddForce(projectile.transform.up * impulseForceInDirection, ForceMode.Impulse);                                    
    }
}
