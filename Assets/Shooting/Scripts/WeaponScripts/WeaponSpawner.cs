using System.Collections.Generic;
using Enums;
using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();
    public Transform weaponSpawnPoint;

    void Start()
    {
        WeaponCheck();
    }

    void WeaponCheck()
    {
        WeaponEnum curWeapon = GameManager.Instance.curWeapon;
        
        foreach (var weapon in weaponList)
        {
            if(weapon.GetComponent<Weapon>().weaponEnum == curWeapon)
            {
                WeaponSpawn(weapon);
            }
        }
    }

    void WeaponSpawn(GameObject weapon)
    {
        if (weapon != null)
        {
            GameObject temp = Instantiate(weapon, weaponSpawnPoint.localPosition, weapon.transform.rotation);
            temp.transform.SetParent(weaponSpawnPoint.parent, false);
        }
    }
}
