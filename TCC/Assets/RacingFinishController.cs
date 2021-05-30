using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class RacingFinishController : MonoBehaviourPunCallbacks
{
    public PhotonView m_PhotonView;
    public Transform prize;
    public float rotationSpeed;
    public bool isRaceOver = false;
    public bool winner = false;
    public GameObject finishPanel;
    public Text finalText;

    public bool WinnerOnOff = true;
    public GameObject WinnerRef;
    // Update is called once per frame
    void Update()
    {
        //if(isRaceOver) {
        //    if (winner)
        //    {
        //        finalText.text = "You won!";
        //    }
        //    else {
        //        finalText.text = "You lose!";
        //    }
        //    return;
        //}
        RotatePrize();
    }

    [PunRPC]
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && WinnerOnOff && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            WinnerRef = other.gameObject;
            finalText.text = WinnerRef.name + " You won!";
            WinnerOnOff = false;
            Debug.Log("Player chegou ao final: " + WinnerRef.name);
        }

        if (!WinnerOnOff && other.gameObject.GetComponent<PhotonView>().IsMine)
        {
            finalText.text = WinnerRef.name + " Winner!!";
        }

        if (!WinnerOnOff && other.gameObject.GetComponent<PhotonView>().IsMine && WinnerRef != this.gameObject)
        {
            finalText.text = WinnerRef.name + " you lose!!";
        }

        if(!isRaceOver)
        {
            m_PhotonView.RPC("RaceOver", RpcTarget.All);
        }
    }

    private void RotatePrize() {
        prize.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    //RPC Functions
    [PunRPC]
    public void RaceOver() {
        isRaceOver = true;
        finishPanel.SetActive(isRaceOver);
        if(m_PhotonView.IsMine) {
            winner = true;
        }
    }
}
