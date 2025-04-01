using UnityEngine;
using UnityEngine.UI;
using Enums;

namespace Enums
{
    /// <summary>
    /// Player�� ����� �� �ִ� ������ & ������ ���
    /// </summary>
    public enum MagicShape
    {
        SATOR = 0,
        MoonStar = 1
    }

    /// <summary>
    /// Player�� ������ �� �ִ� ���� ���
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
    /// Player�� ���� �����ϰ� �ִ� ����
    /// </summary>
    public WeaponEnum curWeapon;

    /// <summary>
    /// Player�� ���� ����ϰ� �ִ� ������ �Ǵ� ������
    /// </summary>
    public MagicShape curMagicShape;

    /// <summary>
    /// ���� ��Ÿ�� �̹���
    /// </summary>
    public Image cooltimeImage;
    
    /// <summary>
    /// Player�� Transform
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
