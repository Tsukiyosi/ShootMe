using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ConnectToServer : MonoBehaviourPunCallbacks
{
    #region MonoBehaviour Callbacks   
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    #endregion
    #region Pun Callbacks
    public override void OnConnectedToMaster(){
        SceneManager.LoadScene("Lobby");
    }
    #endregion
}
