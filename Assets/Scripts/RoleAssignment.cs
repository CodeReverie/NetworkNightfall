using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System.Collections.Generic;

public class RoleAssignment : MonoBehaviourPunCallbacks
{
    public enum PlayerRole { Role1, Role2, Role3 }

    private Dictionary<int, PlayerRole> playerRoles = new Dictionary<int, PlayerRole>();
    private Dictionary<int, int> playerLives = new Dictionary<int, int>();

    private void Start()
    {
        // Make sure to only run this code for the MasterClient
        if (!PhotonNetwork.IsMasterClient) return;

        // Get all players in the room
        Player[] players = PhotonNetwork.PlayerList;
        List<PlayerRole> availableRoles = new List<PlayerRole>((PlayerRole[])System.Enum.GetValues(typeof(PlayerRole)));

        foreach (Player player in players)
        {
            // Assign a random role to each player
            int randomIndex = Random.Range(0, availableRoles.Count);
            PlayerRole assignedRole = availableRoles[randomIndex];
            playerRoles.Add(player.ActorNumber, assignedRole);
            availableRoles.RemoveAt(randomIndex);

            // Set initial life value to 1 for each player
            playerLives.Add(player.ActorNumber, 1);

            // Send RPC to synchronize the role and life value for each player
            photonView.RPC("SyncPlayerData", player, assignedRole, 1);
        }
    }

    // Called when a player presses another player's button
    public void OnButtonPress(int targetPlayerID)
    {
        if (!PhotonNetwork.IsMasterClient) return;

        // Get the role and life value of the player who pressed the button
        int currentPlayerID = PhotonNetwork.LocalPlayer.ActorNumber;
        PlayerRole currentPlayerRole = playerRoles[currentPlayerID];
        int currentPlayerLife = playerLives[currentPlayerID];

        // Get the role and life value of the target player whose button was pressed
        PlayerRole targetPlayerRole = playerRoles[targetPlayerID];
        int targetPlayerLife = playerLives[targetPlayerID];

        // Check if the target player can be interacted with based on roles and life value
        if (currentPlayerRole == PlayerRole.Role1 && targetPlayerLife == 1)
        {
            // Increase target player's life by 1
            targetPlayerLife++;
        }
        else if (currentPlayerRole == PlayerRole.Role2 && targetPlayerLife == 1)
        {
            // Decrease target player's life by 1
            targetPlayerLife--;
        }

        // Send RPC to synchronize the updated life value for the target player
        photonView.RPC("SyncPlayerLife", RpcTarget.All, targetPlayerID, targetPlayerLife);
    }

    // RPC to synchronize player role and life data
    [PunRPC]
    private void SyncPlayerData(PlayerRole role, int life, PhotonMessageInfo info)
    {
        playerRoles[info.Sender.ActorNumber] = role;
        playerLives[info.Sender.ActorNumber] = life;
    }

    // RPC to synchronize player life data
    [PunRPC]
    private void SyncPlayerLife(int playerID, int life, PhotonMessageInfo info)
    {
        playerLives[playerID] = life;
    }
}
