using System.Collections;
using UnityEngine;
using JustShapesAndShooting.ObjectPool;

public class MagicBullet : Bullet
{
    [SerializeField] Vector2 StartPos;
    [SerializeField] Vector2 middlePos;

    protected override void Start()
    {
        base.Start();

        bulletDmg = 1.5f;
    }

    protected override void Update()
    {

    }

    public void InitBullet(Vector2 start, Transform enemyTransform, float duration)
    {
        StartPos = start;
        middlePos = start + Random.insideUnitCircle * Random.Range(1f, 4.5f);

        transform.position = StartPos;
        StartCoroutine(moveRoutine());

        IEnumerator moveRoutine()
        {
            float timer = 0f;

            while (timer < duration)
            {
                transform.position = cubicBezier(StartPos, middlePos,
                                                 enemyTransform.position, timer / duration);

                timer += Time.deltaTime;
                yield return null;
            }

            gameObject.SetActive(false);
        }
    }

    Vector2 cubicBezier(Vector2 start, Vector2 medium, Vector2 end, float t)
    {
        Vector2 startToMedium = Vector2.Lerp(start, medium, t);
        Vector2 mediumToEnd = Vector2.Lerp(medium, end, t);

        Vector2 bezierPos = Vector2.Lerp(startToMedium, mediumToEnd, t);
        return bezierPos;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == targetLayer)
        {
            EnemyHp scriptEnemyHp = col.GetComponent<EnemyHp>();
            if (scriptEnemyHp != null)
            {
                scriptEnemyHp.enemyAttacked(bulletDmg);
                ObjectPoolManager.Instance.Release(bulletEnum, gameObject);
            }
        }
    }
}
