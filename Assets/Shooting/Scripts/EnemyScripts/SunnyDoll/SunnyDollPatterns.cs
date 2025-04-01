using System.Collections;
using UnityEngine;

public class SunnyDollPatterns : MonoBehaviour
{
    [Header("Enemy Patterns")]
    [SerializeField] Rainy rainy;
    [SerializeField] SolarEnergySpawn solarEnergySpawn;

    [Header("Enemy Hp")]
    [SerializeField] EnemyHp enemyHp;
    [SerializeField] float healthRatio;

    [Header("Enemy Phase")]
    [SerializeField] float phase2Hp = 0.7f;
    [SerializeField] float phase3Hp = 0.4f;
    [SerializeField] bool onRainy = false;
    [SerializeField] bool onSunny = false;

    [Header("Camera Shake")]
    [SerializeField] CameraShake cameraShake;
    [SerializeField] float duration;    // ī�޶� ��鸮�� �ð�
    [SerializeField] float roughness;   // ī�޶� ��鸲�� ��ĥ��
    [SerializeField] float magnitude;   // ī�޶��� ������ ����

    [Header("Head Sprite")]
    [SerializeField] Sprite normalHead;
    [SerializeField] Sprite sunnyHead;
    [SerializeField] Sprite rainyHead;

    [SerializeField] SpriteRenderer spriteRenderer;

    void Start()
    {
        rainy = GetComponent<Rainy>();
        solarEnergySpawn = GetComponent<SolarEnergySpawn>();
        enemyHp = GetComponent<EnemyHp>();

        cameraShake = GetComponent<CameraShake>();

        spriteRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();

        spriteRenderer.sprite = normalHead;

        StartCoroutine(Co_StartPattern());
    }

    IEnumerator Co_StartPattern()
    {
        while (gameObject.activeSelf)
        {
            healthRatio = enemyHp.enemyHp / enemyHp.maxEnemyHp;

            if (!onSunny && healthRatio <= phase2Hp)
            {
                StartCoroutine(cameraShake.Shake(duration, roughness, magnitude));
                spriteRenderer.sprite = sunnyHead;

                solarEnergySpawn.enabled = true;

                onSunny = true;
            }
            else if (!onRainy && healthRatio <= phase3Hp)
            {
                StartCoroutine(cameraShake.Shake(duration, roughness, magnitude));
                spriteRenderer.sprite = rainyHead;

                solarEnergySpawn.enabled = false;
                rainy.enabled = true;

                onRainy = true;
                break;
            }

            yield return null;
        }
    }
}
