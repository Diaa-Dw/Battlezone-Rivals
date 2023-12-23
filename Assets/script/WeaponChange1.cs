using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.UI;

public class WeaponChangeBeginner : MonoBehaviour
{
public TwoBoneIKConstraint leftHand;
public TwoBoneIKConstraint rightHand;
public RigBuilder rig;
public Transform leftTargetWeapon1;
public Transform rightTargetWeapon1;
public Transform leftTargetWeapon2;
public Transform rightTargetWeapon2;
public Transform leftTargetWeapon3;
public Transform rightTargetWeapon3;
public GameObject weapon1;
public GameObject weapon2;
public GameObject weapon3;
private Image weaponIcon;
private Text ammoAmtText;
public Sprite[] weaponIcons;
public int[] ammoAmts;
// Start is called before the first frame update
void Start()
{
    weaponIcon = GameObject.Find("WeaponUI").GetComponent<Image>();
    ammoAmtText = GameObject.Find("AmmoAmt").GetComponent <Text>();
}
// Update is called once per frame
void Update()
{
if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown
(KeyCode.Keypad1))
{
weaponIcon.GetComponent<Image>().sprite = weaponIcons[0];
ammoAmtText.text = ammoAmts[0].ToString();
weapon1.SetActive(true);
weapon2.SetActive(false);
weapon3.SetActive(false);
leftHand.data.target = leftTargetWeapon1;
rightHand.data.target = rightTargetWeapon1;
rig.Build();
}
if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown
(KeyCode.Keypad2))
{
weaponIcon.GetComponent<Image>().sprite = weaponIcons[1];
ammoAmtText.text = ammoAmts[1].ToString();
weapon1.SetActive(false);
weapon2.SetActive(true);
weapon3.SetActive(false);
leftHand.data.target = leftTargetWeapon2;
rightHand.data.target = rightTargetWeapon2;
rig.Build();
}
if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown
(KeyCode.Keypad3))
{
weaponIcon.GetComponent<Image>().sprite = weaponIcons[2];
ammoAmtText.text = ammoAmts[2].ToString();
weapon1.SetActive(false);
weapon2.SetActive(false);
weapon3.SetActive(true);
leftHand.data.target = leftTargetWeapon3;
rightHand.data.target = rightTargetWeapon3;
rig.Build();
}
}
}