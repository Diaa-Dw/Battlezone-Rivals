using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using Cinemachine;
public class WeaponChangeAdvanced : MonoBehaviour {
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
// Start is called before the first frame update
void Start() {
camObject = GameObject.Find("PlayerCamera");
//aimTarget = GameObject.Find("AimRef").transform;
if
(this.gameObject.GetComponent<PhotonView>().IsMine == true
)
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
}
/*
void SetLookAt() {
if (aimTarget != null
)
{
for
(int i = 0; i < aimObjects.Length; i++)
{
var target = aimObjects[i].data.sourceObjects;
target.SetTransform(0, aimTarget.transform);
aimObjects[i].data.sourceObjects = target;
}
rig.Build();
}
}
*/
// Update is called once per frame
void Update() {
if
(Input.GetMouseButtonDown(1))
{
weaponNumber++;
if (weaponNumber > weapons.Length
- 1)
{
weaponNumber = 0;
}
for
(int i = 0; i < weapons.Length; i++)
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
}