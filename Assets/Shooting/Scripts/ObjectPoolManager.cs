using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JustShapesAndShooting.ObjectPool
{
    public enum PoolEnum
    {
        MiniGunBullet,
        ShotGunBullet,
        MagicWandBullet,
        EnemyBullet,
        SolarEnergy,
        Raindrop,

    }

    public class ObjectPoolManager : MonoBehaviour
    {
        public static ObjectPoolManager Instance {get; private set;}

        /// <summary>
        /// 풀에 들어갈 오브젝트들의 프리팹을 모아놓은 딕셔너리
        /// </summary>
        public Dictionary<PoolEnum, GameObject> prefabDictionary = new Dictionary<PoolEnum, GameObject>();

        /// <summary>
        /// enum에 따라 다른 pool을 제공
        /// </summary>
        public Dictionary<PoolEnum, Queue<GameObject>> poolDictionary = new Dictionary<PoolEnum, Queue<GameObject>>();

        private void Awake() {
            if(Instance == null)    Instance = this;
            else                    Destroy(gameObject);
        }

        public void AddPrefabDictionary(PoolEnum poolEnum, GameObject prefab)
        {
            prefabDictionary.Add(poolEnum, prefab);
        }

        public GameObject Get(PoolEnum poolEnum, Vector2 spawnPoint, Quaternion rotation)
        {
            if(!poolDictionary.ContainsKey(poolEnum))
            {
                poolDictionary.Add(poolEnum, new Queue<GameObject>());
            }

            if(poolDictionary[poolEnum].Count > 0)
            {
                GameObject obj = poolDictionary[poolEnum].Dequeue();
                obj.transform.position = spawnPoint;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                return obj;
            }
            else
            {
                return Instantiate(prefabDictionary[poolEnum], spawnPoint, rotation);
            }
        }

        public void Release(PoolEnum poolEnum, GameObject obj)
        {
            if(!poolDictionary.ContainsKey(poolEnum))
            {
                Debug.LogError($"ObjectPoolManager: {obj.name}이라는 잘못된 오브젝트를 풀에 릴리스 시도");
                //Destroy(obj);
                return;
            }
            else
            {
                obj.SetActive(false);
                poolDictionary[poolEnum].Enqueue(obj);
            }
        }
    }

}