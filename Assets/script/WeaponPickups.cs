using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class WeaponPickups : MonoBehaviour
{
private AudioSource audioPlayer;
public float respawnTime = 5;
// Start is called before the first frame update
void Start()
{
audioPlayer = GetComponent<AudioSource>();
}
private void OnTriggerEnter(Collider other)
{
if (other.CompareTag("Player"))
{
this.GetComponent<PhotonView>().RPC("PlayPickupAudio",
RpcTarget.All);
this.GetComponent<PhotonView>().RPC("TurnOff", RpcTarget.All);
}
}
[PunRPC]
void PlayPickupAudio()
{
audioPlayer.Play();
}
[PunRPC]
void TurnOff()
{
this.transform.gameObject.GetComponent<Renderer>().enabled = false;
this.transform.gameObject.GetComponent<Collider>().enabled = false;
StartCoroutine(WaitToRespawn());
}
[PunRPC]
void TurnOn()
{
this.transform.gameObject.GetComponent<Renderer>().enabled = true;
this.transform.gameObject.GetComponent<Collider>().enabled = true;
}
IEnumerator WaitToRespawn()
{
yield return new WaitForSeconds(respawnTime);
this.GetComponent<PhotonView>().RPC("TurnOn", RpcTarget.All);
}
}