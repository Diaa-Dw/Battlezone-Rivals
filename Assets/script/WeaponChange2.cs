using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using Cinemachine;
using UnityEngine.UI;
public class WeaponChangeAdvanced : MonoBehaviour
{
public TwoBoneIKConstraint leftHand;
public TwoBoneIKConstraint rightHand;
public TwoBoneIKConstraint leftThumb;
private CinemachineVirtualCamera cam;
private GameObject camObject;
public MultiAimConstraint[] aimObjects;
private Transform aimTarget;
public RigBuilder rig;
public Transform[] leftTargets;
public Transform[] rightTargets;
public Transform[] thumbTargets;
public GameObject[] weapons;
private int weaponNumber = 0;
private GameObject testForWeapons;
private Image weaponIcon;
private Text ammoAmtText;
public Sprite[] weaponIcons;
public int[] ammoAmts;
// Start is called before the first frame update
void Start()
{
weaponIcon = GameObject.Find("WeaponUI").GetComponent<Image>();
ammoAmtText = GameObject.Find("AmmoAmt").GetComponent <Text>();
camObject = GameObject.Find("PlayerCamera");
//aimTarget = GameObject.Find("AimRef").transform;
if (this.gameObject.GetComponent<PhotonView>().IsMine == true)
{
cam = camObject.GetComponent<CinemachineVirtualCamera>();
cam.Follow = this.gameObject.transform;
cam.LookAt = this.gameObject.transform;
//Invoke("SetLookAt", 0.1f);
}
else
{
this.gameObject.GetComponent<PlayerMovement>().enabled = false;
}
testForWeapons = GameObject.Find("Weapon1Pickup(Clone)");
if (testForWeapons == null)
{
var spawner = GameObject.Find("SpawnScript");
spawner.GetComponent<SpawnCharacters>().SpawnWeaponsStart();
}
}
// Update is called once per frame
void Update()
{
if (Input.GetMouseButtonDown(1) &&
this.gameObject.GetComponent<PhotonView>().IsMine == true)
{
//weaponNumber++;
this.GetComponent<PhotonView>().RPC("Change",
RpcTarget.AllBuffered);
if (weaponNumber > weapons.Length - 1)
{
weaponIcon.GetComponent<Image>().sprite = weaponIcons
[0];
ammoAmtText.text = ammoAmts[0].ToString();
weaponNumber = 0;
}
for (int i = 0; i < weapons.Length; i++)
{
weapons[i].SetActive(false);
}
weapons[weaponNumber].SetActive(true);
weaponIcon.GetComponent<Image>().sprite = weaponIcons
[weaponNumber];
ammoAmtText.text = ammoAmts[weaponNumber].ToString();
leftHand.data.target = leftTargets[weaponNumber];
rightHand.data.target = rightTargets[weaponNumber];
leftThumb.data.target = thumbTargets[weaponNumber];
rig.Build();
}
}
[PunRPC]
public void Change()
{
weaponNumber++;
if (weaponNumber > weapons.Length - 1)
{
weaponNumber = 0;
}
for (int i = 0; i < weapons.Length; i++)
{
weapons[i].SetActive(false);
}
weapons[weaponNumber].SetActive(true);

leftHand.data.target = leftTargets[weaponNumber];
rightHand.data.target = rightTargets[weaponNumber];
leftThumb.data.target = thumbTargets[weaponNumber];
rig.Build();
}
}