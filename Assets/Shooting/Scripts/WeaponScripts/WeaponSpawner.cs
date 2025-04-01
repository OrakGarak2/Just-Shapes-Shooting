using System.Collections.Generic;
using System.Linq;
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
        string str_curWeapon = GameManager.Instance.curWeapon.ToString();
        GameObject foundWeapon = weaponList.Where(obj => obj.name.Contains(str_curWeapon)).First();
        WeaponSpawn(foundWeapon);
    }

    void WeaponSpawn(GameObject Weapon)
    {
        if (Weapon != null)
        {
            GameObject temp = Instantiate(Weapon, weaponSpawnPoint.localPosition, Weapon.transform.rotation);
            temp.transform.SetParent(GameManager.Instance.trPlayer, false);
        }
    }
}
