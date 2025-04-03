using JustShapesAndShooting.ObjectPool;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float bulletSpeed;
    public float bulletDmg;

    [SerializeField] protected PoolEnum bulletEnum;
    protected int targetLayer;
    protected int WallLayer;

    protected virtual void Start()
    {
        targetLayer = LayerMask.NameToLayer("Enemy");
        WallLayer = LayerMask.NameToLayer("Wall");
    }

    protected virtual void Update()
    {
        BulletMove();
    }

    /// <summary>
    /// 탄환 이동동
    /// </summary>
    protected virtual void BulletMove()
    {
        float z = transform.rotation.eulerAngles.z + 90;
        Vector2 direction = new Vector2(Mathf.Cos(z * Mathf.Deg2Rad), Mathf.Sin(z * Mathf.Deg2Rad));
        GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == targetLayer)
        {
            EnemyHp enemyHp = col.GetComponent<EnemyHp>();
            if (enemyHp != null)
            {
                enemyHp.enemyAttacked(bulletDmg);
                Release();
            }
        }
    }

    /// <summary>
    /// 벽에 닿자마자 사라지면 부자연스럽기 때문에 벽과의 충돌이 끝날 때(화면에서 벗어난 상태일 때) 릴리스
    /// </summary>
    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer == WallLayer && gameObject.activeSelf)
        {
            Release();
        }
    }

    protected virtual void Release()
    {
        ObjectPoolManager.Instance.Release(bulletEnum, gameObject);
    }
}
