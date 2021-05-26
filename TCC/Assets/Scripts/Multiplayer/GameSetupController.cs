using Photon.Pun;
using System.IO;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameSetupController : MonoBehaviour
{
    public List<Transform> spawnPoints;
    public float initTimer = 30.0f;
    public Text counterText;
    public GameObject counterPanel;
    private int playersNumber = 0;
    private bool readyToCount = false;
    private float counter = 0;
    public static bool isGameReady = false; //Controls when the game is ready for everyone in the room.
    void Start()
    {
        CreatePlayer(); //Creates a network player object for each player that loads into the room.
    }

    void Update()
    {
        CountBeforeStart();
        if (isGameReady) { //Call game functions inside here.

        }
    }

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

    private void CountBeforeStart() {
        if(readyToCount) {
            if(counter < initTimer) {
                counter += Time.deltaTime;
                counterText.text = Mathf.RoundToInt(initTimer - counter).ToString();
            }
            else {
                if(!isGameReady) {
                    StartGame();
                }
            }
        }
    }

    private void StartGame() { //Start the racing.
        isGameReady = true;
    }
}
