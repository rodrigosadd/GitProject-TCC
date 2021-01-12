using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
     public static GameManager instance;

     public SettingsData instanceSettingsData;

     void Awake()
     {
          instance = this;
          DontDestroyOnLoad(gameObject);
     }
}
