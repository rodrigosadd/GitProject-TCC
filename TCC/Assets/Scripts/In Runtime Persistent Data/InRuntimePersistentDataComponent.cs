using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InRuntimePersistentDataComponent : MonoBehaviour
{
    public GameObject referencePrefab;

    public InRuntimePersistenteComponentInfo CacheValues(Vector3 lastPosition, Vector3 lastRotation)
    {
        return new InRuntimePersistenteComponentInfo(gameObject.name ,lastPosition, lastRotation, referencePrefab == null? null : referencePrefab);
    }
}