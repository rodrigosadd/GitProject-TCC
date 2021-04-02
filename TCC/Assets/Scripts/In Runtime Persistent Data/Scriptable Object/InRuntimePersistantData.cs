using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "LOB/scriptableObjects/In Runtime Persistant Data", fileName = "In Runtime Persistant Data")]
public class InRuntimePersistantData : ScriptableObject
{
    public List<InRuntimePersistenteComponentInfo> cachedPersistenteComponentInfo;
    public int lastLoadedLevel;

    const string path = "ScriptableObjects";

    static InRuntimePersistantData _instance = null;
    public static InRuntimePersistantData Instance
    {
        get
        {
            var items = Resources.LoadAll<InRuntimePersistantData>(path);

            if (items.Length > 0)
            {
                foreach (var item in items) 
                {
                    if (item.GetType() == typeof(InRuntimePersistantData))
                    {
                        _instance = item;
                        break;
                    }
                }
                return _instance;
            }

            Debug.Log(typeof(InRuntimePersistantData).Name + $"There is no singleton scriptable object of type {(typeof(InRuntimePersistantData))}");

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
