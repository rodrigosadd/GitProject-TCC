using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class GameManager_Demo_ENDM : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerPrefab_Generic;




    void Start()
    {
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (playerPrefab_Generic != null)
            {
                float x = Random.Range(-2,2);
                float z = Random.Range(-2, 2);
                GameObject gameRef = PhotonNetwork.Instantiate(playerPrefab_Generic.name, new Vector3(x,1,z), Quaternion.identity);
                string randomName = "Nick: " + Random.RandomRange(100, 1000);
                gameRef.GetComponentInChildren<Text>().text = randomName;
                Destroy(this.gameObject);
                Debug.Log("Instanciado " + PhotonNetwork.NickName);
            }

            else
            {
                Debug.Log("Place playerprefabs");
            }
            //Att_Info();
        }
    }

    // public void Att_Info()
    // {
    //     name_01.text = PhotonNetwork.LocalPlayer.NickName;
    //     name_02.text = name_01.text;
    //     float randomCoins = Random.Range(100, 7654);
    //     Debug.Log(PhotonNetwork.LocalPlayer.NickName);
    // }
}
