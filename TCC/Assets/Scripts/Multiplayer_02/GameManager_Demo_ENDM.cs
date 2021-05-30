using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;

public class GameManager_Demo_ENDM : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject playerPrefab_Generic;
    public List<Transform> spawnPoints;
    public float initTimer = 30.0f;
    public Text counterText;
    public GameObject counterPanel;
    public bool isGameReady = false; //Controls when the game is ready for everyone in the room.
    private int playersNumber = 0;
    private bool readyToCount = false;
    private float counter = 0;
    private PhotonView photon;
    void Start()
    {
        photon = GetComponent<PhotonView>();
        if (PhotonNetwork.IsConnectedAndReady && photon.IsMine)
        {
            if (playerPrefab_Generic != null)
            {
                float x = Random.Range(-2,2);
                float z = Random.Range(-2, 2);
                GameObject gameRef = PhotonNetwork.Instantiate(playerPrefab_Generic.name, new Vector3(x,1,z), Quaternion.identity);
                string randomName = RandName();
                gameRef.GetComponentInChildren<Text>().text = randomName;
                //Destroy(this.gameObject);
                Debug.Log("Instanciado " + randomName);
            }

            else
            {
                Debug.Log("Place playerprefabs");
            }
        }
    }

    void Update()
    {
        if(photon.IsMine) {
            photon.RPC("CountBeforeStart", RpcTarget.AllBuffered);
            if(PhotonNetwork.PlayerList.Length > 1) {
                photon.RPC("GetReady", RpcTarget.All);
            }
            if (isGameReady) { //Call game functions inside here.
                //TODO
            }
        }
    }

    [PunRPC]
    public void CountBeforeStart() {
        if(readyToCount) {
            if(counter < initTimer) {
                counter += Time.deltaTime;
                counterText.text = "Initializing in " + Mathf.RoundToInt(initTimer - counter).ToString() + " seconds, get ready!";
            }
            else {
                if(!isGameReady) {
                    counterText.text = "Start!";
                    photon.RPC("StartGame", RpcTarget.AllBuffered);
                }
            }
        }
        else {
            counterText.text = "Waiting for players to join...";
        }
    }
    [PunRPC]
    public void GetReady() {
        readyToCount = true;
        counter = 0;
    }
    [PunRPC]
    public void StartGame() { //Start the racing.
        isGameReady = true;
    }

    public void PanelSlideAnimation(string mode) {
        switch (mode)
        {
            // default:
        }
    }

    private string RandName()
    {
        int randLastName = Random.Range(0, 6);
        string playerName = "";
        switch(randLastName) {
            case 0:
                playerName = "Beto the Legend";
                break;
            case 1:
                playerName = "Beto Crusher";
                break;
            case 2:
                playerName = "Beto Smasher";
                break;
            case 3:
                playerName = "Beto the Lord";
                break;
            case 4:
                playerName = "Beto the Fearless";
                break;
            case 5:
                playerName = "Beto the Hero";
                break;
        }
        return playerName;
    }
}
