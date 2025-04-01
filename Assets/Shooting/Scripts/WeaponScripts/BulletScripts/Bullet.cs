using JustShapesAndShooting.ObjectPool;
using UnityEngine;

public class Bullet : MonoBehaviour // Bullet Ŭ����
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
    /// �Ѿ� �̵� �Լ�
    /// </summary>
    protected virtual void BulletMove()
    {
        float z = transform.rotation.eulerAngles.z + 90;
        Vector2 direction = new Vector2(Mathf.Cos(z * Mathf.Deg2Rad), Mathf.Sin(z * Mathf.Deg2Rad));
        GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }

    /// <summary>
    /// źȯ�� Enemy�� �������� ȣ���ؼ�
    /// bulletDmg��ŭ enemyHp�� ����ų �� �ֵ���
    /// enemyAttacked�� ȣ���ϴ� �Լ�
    /// </summary>
    protected virtual void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == targetLayer)
        {
            EnemyHp enemyHp = col.GetComponent<EnemyHp>();
            if (enemyHp != null)
            {
                enemyHp.enemyAttacked(bulletDmg);
                ObjectPoolManager.Instance.Release(bulletEnum, gameObject);
            }
        }
    }

    /// <summary>
    /// źȯ�� ���� ���ڸ��� ������� �÷��̾� ���忡�� 
    /// ���ڱ� �Ѿ��� �����ϴ� ��ó�� ���̱⿡ ȭ�� �ۿ��� ��������� Exit ���
    /// </summary>
    protected virtual void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer == WallLayer && gameObject.activeSelf)
        {
            ObjectPoolManager.Instance.Release(bulletEnum, gameObject);
        }
    }
}
