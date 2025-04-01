using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WeaponSpawn : MonoBehaviour
{
    public List<GameObject> weaponList = new List<GameObject>();
    public Transform weaponSpawnPoint;

    void Start()
    {
        WeaponCheck();
    }

    /// <summary>
    /// ���� �����ؾ� �� ������ Ȯ���ϴ� �Լ� + ���� ����
    /// </summary>
    void WeaponCheck()
    {
        string str_curWeapon = GameManager.Instance.curWeapon.ToString();
        GameObject foundWeapon = weaponList.Where(obj => obj.name.Contains(str_curWeapon)).First();
        WeaponSpawnMethod(foundWeapon);
    }

    /// <summary>
    /// �����ؾ� �� ������ Player�� �ڽ� ������Ʈ�� �ְ� 
    /// źȯ ������ ���� bulletGroupSpawner�� Ȱ��ȭ�Ѵ�.
    /// </summary>
    /// <param name="Weapon">�����ؾ� �� ����</param>
    void WeaponSpawnMethod(GameObject Weapon)
    {
        if (Weapon != null)
        {
            GameObject temp = Instantiate(Weapon, weaponSpawnPoint.localPosition, Weapon.transform.rotation);
            temp.transform.SetParent(GameManager.Instance.trPlayer, false);
        }
    }
}
