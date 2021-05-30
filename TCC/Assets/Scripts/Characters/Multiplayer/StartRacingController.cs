using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class StartRacingController : MonoBehaviourPunCallbacks
{
    public List<GameObject> List_Player;
    public float timeCount;
    bool countStart;

    public void OnPlayersInScene()
    {
        GameObject playerRef = GameObject.FindGameObjectWithTag("Player");

        if (!List_Player.Contains(playerRef))
        {
            List_Player.Add(playerRef);
            Debug.Log("Jogador: " + playerRef.name + ", entrou!\nQuantidade de jogadores: " + List_Player.Count);
            timeCount = 30;

            if (List_Player.Count > 1)
            {
                countStart = true;
            }
        }
    }

    public void Update()
    {
        OnPlayersInScene();
        CheckCountStart();
    }

    public void CheckCountStart()
    {
        if (countStart)
        {
            timeCount = timeCount - 1 * Time.deltaTime;
            Debug.Log("Tempo: " + timeCount);
            if (timeCount <= 0)
            {
                for (int x =0; x < List_Player.Count; x++)
                {
                    Debug.Log("Startar jogo para: " + List_Player[x].name);
                }
            }

        }
        else
        {
            Debug.Log("Não há jogadores suficientes");
        }
    }
}
