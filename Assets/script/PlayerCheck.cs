using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
public class PlayerCheck : MonoBehaviour
{
public int maxPlayersInRoom = 2;
public Text currentPlayers;
// Update is called once per frame
void Update()
{
if (PhotonNetwork.CurrentRoom.PlayerCount == maxPlayersInRoom)
{
PhotonNetwork.CurrentRoom.IsOpen = false;
this.gameObject.SetActive(false);
}
currentPlayers.text = PhotonNetwork.CurrentRoom.PlayerCount.ToString
() + "/" + maxPlayersInRoom.ToString();
}
}