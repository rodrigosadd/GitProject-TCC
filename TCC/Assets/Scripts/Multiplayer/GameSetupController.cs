using Photon.Pun;
using System.IO;
using UnityEngine;

public class GameSetupController : MonoBehaviour
{
    void Start()
    {
        CreatePlayer(); //Creates a network player object for each player that loads into the room.
    }

    private void CreatePlayer() {
        Debug.Log("Creating player.");
        PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"), Vector3.up * 10, Quaternion.identity);
    }
}
