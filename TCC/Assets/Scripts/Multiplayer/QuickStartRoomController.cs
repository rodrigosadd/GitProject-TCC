using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class QuickStartRoomController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private int multiplayerSceneIndex; //Number of the build index of multiplayer scene.

    public override void OnEnable() {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public override void OnDisable() {
        PhotonNetwork.RemoveCallbackTarget(this);
    }

    public override void OnJoinedRoom() { //Callback function for when we successfully join a room.
        Debug.Log("Joined room.");
        StartGame();
    }

    public void StartGame() { //Function for loading the multiplayer scene.
        if (PhotonNetwork.IsMasterClient) {
            Debug.Log("Starting game...");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
    }
}
