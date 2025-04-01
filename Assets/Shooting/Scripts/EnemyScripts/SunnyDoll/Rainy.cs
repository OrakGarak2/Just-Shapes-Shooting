using System.Collections;
using System.Collections.Generic;
using JustShapesAndShooting.ObjectPool;
using UnityEngine;

public class Rainy : MonoBehaviour
{
    [Header("�����")]
    [SerializeField] GameObject raindropPrefab;
    readonly float raindropSpawnPosY = 3.7f;
    readonly float raindropDelayTime = 0.2f;
    
    [SerializeField] float rainySize = 12f;

    [Header("���")]
    [SerializeField] Transform cloud1;
    [SerializeField] Transform cloud2;
    [SerializeField] GameObject bgSun;
    readonly float cloudGoalPosX = 4.3f;

    void Awake()
    {
        ObjectPoolManager.Instance.AddPrefabDictionary(PoolEnum.Raindrop ,raindropPrefab);
    }

    private void OnEnable()
    {
        StartCoroutine(RaindropSpawn());
    }

    /// <summary>
    /// raindrop�� ������ ��ġ���� Ȱ��ȭ��Ű�� �ڷ�ƾ
    /// </summary>
    IEnumerator RaindropSpawn()
    {
        Vector2 goalPos1 = new Vector2(cloudGoalPosX, cloud1.position.y);
        Vector2 goalPos2 = new Vector2(-cloudGoalPosX, cloud1.position.y);
        float elapsedTime = 0f; // ��� �ð�

        Transform sunLight = bgSun.transform.GetChild(0);
        SpriteRenderer sunLightSpriteRenderer = sunLight.GetComponent<SpriteRenderer>();
        Color sunLightColor = sunLightSpriteRenderer.color;
        sunLightColor.a = 1f;

        while (elapsedTime < 1f)
        {
            elapsedTime += Time.deltaTime;
            cloud1.position = Vector2.Lerp(cloud1.position, goalPos1, elapsedTime / 1f);
            cloud2.position = Vector2.Lerp(cloud2.position, goalPos2, elapsedTime / 1f);
            sunLightColor.a = 1f - elapsedTime / 1f;
            sunLightSpriteRenderer.color = sunLightColor;

            yield return null;
        }

        bgSun.SetActive(false);

        float ran = 0;

        WaitForSeconds raindropDelay = new WaitForSeconds(raindropDelayTime);

        while (gameObject.activeSelf) 
        {
            ran = Random.Range(-rainySize * 0.5f, rainySize * 0.5f);

            ObjectPoolManager.Instance.Get(PoolEnum.Raindrop, new Vector2(ran, raindropSpawnPosY), Quaternion.identity);

            yield return raindropDelay;
        }
    }
}
