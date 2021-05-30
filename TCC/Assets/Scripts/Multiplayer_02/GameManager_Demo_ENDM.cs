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
    public RectTransform panelToAnimate;
    public bool isGameReady = false; //Controls when the game is ready for everyone in the room.
    [Header("Racing Config:")]
    public float counter = 0;
    public float panelLimit;
    private bool readyToCount = false;
    private PhotonView photon;
    void Start()
    {
        panelToAnimate = counterPanel.GetComponent<RectTransform>();
        photon = GetComponent<PhotonView>();
        if (PhotonNetwork.IsConnectedAndReady)
        {
            if (playerPrefab_Generic != null)
            {
                int _spawn = Random.Range(0, spawnPoints.Count);
                GameObject gameRef = PhotonNetwork.Instantiate(playerPrefab_Generic.name, spawnPoints[_spawn].position, Quaternion.identity);
                string randomName = RandName();
                gameRef.GetComponentInChildren<Text>().text = randomName;
                PlayerControllerMultiplayer _controller = gameRef.GetComponent<PlayerControllerMultiplayer>();
                _controller.photon.m_Manager = this;
                _controller.photon.m_RacingPosition = spawnPoints[_spawn];
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
        if (isGameReady) {
            PanelSlideAnimation(); //Only first frame.
            counterText.text = "Start!";
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
    }

    [PunRPC]
    public void StartGame() { //Start the racing.
        isGameReady = true;
    }

    public void PanelSlideAnimation() {
        if(panelToAnimate.position.x > panelLimit) {
            panelToAnimate.position -= new Vector3(Time.deltaTime * 85, 0, 0);
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
