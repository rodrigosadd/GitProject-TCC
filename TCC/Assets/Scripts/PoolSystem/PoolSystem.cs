using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    [Header("Projectile Pool variables")]
    public List<Projectile> listProjectilePool;
    public Projectile projectilePrefab;
    public int initialAmountProjectiles;
    private GameObject _projectilesHolder;

    [Header("Projectile Throw Pool variables")]
    public List<Projectile> listProjectileThrowPool;
    public Projectile projectileThrowPrefab;
    public int initialAmountProjectilesThrow;
    private GameObject _projectileThrowHolder;
    
    [Header("Projectile In Dir Pool variables")]
    public List<Projectile> listProjectileInDirPool;
    public Projectile projectileInDirPrefab;
    public int initialAmountProjectilesInDir;
    private GameObject _projectileInDirHolder;

    [Header("Thorn Pool variables")]
    public List<BossThorn> listThornPool;
    public BossThorn thornPrefab;
    public int initialAmountThorns;
    private GameObject _thornsHolder;

    void Awake()
    {
        InitializePool();
    }

    public void ResetPool()
    {
        for (int index = 0; index <= initialAmountProjectiles; index++)
        {
            listProjectilePool[index].transform.gameObject.SetActive(false);
        }

        for (int index = 0; index <= initialAmountProjectilesThrow; index++)
        {
            listProjectileThrowPool[index].transform.gameObject.SetActive(false);
        }

        for (int index = 0; index <= initialAmountProjectilesInDir; index++)
        {
            listProjectileInDirPool[index].transform.gameObject.SetActive(false);
        }

        for (int index = 0; index <= initialAmountThorns; index++)
        {
            listThornPool[index].transform.gameObject.SetActive(false);
        }
    }

    void InitializePool()
    {
        _projectilesHolder = new GameObject("--- Projectiles Pool");
        _projectilesHolder.transform.position = Vector2.zero;
        for (int index = 0; index <= initialAmountProjectiles; index++)
        {
            Projectile _projectile = Instantiate(projectilePrefab);
            _projectile.transform.SetParent(_projectilesHolder.transform);
            _projectile.gameObject.SetActive(false);
            listProjectilePool.Add(_projectile);
        }

        _projectileThrowHolder = new GameObject("--- Projectile Throw Pool");
        _projectileThrowHolder.transform.position = Vector2.zero;
        for (int index = 0; index <= initialAmountProjectilesThrow; index++)
        {
            Projectile _projectileThrow = Instantiate(projectileThrowPrefab);
            _projectileThrow.transform.SetParent(_projectileThrowHolder.transform);
            _projectileThrow.gameObject.SetActive(false);
            listProjectileThrowPool.Add(_projectileThrow);
        }

        _projectileInDirHolder = new GameObject("--- Projectile In Dir Pool");
        _projectileInDirHolder.transform.position = Vector2.zero;
        for (int index = 0; index <= initialAmountProjectilesInDir; index++)
        {
            Projectile _projectileInDir = Instantiate(projectileInDirPrefab);
            _projectileInDir.transform.SetParent(_projectileInDirHolder.transform);
            _projectileInDir.gameObject.SetActive(false);
            listProjectileInDirPool.Add(_projectileInDir);
        }

        _thornsHolder = new GameObject("--- Thorns Pool");
        _thornsHolder.transform.position = Vector2.zero;
        for (int index = 0; index <= initialAmountThorns; index++)
        {
            BossThorn _thorns = Instantiate(thornPrefab);
            _thorns.transform.SetParent(_thornsHolder.transform);
            _thorns.gameObject.SetActive(false);
            listThornPool.Add(_thorns);
        }
    }

    public Projectile TryToGetProjectile()
    {
        Projectile _toReturn = null;

        for (int index = 0; index < listProjectilePool.Count; index++)
        {
            Projectile _possibleProjectile = listProjectilePool[index];
            if (!_possibleProjectile.gameObject.activeSelf)
            {
                _toReturn = _possibleProjectile;
                break;
            }
        }

        if (_toReturn == null)
        {
            _toReturn = Instantiate(projectilePrefab);
            _toReturn.transform.SetParent(_projectilesHolder.transform);
            listProjectilePool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }

    public Projectile TryToGetProjectilesThrow()
    {
        Projectile _toReturn = null;

        for (int index = 0; index < listProjectileThrowPool.Count; index++)
        {
            Projectile _possibleProjectilesThrow = listProjectileThrowPool[index];
            if (!_possibleProjectilesThrow.gameObject.activeSelf)
            {
                _toReturn = _possibleProjectilesThrow;
                break;
            }
        }

        if (_toReturn == null)
        {
            _toReturn = Instantiate(projectileThrowPrefab);
            _toReturn.transform.SetParent(_projectileThrowHolder.transform);
            listProjectileThrowPool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }

    public Projectile TryToGetProjectilesInDir()
    {
        Projectile _toReturn = null;

        for (int index = 0; index < listProjectileInDirPool.Count; index++)
        {
            Projectile _possibleProjectilesInDir = listProjectileInDirPool[index];
            if (!_possibleProjectilesInDir.gameObject.activeSelf)
            {
                _toReturn = _possibleProjectilesInDir;
                break;
            }
        }

        if (_toReturn == null)
        {
            _toReturn = Instantiate(projectileInDirPrefab);
            _toReturn.transform.SetParent(_projectileInDirHolder.transform);
            listProjectileInDirPool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }

    public BossThorn TryToGetThorn()
    {
        BossThorn _toReturn = null;

        for (int index = 0; index < listThornPool.Count; index++)
        {
            BossThorn _possibleThorn = listThornPool[index];
            if (!_possibleThorn.gameObject.activeSelf)
            {
                _toReturn = _possibleThorn;
                break;
            }
        }

        if (_toReturn == null)
        {
            _toReturn = Instantiate(thornPrefab);
            _toReturn.transform.SetParent(_thornsHolder.transform);
            listThornPool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }
}
