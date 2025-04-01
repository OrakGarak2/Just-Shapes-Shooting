using Enums;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponIcon : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField] WeaponEnum thisWeapon;
    [SerializeField] WeaponChoice weaponChoice;

    [SerializeField] Image weaponIcon;
    [SerializeField] GameObject chosenWeapon;

    public WeaponEnum ThisWeapon => thisWeapon;
    public GameObject ChosenWeapon => chosenWeapon;

    void Start()
    {
        weaponIcon = GetComponent<Image>();
    }

    public void SetWeaponChoice(WeaponChoice weaponChoice)
    {
        this.weaponChoice = weaponChoice;
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

        weaponChoice.WeaponSelect(thisWeapon);
    }
}
