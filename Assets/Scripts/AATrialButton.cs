using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class AATrialButton : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI[] numberTexts;
    private int[] counters;
    private PhotonView view;
    private PlayerDevice currentPlayerDevice;
    private Dictionary<int, int> playerButtonMap; // Map player IDs to button indices

    public enum PlayerDevice
    {
        Phone1,
        Phone2,
        Phone3,
        Phone4,
        Phone5,
        Phone6
    }

    void Start()
    {
        view = GetComponent<PhotonView>();
        counters = new int[numberTexts.Length];
        playerButtonMap = new Dictionary<int, int>();


       List<int> availableButtonIndices = new List<int>();
            for (int i = 0; i < numberTexts.Length; i++)
            {
                availableButtonIndices.Add(i);
                counters[i] = 1;
                numberTexts[i].text = counters[i].ToString();
            }

         for (int i = 1; i <= 6; i++)
    {
        int randomButtonIndex = Random.Range(0, availableButtonIndices.Count);
        int playerID = i; // Adjust this if your player IDs don't start from 1
        int buttonIndex = availableButtonIndices[randomButtonIndex];
        
        playerButtonMap.Add(playerID, buttonIndex);
        availableButtonIndices.RemoveAt(randomButtonIndex);
    }

        if (PhotonNetwork.IsConnected && view.IsMine)
        {
            int actorNumber = PhotonNetwork.LocalPlayer.ActorNumber;
            if (playerButtonMap.ContainsKey(actorNumber))
            {
                currentPlayerDevice = (PlayerDevice)playerButtonMap[actorNumber];
            }
            else
            {
                // Handle the case where player's button is not assigned
            }
            // Check if all 6 players are in the room
            if (PhotonNetwork.PlayerList.Length >= 6)
            {
            // Close the room
            PhotonNetwork.CurrentRoom.IsOpen = false;
            }
        }
    }

    private bool IsActionButtonEnabled(int buttonIndex)
    {
        // Logic to determine if the action button is enabled based on the current player's device
        switch (currentPlayerDevice)
        {
            case PlayerDevice.Phone1:
                return counters[buttonIndex] > 1; // Player 1 can only increase counter if it's > 0
            case PlayerDevice.Phone2:
                return counters[buttonIndex] == 1; // Player 2 can only decrease counter if it's > 0
            case PlayerDevice.Phone3:
                return counters[buttonIndex] == 0; // Player 3 can only increase if counter is 0
            case PlayerDevice.Phone4:
                // Implement logic to copy ability of the player
                return true; // Adjust this condition based on copied ability
            case PlayerDevice.Phone5:
                return true; // Player 5 can disable buttons
            case PlayerDevice.Phone6:
                return true; // Player 6 can reveal player's counter
            default:
                return false;
        }
    }

    public void OnButtonPress(int buttonIndex)
    {
        if (PhotonNetwork.IsConnected)
        {
            if (view.IsMine && IsActionButtonEnabled(buttonIndex) && currentPlayerDevice != (PlayerDevice)buttonIndex)
            {
                 switch (currentPlayerDevice)
                {
                    case PlayerDevice.Phone1:
                        counters[buttonIndex]--;
                        break;
                    case PlayerDevice.Phone2:
                        counters[buttonIndex]++;
                        break;
                    case PlayerDevice.Phone3:
                        counters[buttonIndex]++;
                        break;
                    case PlayerDevice.Phone4:
                        // Implement logic to copy ability and perform action here
                        // For now, let's just increase the counter
                        counters[buttonIndex]++;
                        break;
                    case PlayerDevice.Phone5:
                        // Disable the button's interaction here
                        // For now, let's just increase the counter
                        counters[buttonIndex]++;
                        break;
                    case PlayerDevice.Phone6:
                        // Implement logic to copy ability and perform action here
                        // For now, let's just increase the counter
                        counters[buttonIndex]++;
                        break;
                }
                view.RPC("UpdateCount", RpcTarget.All, buttonIndex, counters[buttonIndex]);
            }
        }
        else
        {
            counters[buttonIndex]++;
            numberTexts[buttonIndex].text = counters[buttonIndex].ToString();
        }
    }

    [PunRPC]
    private void UpdateCount(int buttonIndex, int newCounter)
    {
        counters[buttonIndex] = newCounter;
        numberTexts[buttonIndex].text = newCounter.ToString();
    }
}