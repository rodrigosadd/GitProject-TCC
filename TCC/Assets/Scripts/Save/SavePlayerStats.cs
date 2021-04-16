using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavePlayerStats : MonoBehaviour
{
     void Start()
     {
          DontDestroyOnLoad(gameObject);
     }

     public void CleanPlayerStats()
     {
        Debug.Log("Clean Stats");
        PlayerPrefs.SetInt("IndexMaxJump", 1);
        PlayerPrefs.SetInt("IndexCanAttack", 0);
        PlayerPrefs.SetInt("IndexCanSeeTeleport", 0);
     }

     public void Save()
     {
        Debug.Log("Save Stats");
        PlayerPrefs.SetInt("IndexMaxJump", GameManager.instance.playerStatsData.maxJump);
        PlayerPrefs.SetInt("IndexCanAttack", GameManager.instance.playerStatsData.canAttack);
        PlayerPrefs.SetInt("IndexCanSeeTeleport", GameManager.instance.playerStatsData.canSeeTeleport);
     }

     public void Load()
     {
          Debug.Log("Load Stats");
          GameManager.instance.playerStatsData.maxJump = PlayerPrefs.GetInt("IndexMaxJump");
          GameManager.instance.playerStatsData.canAttack = PlayerPrefs.GetInt("IndexCanAttack");
          GameManager.instance.playerStatsData.canSeeTeleport = PlayerPrefs.GetInt("IndexCanSeeTeleport");
     }
}
