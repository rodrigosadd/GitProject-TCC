using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [Header("Projectile Forward variables")]
    public Transform [] spawnPointsProjectileForward;
    public float impulseForceProjectileForward;
    public UnityEvent OnSpawnProjectileForward;

    [Header("Projectile Above variables")]
    public Transform [] spawnPointsProjectileAbove;
    public float impulseForceProjectileAbove;
    public float timeToSpawnRandomPosition;
    public int maxAmountProjectileAbove;
    public int amountProjectileAbove;
    public UnityEvent OnSpawnProjectileAbove;

    [Header("Projectile Throw variables")]
    public Transform [] spawnPointsProjectileThrow;
    public float impulseForceProjectileThrow;
    public UnityEvent OnSpawnProjectileThrow;

    [Header("Thorn variables")]
    public Transform [] spawnPointsThorn;
    public int amountThorns;
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

    void SpawnProjectileForward()
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

    void SpawnRandomPositionAbove()
    {   
        _canSpawnProjectileAbove = true;
        StartCoroutine("DelaySpawnRandomPosition");
    }

    IEnumerator DelaySpawnRandomPosition()
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
            OnSpawnProjectileAbove?.Invoke();

            if(amountProjectileAbove <= 0)
            {
                amountProjectileAbove = maxAmountProjectileAbove;
                _canSpawnProjectileAbove = false;
                StopCoroutine("DelaySpawnRandomPosition");
            }
            yield return new WaitForSeconds(timeToSpawnRandomPosition);
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
        OnSpawnProjectileInDir?.Invoke();                                   
    }

    public void SpawnRandomPositionThorns()
    {   
        for (int i = 0; i < amountThorns; i++)
        {
            int indexSpanwPoint = Random.Range(0, spawnPointsThorn.Length);
            Transform spawnPoint = spawnPointsThorn[indexSpanwPoint]; 
            BossThorn thorn = GameManager.instance.poolSystem.TryToGetThorn();
            thorn.transform.position = spawnPoint.position;
        }
        OnSpawnThorn?.Invoke();
    }
}
