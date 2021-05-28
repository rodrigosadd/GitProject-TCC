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

    public void OnCreateRoomOrJoin()
    {
        
        string roomName = "Room: " + Random.Range(10,100);
        if (!string.IsNullOrEmpty(playerName))
        {
            PhotonNetwork.LocalPlayer.NickName = playerName;
            PhotonNetwork.JoinOrCreateRoom(roomName,new RoomOptions { MaxPlayers = 6 }, null);
        }
        
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("Return code: " + returnCode + ". Menssage: " + message);
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

    public override void OnJoinedRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name + " is joined.");
    }
}
