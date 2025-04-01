using System.Collections.Generic;
using UnityEngine;

public class SelectedWeapon : MonoBehaviour
{
    [SerializeField] private List<GameObject> weapons = new List<GameObject>();
    [SerializeField] private List<GameObject> weaponIcons = new List<GameObject>();

    void Start()
    {
        int curWeaponNum = (int)GameManager.Instance.curWeapon;

        for (int i = 0; i < weapons.Count; i++)
        {
            if (i != curWeaponNum) continue;

            weapons[i].SetActive(true);
            weaponIcons[i].SetActive(false);
        }
    }
}
