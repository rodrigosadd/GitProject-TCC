using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [Header("Projectile Forward variables")]
    public Transform [] spawnPointsProjectileForwardRight;
    public float impulseForceProjectileForwardRight;
    public Transform [] spawnPointsProjectileForwardLeft;
    public float impulseForceProjectileForwardLeft;
    public UnityEvent OnSpawnProjectileForward;

    [Header("Projectile Random variables")]
    public Transform [] spawnPointsProjectileRandom;
    public float impulseForceProjectileRandom;
    public float timeToSpawnRandomPosition;
    public float timeToDeactivateProjectileRandom;
    public int maxAmountProjectileRandom;
    public int amountProjectileRandom;
    public UnityEvent OnSpawnProjectileRandom;

    [Header("Projectile Throw variables")]
    public Transform [] spawnPointsProjectileThrow;
    public float impulseForceProjectileThrow;
    public UnityEvent OnSpawnProjectileThrow;

    [Header("Thorn variables")]
    public Transform [] spawnPointsThorn;
    public int amountThorns;
    public int maxAmountThorns;
    private bool _canSpawnThorns;
    
    public UnityEvent OnSpawnThorn;

    [Header("Projectile In Dir variables")]
    public Transform spawnPointProjectileInDir;
    public float impulseForceInDirection;
    public UnityEvent OnSpawnProjectileInDir;

    private bool _canSpawnProjectileAbove;

    void Update()
    {
        SetDirection();
    }

    void SpawnProjectileForwardRight()
    {
        for (int i = 0; i < spawnPointsProjectileForwardRight.Length; i++)
        {
            Transform spawnPoint = spawnPointsProjectileForwardRight[i]; 
            Projectile projectile = GameManager.instance.poolSystem.TryToGetProjectile();
            projectile.rbody.velocity = Vector3.zero;
            projectile.transform.position = spawnPoint.position;
            projectile.transform.rotation = spawnPoint.rotation;
            projectile.rbody.AddForce(projectile.transform.up * impulseForceProjectileForwardRight, ForceMode.Impulse);
        }                                   
        OnSpawnProjectileForward?.Invoke(); 
    }

    void SpawnProjectileForwardLeft()
    {
        for (int i = 0; i < spawnPointsProjectileForwardLeft.Length; i++)
        {
            Transform spawnPoint = spawnPointsProjectileForwardLeft[i]; 
            Projectile projectile = GameManager.instance.poolSystem.TryToGetProjectile();
            projectile.rbody.velocity = Vector3.zero;
            projectile.transform.position = spawnPoint.position;
            projectile.transform.rotation = spawnPoint.rotation;
            projectile.rbody.AddForce(projectile.transform.up * impulseForceProjectileForwardLeft, ForceMode.Impulse);
        }                                   
        OnSpawnProjectileForward?.Invoke(); 
    }

    void SpawnProjectileThrow()
    {
        for (int i = 0; i < spawnPointsProjectileThrow.Length; i++)
        {
            Transform spawnPoint = spawnPointsProjectileThrow[i]; 
            Projectile projectile = GameManager.instance.poolSystem.TryToGetProjectilesThrow();
            projectile.rbody.velocity = Vector3.zero;
            projectile.transform.position = spawnPoint.position;
            projectile.transform.rotation = spawnPoint.rotation;
            projectile.rbody.AddForce(projectile.transform.up * impulseForceProjectileThrow, ForceMode.Impulse); 
        }                                   
        OnSpawnProjectileThrow?.Invoke();
    }

    void SpawnRandomPosition()
    {   
        _canSpawnProjectileAbove = true;
        StartCoroutine("DelaySpawnRandomPosition");
    }

    IEnumerator DelaySpawnRandomPosition()
    {
        int currentIndex = 0;
        while(_canSpawnProjectileAbove)
        {
            int indexSpanwPoint = Random.Range(0, spawnPointsProjectileRandom.Length);

            if(currentIndex != indexSpanwPoint)
            {
                currentIndex = indexSpanwPoint;
                Transform spawnPoint = spawnPointsProjectileRandom[indexSpanwPoint]; 
                Projectile projectile = GameManager.instance.poolSystem.TryToGetProjectile();
                projectile.rbody.velocity = Vector3.zero;
                projectile.delayDeactivateObject = timeToDeactivateProjectileRandom;
                projectile.transform.position = spawnPoint.position;
                projectile.transform.rotation = spawnPoint.rotation;
                projectile.rbody.AddForce(projectile.transform.up * impulseForceProjectileRandom, ForceMode.Impulse); 
                amountProjectileRandom--;
                OnSpawnProjectileRandom?.Invoke();

                if(amountProjectileRandom <= 0)
                {
                    amountProjectileRandom = maxAmountProjectileRandom;
                    _canSpawnProjectileAbove = false;
                    StopCoroutine("DelaySpawnRandomPosition");
                }
                yield return new WaitForSeconds(timeToSpawnRandomPosition);
            }
        }
    }

    public void SetDirection()
    {
        Vector3 _direction = (PlayerController.instance.transform.position - spawnPointProjectileInDir.position).normalized;
        spawnPointProjectileInDir.rotation = Quaternion.FromToRotation(Vector3.up, _direction);
    }

    void SpawnProjectileInDirection()
    {
        Projectile projectile = GameManager.instance.poolSystem.TryToGetProjectilesInDir();
        projectile.rbody.velocity = Vector3.zero;
        projectile.transform.position = spawnPointProjectileInDir.position;
        projectile.transform.rotation = spawnPointProjectileInDir.rotation;
        projectile.rbody.AddForce(projectile.transform.up * impulseForceInDirection, ForceMode.Impulse); 
        OnSpawnProjectileInDir?.Invoke();                                   
    }

    public void SpawnRandomPositionThorns()
    {   
        int currentIndex = 0;
        _canSpawnThorns = true;

        while(_canSpawnThorns)
        {
            int indexSpanwPoint = Random.Range(0, spawnPointsThorn.Length);

            if(currentIndex != indexSpanwPoint)
            {
                Transform spawnPoint = spawnPointsThorn[indexSpanwPoint]; 
                BossThorn thorn = GameManager.instance.poolSystem.TryToGetThorn();
                thorn.transform.position = spawnPoint.position;
                amountThorns--;

                if(amountThorns <= 0)
                {
                    amountThorns = maxAmountThorns;
                    _canSpawnThorns = false;
                }
            }
        }
        OnSpawnThorn?.Invoke();
    }
}
