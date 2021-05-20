using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LOB/scriptableObjects/In Runtime Persistent Data", fileName = "In Runtime Persistent Data")]
public class InRuntimePersistentData : ScriptableObject
{
    public static bool blockLoad;    
    public List<InRuntimePersistentComponentInfo> cachedPersistenteComponentInfo;
    public int lastLoadedLevel;

    const string path = "ScriptableObjects";

    static InRuntimePersistentData _instance = null;
    public static InRuntimePersistentData Instance
    {
        get
        {
            var items = Resources.LoadAll<InRuntimePersistentData>(path);

            if (items.Length > 0)
            {
                foreach (var item in items) 
                {
                    if (item.GetType() == typeof(InRuntimePersistentData))
                    {
                        _instance = item;
                        break;
                    }
                }
                return _instance;
            }

            Debug.Log(typeof(InRuntimePersistentData).Name + $"There is no singleton scriptable object of type {(typeof(InRuntimePersistentData))}");

            return null;
        }
    }

    public static void CachePersistenteComponents(Vector3 playerPosition)
    {    
        if(_instance == null)
        {          
            _instance = Instance;
        }

        if(_instance.lastLoadedLevel != -1)
        {
            return;
        }

        var itens = GameObject.FindObjectsOfType<InRuntimePersistentDataComponent>();      
        var levelManager = GameObject.FindObjectOfType<LevelManager>();

        foreach (var item in itens)
        {
            var position = item.name == levelManager.inScenePlayer.name? playerPosition : item.transform.position;
            _instance.cachedPersistenteComponentInfo.Add(item.CacheValues(position, item.transform.eulerAngles));
        }
    }
}
