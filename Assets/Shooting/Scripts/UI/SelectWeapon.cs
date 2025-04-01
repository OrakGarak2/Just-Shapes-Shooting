using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectWeapon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] YourWeapons yourWeapons;

    [SerializeField] Image weaponIcon;
    [SerializeField] Transform weaponIcons; // ���� �����ܵ��� �θ� ������Ʈ

    [SerializeField] GameObject weapon; // UI�� �ƴ� ���� ������Ʈ
    [SerializeField] Transform weapons; // UI�� �ƴ� ���� ������Ʈ���� �θ� ������Ʈ

    void Start()
    {
        weaponIcon = GetComponent<Image>();
    }

    void Select()
    {
        foreach(Transform t in weaponIcons.GetComponentInChildren<Transform>())
        {
            if (t == weaponIcons || t == transform) { continue; } // t�� weaponIcons�̰ų� �ڽ��̶�� continue

            if (!t.gameObject.activeSelf) { t.gameObject.SetActive(true); } // t�� ��Ȱ��ȭ ���¶�� Ȱ��ȭ
        }

        foreach (Transform t in weapons.GetComponentInChildren<Transform>())
        {
            if (t == weapons || t == weapon.transform) { continue; } // t�� weapons�̰ų� weapon.transform�̶�� continue

            if (t.gameObject.activeSelf) { t.gameObject.SetActive(false); } // t�� Ȱ��ȭ ���¶�� ��Ȱ��ȭ
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Color color = weaponIcon.color;
        color.a = 1f;

        weaponIcon.color = color;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Color color = weaponIcon.color;
        color.a = 0.3f;

        weaponIcon.color = color;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Color color = weaponIcon.color;
        color.a = 0.3f;
        weaponIcon.color = color;

        GameManager.Instance.curWeapon = yourWeapons;
        PlayerPrefs.SetInt("Weapon", (int)yourWeapons);

        gameObject.SetActive(false);
        weapon.SetActive(true);
        Select();
    }
}
