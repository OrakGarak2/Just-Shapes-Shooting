using System;
using System.Collections.Generic;
using Enums;
using UnityEngine;

public class WeaponChoice : MonoBehaviour
{
    [SerializeField] private GameObject[] weaponIcons;
    [SerializeField] private GameObject[] chosenWeapons;

    void Start()
    {
        // Enum의 길이만큼 배열 생성
        int length = Enum.GetValues(typeof(WeaponEnum)).Length;
        weaponIcons = new GameObject[length];
        chosenWeapons = new GameObject[length];

        WeaponEnum curWeapon = GameManager.Instance.curWeapon;

        foreach(var weaponIcon in GetComponentsInChildren<WeaponIcon>())
        {
            weaponIcon.SetWeaponChoice(this);

            // Enum 값을 통해 배열에 할당
            int weaponNum = (int)weaponIcon.ThisWeapon;
            weaponIcons[weaponNum] = weaponIcon.gameObject;
            chosenWeapons[weaponNum] = weaponIcon.ChosenWeapon;

            SelectedWeaponUpdate(weaponNum, weaponIcon.ThisWeapon == curWeapon);
        }
    }

    void SelectedWeaponUpdate(int weaponNum, bool isSelected)
    {
        weaponIcons[weaponNum].SetActive(!isSelected);  // 선택된 무기의 아이콘을 비활성화, 선택되지 않았으면 활성화
        chosenWeapons[weaponNum].SetActive(isSelected); // 선택된 무기의 오브젝트를 활성화, 선택되지 않았으면 비활성화
    }

    public void WeaponSelect(WeaponEnum selectedWeapon)
    {
        SelectedWeaponUpdate((int)GameManager.Instance.curWeapon, false);

        SelectedWeaponUpdate((int)selectedWeapon, true);

        GameManager.Instance.curWeapon = selectedWeapon;
        PlayerPrefs.SetInt("Weapon", (int)selectedWeapon);
    }
}
