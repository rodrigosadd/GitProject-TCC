using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolSystem : MonoBehaviour
{
    [Header("Fruit Pool variables")]
    public List<Projectile> listProjectilePool;
    public Projectile projectilePrefab;
    public int initialAmountProjectiles;
    private GameObject _projectilesHolder;

    [Header("Fruit Pool variables")]
    public List<Projectile> listProjectileAbovePool;
    public Projectile projectileAbovePrefab;
    public int initialAmountProjectilesAbove;
    private GameObject _projectilesAboveHolder;

    void Start()
    {
        InitializePool();
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

        _projectilesAboveHolder = new GameObject("--- Projectiles Above Pool");
        _projectilesAboveHolder.transform.position = Vector2.zero;
        for (int index = 0; index <= initialAmountProjectilesAbove; index++)
        {
            Projectile _projectileAbove = Instantiate(projectileAbovePrefab);
            _projectileAbove.transform.SetParent(_projectilesAboveHolder.transform);
            _projectileAbove.gameObject.SetActive(false);
            listProjectileAbovePool.Add(_projectileAbove);
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

    public Projectile TryToGetProjectileAbove()
    {
        Projectile _toReturn = null;

        for (int index = 0; index < listProjectileAbovePool.Count; index++)
        {
            Projectile _possibleProjectileAbove = listProjectileAbovePool[index];
            if (!_possibleProjectileAbove.gameObject.activeSelf)
            {
                _toReturn = _possibleProjectileAbove;
                break;
            }
        }

        if (_toReturn == null)
        {
            _toReturn = Instantiate(projectileAbovePrefab);
            _toReturn.transform.SetParent(_projectilesAboveHolder.transform);
            listProjectileAbovePool.Add(_toReturn);
        }

        _toReturn.gameObject.SetActive(true);

        return _toReturn;
    }
}
