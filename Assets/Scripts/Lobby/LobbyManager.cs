using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    #region Variables
    [SerializeField] TMP_InputField createLobbyInput;
    [SerializeField] TMP_InputField joinLobbyInput;
    #endregion
    #region Public methods
    public void CreateLobby(){
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        PhotonNetwork.CreateRoom(createLobbyInput.text, roomOptions);
    }
    public void JoinLobby(){
        PhotonNetwork.JoinRoom(joinLobbyInput.text);
    }

    public override void OnJoinedRoom(){
        PhotonNetwork.LoadLevel("Game");
    }
    #endregion
    
}
