using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.Animations.Rigging;
using Cinemachine;
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
    public GameObject[] muzzleFlash;

    // Start is called before the first frame update
    void Start()
    {
        weaponIcon = GameObject.Find("WeaponUI").GetComponent<Image>();
        ammoAmtText = GameObject.Find("AmmoAmt").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            SetWeapon(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            SetWeapon(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            SetWeapon(2);
        }
    }

    void SetWeapon(int weaponNumber)
    {
        weaponIcon.GetComponent<Image>().sprite = weaponIcons[weaponNumber];
        ammoAmtText.text = ammoAmts[weaponNumber].ToString();
        weapon1.SetActive(weaponNumber == 0);
        weapon2.SetActive(weaponNumber == 1);
        weapon3.SetActive(weaponNumber == 2);

        leftHand.data.target = GetLeftTarget(weaponNumber);
        rightHand.data.target = GetRightTarget(weaponNumber);
        rig.Build();

        // Call RPC to synchronize muzzle flash across network
        this.GetComponent<PhotonView>().RPC("GunMuzzleFlash", RpcTarget.AllBuffered, weaponNumber);
    }

    Transform GetLeftTarget(int weaponNumber)
    {
        switch (weaponNumber)
        {
            case 0: return leftTargetWeapon1;
            case 1: return leftTargetWeapon2;
            case 2: return leftTargetWeapon3;
            default: return null;
        }
    }

    Transform GetRightTarget(int weaponNumber)
    {
        switch (weaponNumber)
        {
            case 0: return rightTargetWeapon1;
            case 1: return rightTargetWeapon2;
            case 2: return rightTargetWeapon3;
            default: return null;
        }
    }

    [PunRPC]
    void GunMuzzleFlash(int weaponNumber)
    {
        muzzleFlash[weaponNumber].SetActive(true);
        StartCoroutine(MuzzleOff(weaponNumber));
    }

    IEnumerator MuzzleOff(int weaponNumber)
    {
        yield return new WaitForSeconds(0.03f);
        this.GetComponent<PhotonView>().RPC("MuzzleFlashOff", RpcTarget.AllBuffered, weaponNumber);
    }

    [PunRPC]
    void MuzzleFlashOff(int weaponNumber)
    {
        muzzleFlash[weaponNumber].SetActive(false);
    }
}
