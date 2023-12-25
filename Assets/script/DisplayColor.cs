using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEditor.Rendering;
using Photon.Realtime;
public class DisplayColor : MonoBehaviourPunCallbacks
{
public int[] buttonNumbers;
public int[] viewID;
public Color32[] colors;
private GameObject namesObject;
private GameObject waitForPlayers;
public AudioClip[] gunShotSounds;
private void Start()
{
namesObject = GameObject.Find("NamesBG");
waitForPlayers = GameObject.Find("WaitingBG");
}
private void Update()
{
if (Input.GetKeyDown(KeyCode.Escape))
{
if (GetComponent<PhotonView>().IsMine == true &&
waitForPlayers.activeInHierarchy == false)
{
RemoveData();
RoomExit();
}
}
}
void RemoveData()
{
GetComponent<PhotonView>().RPC("RemoveMe", RpcTarget.AllBuffered);
}
void RoomExit()
{
StartCoroutine(GetReadyToLeave());
}
public void ChooseColor()
{
GetComponent<PhotonView>().RPC("AssignColor",
RpcTarget.AllBuffered);
}
public void PlayGunShot(string name, int weaponNumber)
{
GetComponent<PhotonView>().RPC("PlaySound", RpcTarget.All, name,
weaponNumber);
}
[PunRPC]
void PlaySound(string name, int weaponNumber)
{
for (int i = 0; i < namesObject.GetComponent<NickNamesScript>
().names.Length; i++)
{
if (name == namesObject.GetComponent<NickNamesScript>().names
[i].text)
{
GetComponent<AudioSource>().clip = gunShotSounds
[weaponNumber];
GetComponent<AudioSource>().Play();
}
}
}
[PunRPC]
void AssignColor()
{
for (int i = 0; i < viewID.Length; i++)
{
if (this.GetComponent<PhotonView>().ViewID == viewID[i])
{
this.transform.GetChild(1).GetComponent<Renderer>
().material.color = colors[i];
namesObject.GetComponent<NickNamesScript>().names
[i].gameObject.SetActive(true);
namesObject.GetComponent<NickNamesScript>().healthbars
[i].gameObject.SetActive(true);
namesObject.GetComponent<NickNamesScript>().names[i].text =
this.GetComponent<PhotonView>().Owner.NickName;
}
}
}
[PunRPC]
void RemoveMe()
{
for (int i = 0; i <
namesObject.gameObject.GetComponent<NickNamesScript>
().names.Length; i++)
{
if (this.GetComponent<PhotonView>().Owner.NickName ==
namesObject.GetComponent<NickNamesScript>().names[i].text)
{
namesObject.GetComponent<NickNamesScript>().names
[i].gameObject.SetActive(false);
namesObject.GetComponent<NickNamesScript>().healthbars
[i].gameObject.SetActive(false);
}
}
}
IEnumerator GetReadyToLeave()
{
yield return new WaitForSeconds(1);
namesObject.GetComponent<NickNamesScript>().Leaving();
Cursor.visible = true;
PhotonNetwork.LeaveRoom();
}
}