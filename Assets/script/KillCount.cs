using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KillCount : MonoBehaviour {
public List
<Kills> highestKills = new List
<Kills>();
public Text[] names;
public Text[] killAmts;
private GameObject killCountPanel;
private GameObject namesObject;
private bool killCountOn = false
;
public bool countDown = true
;
public GameObject winnerPanel;
public Text winnerText;
// Start is called before the first frame update
    void Start()
    {
        killCountPanel = GameObject.Find("KillCountPanel");
        namesObject = GameObject.Find("NamesBG");
        killCountPanel.SetActive(false);
        winnerPanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K) && countDown == true)
        {
            if (killCountOn == false)
            {
                ShowKillCountPanel();
            }
            else if (killCountOn == true)
            {
                HideKillCountPanel();
            }
        }
    }

    void ShowKillCountPanel()
    {
        killCountPanel.SetActive(true);
        killCountOn = true;
        highestKills.Clear();

        for (int i = 0; i < names.Length; i++)
        {
            highestKills.Add(new Kills(namesObject.GetComponent<NickNamesScript>().names[i].text,
                                       namesObject.GetComponent<NickNamesScript>().kills[i]));
        }

        highestKills.Sort();

        for (int i = 0; i < names.Length; i++)
        {
            names[i].text = highestKills[i].playerName;
            killAmts[i].text = highestKills[i].playerKills.ToString();
        }

        for (int i = 0; i < names.Length; i++)
        {
            if (names[i].text == "name")
            {
                names[i].text = "";
                killAmts[i].text = "";
            }
        }

        StartCoroutine(WaitToShowWinnerPanel());
    }

    IEnumerator WaitToShowWinnerPanel()
    {
        yield return new WaitForSeconds(30f); // Wait for 30 seconds
        TimeOver(); // Show the winner panel
        LeaveRoom(); // Leave the room and return to the lobby
    }

    void LeaveRoom()
    {
        Photon.Pun.PhotonNetwork.LeaveRoom();
    }

    void HideKillCountPanel()
    {
        killCountPanel.SetActive(false);
        killCountOn = false;
    }
public void TimeOver()
{
killCountPanel.SetActive(true);
winnerPanel.SetActive(true);
killCountOn = true;
highestKills.Clear();
for (int i = 0; i < names.Length; i++)
{
highestKills.Add(new Kills(namesObject.GetComponent<NickNamesScript>().names[i].text,
namesObject.GetComponent<NickNamesScript>().kills[i]));
}
highestKills.Sort();
winnerText.text = highestKills[0].playerName;
for (int i = 0; i < names.Length; i++)
{
names[i].text = highestKills[i].playerName;
killAmts[i].text = highestKills[i].playerKills.ToString();
}
for (int i = 0; i < names.Length; i++)
{
if (names[i].text == "name")
{
names[i].text = "";
killAmts[i].text = "";
}
}
}
public void NoRespawnWinner(string name)
{
winnerPanel.SetActive(true);
winnerText.text = name;
}
}