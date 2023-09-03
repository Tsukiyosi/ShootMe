using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField] public List<GameObject> playerPrefs;
    private Vector2 spawnPosition = new Vector2(-1f, 0f);

    void Start()
    {
        PhotonNetwork.Instantiate(playerPrefs[Random.Range(0, playerPrefs.Count - 1)].name, spawnPosition, Quaternion.identity);
    }
}
