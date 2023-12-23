using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class KillCount : MonoBehaviour
{
public List<Kills> highestKills = new List<Kills>();
public Text[] names;
public Text[] killAmts;
private GameObject killCountPanel;
private GameObject namesObject;
private bool killCountOn = false;
// Start is called before the first frame update
void Start()
{
killCountPanel = GameObject.Find("KillCountPanel");
namesObject = GameObject.Find("NamesBG");
killCountPanel.SetActive(false);
}
// Update is called once per frame
void Update()
{
if (Input.GetKeyDown(KeyCode.K))
{
if (killCountOn == false)
{
killCountPanel.SetActive(true);
killCountOn = true;
highestKills.Clear();
for (int i = 0; i < names.Length; i++)
{
highestKills.Add(new Kills
(namesObject.GetComponent<NickNamesScript>().names
[i].text, Random.Range(1, 2900)));
}
highestKills.Sort();
for (int i = 0; i < names.Length; i++)
{
names[i].text = highestKills[i].playerName;
killAmts[i].text = highestKills[i].playerKills.ToString
();
}
for (int i = 0; i < names.Length; i++)
{
if (names[i].text == "Name")
{
names[i].text = "";
killAmts[i].text = "";
}

}
}
else if (killCountOn == true
)
{
killCountPanel.SetActive(false);
killCountOn = false
;
}
}
}
}