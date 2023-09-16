using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnPlayers : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        Vector2 rpos = new Vector2(Random.Range(10, 16), Random.Range(10, 26));
        PhotonNetwork.Instantiate (player.name, rpos, Quaternion.identity);
    }

    void Update()
    {
        
    }
}
