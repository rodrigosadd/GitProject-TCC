using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRuntimePersistentDataComponent : MonoBehaviour
{
    public GameObject referencePrefab;

    public InRuntimePersistentComponentInfo CacheValues(Vector3 lastPosition, Vector3 lastRotation)
    {
        return new InRuntimePersistentComponentInfo(gameObject.name ,lastPosition, lastRotation, referencePrefab == null? null : referencePrefab);
    }
}