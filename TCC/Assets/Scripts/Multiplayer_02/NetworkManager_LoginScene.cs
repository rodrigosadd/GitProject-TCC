using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class NetworkManager_LoginScene : MonoBehaviourPunCallbacks
{

    public GameObject Player_prefabs;
    string playerName;
    public void Start()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.AutomaticallySyncScene = true;
        }
        playerName = Random.Range(100, 1000).ToString();
    }

    public void OnLoginButtonClicked()
    {
            PhotonNetwork.ConnectUsingSettings();
    }

    public void OnJoinRandomRoom()
    {
        PhotonNetwork.JoinRandomRoom();  
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log(message);

        string roomName = "Room " + Random.Range(1000, 10000);

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 6;

        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }


    public override void OnConnected()
    {
        Debug.Log("Esta conectado");
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " is created.");
        PhotonNetwork.LoadLevel(7);
    }

    public void OnJoinRandomRoomButtonClicked()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " is joined.");
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(0);
    }
}
