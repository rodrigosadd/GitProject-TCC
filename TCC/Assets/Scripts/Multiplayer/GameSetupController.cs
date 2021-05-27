using Photon.Pun;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

[RequireComponent(typeof(PhotonView))]
public class GameSetupController : MonoBehaviourPun
{
    public List<Transform> spawnPoints;
    public float initTimer = 30.0f;
    public Text counterText;
    public GameObject counterPanel;
    private int playersNumber = 0;
    private bool readyToCount = false;
    private float counter = 0;
    private PhotonView photon;
    public static bool isGameReady = false; //Controls when the game is ready for everyone in the room.
    void Start()
    {
        photon = GetComponent<PhotonView>();
        photon.RPC("CreatePlayer", RPCTarget.AllBuffered); //Creates a network player object for each player that loads into the room.
    }

    void Update()
    {
        photon.RPC("CountBeforeStart", RPCTarget.AllBuffered);
        if (isGameReady) { //Call game functions inside here.

        }
    }

    [PunRPC]
    private void CreatePlayer() {
        Debug.Log("Creating player.");
        int randomNumber = Random.Range(0, spawnPoints.Count);
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), spawnPoints[randomNumber].position + Vector3.up * 10, Quaternion.identity);
        spawnPoints.Remove(spawnPoints[randomNumber]);
        playersNumber++;
        if(playersNumber >= 2) {
            initTimer = 30.0f;
            readyToCount = true;
        }
    }

    [PunRPC]
    private void CountBeforeStart() {
        if(readyToCount) {
            if(counter < initTimer) {
                counter += Time.deltaTime;
                counterText.text = "Initializing in " + Mathf.RoundToInt(initTimer - counter).ToString() + " seconds, get ready!";
            }
            else {
                if(!isGameReady) {
                    counterText.text = "Start!";
                    photon.RPC("StartGame", RPCTarget.AllBuffered);
                }
            }
        }
        else {
            counterText.text = "Waiting for players to join...";
        }
    }

    [PunRPC]
    private void StartGame() { //Start the racing.
        isGameReady = true;
    }

    public void PanelSlideAnimation(string mode) {
        switch (mode)
        {
            
            // default:
        }
    }
}
