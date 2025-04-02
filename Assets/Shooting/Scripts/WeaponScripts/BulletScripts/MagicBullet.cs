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

        bulletDmg = 2.5f;
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
        /* 
        Vector2 startToMedium = Vector2.Lerp(start, medium, t);
        Vector2 mediumToEnd = Vector2.Lerp(medium, end, t);

        Vector2 bezierPos = Vector2.Lerp(startToMedium, mediumToEnd, t);
        return bezierPos;
        */
        
        // Lerp를 계속 호출하는 위의 코드보다 
        // 곱셈과 덧셈 연산만을 사용하는 아래의 코드가 더 효율적임.

        return  (1 - t) * (1 - t) * start
                + 2 * (1 - t) * t * medium
                + t * t * end;
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
