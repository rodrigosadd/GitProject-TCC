using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class QuickStartLobbyController : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private GameObject quickStartButton; //Button used for creating and joining a game.
    [SerializeField]
    private GameObject quickCancelButton; //Button used for stop searching for a game to join.
    [SerializeField]
    private int roomSize; //Button used for stop searching for a game to join.
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster() {
        Debug.Log("We are connected to the " + PhotonNetwork.CloudRegion + " server!");
        PhotonNetwork.AutomaticallySyncScene = true;
        quickStartButton.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message) { //Callback function for joining error.
        Debug.Log("Joining room failed.");
        Debug.Log(message);
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message) { //Callback function for creating room error.
        Debug.Log("Failed to create a room.");
        CreateRoom(); //Retry to create a room with different name.
    }

    private void CreateRoom() { //Try to create your own room.
        Debug.Log("Creating new room.");
        int randomNumber = Random.Range(0, 10000); //Random name for the room.
        RoomOptions options = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte) roomSize };
        PhotonNetwork.CreateRoom("Room " + randomNumber, options); //Attempting to create a new room.
        Debug.Log("Created Room " + randomNumber);
    }

    public void QuickStart() { //Paired with quick start button.
        quickStartButton.SetActive(false);
        quickCancelButton.SetActive(true);
        PhotonNetwork.JoinRandomRoom(); //First tries to join an existing room.
        Debug.Log("Quick start!");
    }

    public void QuickCancel() {
        quickCancelButton.SetActive(false);
        quickStartButton.SetActive(true);
        PhotonNetwork.LeaveRoom(); //Leaves the actual room.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
