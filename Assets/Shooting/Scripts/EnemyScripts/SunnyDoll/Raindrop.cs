using System.Collections;
using System.Collections.Generic;
using JustShapesAndShooting.ObjectPool;
using UnityEngine;
using UnityEngine.UIElements;

public class Raindrop : MonoBehaviour
{ 
    [SerializeField] int playerLayer;
    [SerializeField] int wallLayer;

    [SerializeField] float raindropDamage;

    private void Start() {
        playerLayer = LayerMask.NameToLayer("Player");
        wallLayer = LayerMask.NameToLayer("Wall");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == playerLayer)
        {
            collision.GetComponent<PlayerHp>().CheckDamage(raindropDamage, false);
            ObjectPoolManager.Instance.Release(PoolEnum.Raindrop, gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == wallLayer)
        {
            ObjectPoolManager.Instance.Release(PoolEnum.Raindrop, gameObject);
        }
    }
}
