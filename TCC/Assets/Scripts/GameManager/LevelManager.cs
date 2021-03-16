using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public InRuntimePersistentDataComponent [] inScenePersitenteComponente;
    public PlayerController inScenePlayer;

    void OnValidate()
    {
        inScenePersitenteComponente = GameObject.FindObjectsOfType<InRuntimePersistentDataComponent>(true);
        inScenePlayer = GameObject.FindObjectOfType<PlayerController>();
    }
}
