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

    public GameObject gameBlock;

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
                PlayerControllerMultiplayer _controller = gameRef.GetComponent<PlayerControllerMultiplayer>();
                _controller.photon.m_Manager = this;
                _controller.photon.m_RacingPosition = spawnPoints[_spawn];
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
            gameBlock.SetActive(false);
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

    public void LeaveGame() {
        PhotonNetwork.LeaveRoom();
    }
}
