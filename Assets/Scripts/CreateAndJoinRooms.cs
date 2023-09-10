using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class CreateAndJoinRooms : MonoBehaviourPunCallbacks
{
    public TMP_InputField createInputf, joinInputf;
    public TextMeshProUGUI createInput, joinInput;

    public void setName(){
        createInput.text = createInputf.text;
        joinInput.text = joinInputf.text;
    }
    
    public void CreateRoom()
    {
        setName();
        PhotonNetwork.CreateRoom(createInput.text);
    }
    public void JoinRoom()
    {
        setName();
        PhotonNetwork.JoinRoom(joinInput.text);
    }
    public override void OnJoinedRoom()
    {
        PhotonNetwork.LoadLevel("Game");
    }



   
}
