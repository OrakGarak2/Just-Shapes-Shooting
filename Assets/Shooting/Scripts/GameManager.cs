using UnityEngine;
using UnityEngine.UI;
using Enums;

namespace Enums
{
    /// <summary>
    /// Player가 선택할 수 있는 마법 도형 enum
    /// </summary>
    public enum MagicShape
    {
        SATOR = 0,
        MoonStar = 1
    }

    /// <summary>
    /// Player가 선택할 수 있는 무기 enum
    /// </summary>
    public enum WeaponEnum
    {
        MiniGun,
        ShotGun,
        MagicWand
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    /// <summary>
    /// Player의 현재 무기를 알려주는 enum
    /// </summary>
    public WeaponEnum curWeapon;

    /// <summary>
    /// Player가 선택한 마법 도형을 알려주는 enum
    /// </summary>
    public MagicShape curMagicShape;

    /// <summary>
    /// 탄환 발사 쿨타임을 표시해주는 UI
    /// </summary>
    public Image cooltimeImage;
    
    /// <summary>
    /// Player Transform
    /// </summary>
    public Transform trPlayer;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            if (PlayerPrefs.HasKey("Weapon"))
            {
                curWeapon = (WeaponEnum)PlayerPrefs.GetInt("Weapon");
            }
            else
            {
                curWeapon = WeaponEnum.MiniGun;
            }

            if (PlayerPrefs.HasKey("MagicShape"))
            {
                curMagicShape = (MagicShape)PlayerPrefs.GetInt("MagicShape");
            }
            else
            {
                curMagicShape = MagicShape.SATOR;
            }
            Screen.SetResolution(1920, 1080, true);

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
