using Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

[RequireComponent(typeof(PhotonView))]
public class MultiplayerController : MonoBehaviourPunCallbacks
{
    public List<Transform> spawnPoints;
    public float initTimer = 30.0f;
    public Text counterText;
    public GameObject counterPanel;
    private int playersNumber = 0;
    private bool readyToCount = false;
    private float counter = 0;
    private PhotonView photon;
    public bool isGameReady = false; //Controls when the game is ready for everyone in the room.
    void Start()
    {
        photon = GetComponent<PhotonView>();
        photon.RPC("CreatePlayer", RpcTarget.AllBuffered); //Creates a network player object for each player that loads into the room.
    }

    void Update()
    {
        photon.RPC("CountBeforeStart", RpcTarget.AllBuffered);
        if(PhotonNetwork.PlayerList.Length > 1) {
            photon.RPC("GetReady", RpcTarget.All);
        }
        if (isGameReady) { //Call game functions inside here.
            //TODO
        }
    }

    [PunRPC]
    public void CreatePlayer() {
        Debug.Log("Creating player.");
        int randomNumber = Random.Range(0, spawnPoints.Count);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), spawnPoints[randomNumber].position + Vector3.up * 10, Quaternion.identity);
        spawnPoints.Remove(spawnPoints[randomNumber]);
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
}
