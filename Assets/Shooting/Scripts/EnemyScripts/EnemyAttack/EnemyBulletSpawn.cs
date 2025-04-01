using System.Collections;
using System.Collections.Generic;
using JustShapesAndShooting.ObjectPool;
using UnityEngine;

public class EnemyBulletSpawn : MonoBehaviour
{
    [SerializeField] List<Transform> spawnPoints;

    [SerializeField] GameObject bulletPrefab;

    private float timer = 0f;
    private readonly float tick = 0.5f;

    void Start()
    {
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            spawnPoints.Add(child);
        }

        ObjectPoolManager.Instance.AddPrefabDictionary(PoolEnum.EnemyBullet, bulletPrefab);
    }

    private void Update() {

        if(timer > 0f)
        {
            timer -= Time.deltaTime;
        }
        else{
            ObjectPoolManager.Instance.Get(PoolEnum.EnemyBullet, spawnPoints[Random.Range(0, spawnPoints.Count)].position, Quaternion.identity);

            timer = tick;
        }
    }
}
