using UnityEngine;
using Photon.Pun;

public class PlayerButton : MonoBehaviourPunCallbacks
{
    private RoleAssignment roleAssignment;

    private void Start()
    {
        roleAssignment = GameObject.FindObjectOfType<RoleAssignment>();
    }

    public void OnButtonPress()
    {
        // Get the PhotonView for this GameObject
        PhotonView photonView = GetComponent<PhotonView>();

        // Call the OnButtonPress method on RoleAssignment script and pass the target player's PhotonViewID
        photonView.RPC("OnButtonPress", RpcTarget.MasterClient, photonView.ViewID);
    }
}