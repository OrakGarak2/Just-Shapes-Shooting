using System.Collections;
using System.Collections.Generic;
using JustShapesAndShooting.ObjectPool;
using Unity.VisualScripting;
using UnityEngine;

public class SolarEnergySpawn : MonoBehaviour
{
    [SerializeField] GameObject solarEnergyPrefab;

    [SerializeField] Transform bgSun;
    [SerializeField] float goalPosY = 2.8f;

    [SerializeField] readonly float delay = 2.0f;

    void Start()
    {
        ObjectPoolManager.Instance.AddPrefabDictionary(PoolEnum.SolarEnergy, solarEnergyPrefab);
        
        StartCoroutine(Co_SolarEnergyAttack());
    }

    IEnumerator Co_SolarEnergyAttack()
    {
        Vector2 goalPos = new Vector2(bgSun.position.x, goalPosY);
        float elapsedTime = 0f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            bgSun.position = Vector2.Lerp(bgSun.position, goalPos, elapsedTime / 1f);

            yield return null;
        }

        elapsedTime = 0f;
        Transform sunLight = bgSun.GetChild(0);
        SpriteRenderer sunLightSpriteRenderer = sunLight.GetComponent<SpriteRenderer>();
        Color sunLightColor = sunLightSpriteRenderer.color;
        sunLightColor.a = 0;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            sunLightColor.a = elapsedTime / 1f;
            sunLightSpriteRenderer.color = sunLightColor;

            yield return null;
        }

        WaitForSeconds waitForSeconds = new WaitForSeconds(delay);

        while (enabled)
        {
            ObjectPoolManager.Instance.Get(PoolEnum.SolarEnergy, transform.position, Quaternion.identity);

            yield return waitForSeconds;
        }
    }
}
