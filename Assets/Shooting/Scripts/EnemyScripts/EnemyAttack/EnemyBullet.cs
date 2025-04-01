using System.Collections;
using System.Collections.Generic;
using JustShapesAndShooting.ObjectPool;
using UnityEngine;

public class EnemyBullet : Bullet
{
    Vector2 direction;

    [SerializeField] bool alreadyPassWall = false;

    protected override void Start()
    {
        targetLayer = LayerMask.NameToLayer("Player");
        WallLayer = LayerMask.NameToLayer("Wall");
    }

    protected void OnEnable ()
    {
        alreadyPassWall = false;
        direction = (GameManager.Instance.trPlayer.position - transform.position).normalized;

        // 탄환이 플레이어를 바라보게 만듦.
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    protected override void BulletMove()
    {
        GetComponent<Rigidbody2D>().velocity =  direction * bulletSpeed;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == targetLayer)
        {
            col.GetComponent<PlayerHp>().CheckDamage(bulletDmg, false);
            ObjectPoolManager.Instance.Release(bulletEnum, gameObject);
        }
    }

    protected override void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer == WallLayer && gameObject.activeSelf)
        {
            if(!alreadyPassWall)    alreadyPassWall = true;
            else                    ObjectPoolManager.Instance.Release(bulletEnum, gameObject);
        }
    }
}
