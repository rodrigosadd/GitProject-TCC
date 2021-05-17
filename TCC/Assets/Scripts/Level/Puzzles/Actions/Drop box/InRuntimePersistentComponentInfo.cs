using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InRuntimePersistentComponentInfo
{
    public string cachedObjectName;
    public GameObject referencePrefab;
    public Vector3 lastPosition;
    public Vector3 lastRotation;

    public InRuntimePersistentComponentInfo(string _cachedObjectName, Vector3 _lastPosition, Vector3 _lastRotation, GameObject _referencePrefab = null)
    {
        cachedObjectName = _cachedObjectName;
        lastPosition = _lastPosition;
        lastRotation = _lastRotation;

        if(_referencePrefab != null)
        {
            referencePrefab = _referencePrefab;
        }
    }
}