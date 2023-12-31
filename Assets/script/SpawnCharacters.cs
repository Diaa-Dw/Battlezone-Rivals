using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class SpawnCharacters : MonoBehaviour
{
public GameObject character;
public Transform[] spawnPoints;
public GameObject[] weapons;
public Transform[] weaponSpawnPoints;
public float weaponRespawnTime = 10;
// Start is called before the first frame update
void Start()
{
if (PhotonNetwork.IsConnected)
{
    StartCoroutine(SpawnPlayer());
}
}
private IEnumerator SpawnPlayer()
{
    yield return new WaitForSeconds(2);

    // Check if spawnPoints is not null and has elements
    if (spawnPoints != null && spawnPoints.Length > 0)
    {
        PhotonNetwork.Instantiate(
            character.name,
            spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].position,
            spawnPoints[PhotonNetwork.CurrentRoom.PlayerCount - 1].rotation
        );
    }
    else
    {
        Debug.LogError("SpawnPoints array is not assigned or empty!");
    }
}
// Update is called once per frame
 
public void SpawnWeaponsStart()
{
for (int i = 0; i < weapons.Length; i++)
{
PhotonNetwork.Instantiate(weapons[i].name, weaponSpawnPoints[i].position, weaponSpawnPoints[i].rotation);
}
}
}