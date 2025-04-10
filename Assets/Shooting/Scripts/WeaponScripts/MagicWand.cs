using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JustShapesAndShooting.ObjectPool;

public class MagicWand : Weapon
{
    /// <summary>
    /// LockOnBullet을 적에게 맞혀서 활성화
    /// </summary>
    [SerializeField] private bool isLockOn;

    [SerializeField] Transform enemyTransform;
    [SerializeField] bool isWithinShootingRange;

    [SerializeField] GameObject lockOnBullet;

    [SerializeField] int targetLayer;
    [SerializeField] GameObject aimingPoint;

    [Header("Attack Range")]
    [SerializeField] CircleCollider2D attackRangeCollider;
    [SerializeField] LineRenderer   lineRenderer;
    [SerializeField][Range(5, 50)]  int polygonPoints;
    [SerializeField]                float radius;

    WaitForSeconds waitForSeconds;

    protected override void Start()
    {
        base.Start();
        targetLayer = LayerData.enemyLayer;

        maxCooltime = 5f;
        curCooltime = maxCooltime;
        notFire = false;
        waitForSeconds = new WaitForSeconds(0.3f);

        lockOnBullet = Instantiate(lockOnBullet);
        aimingPoint = lockOnBullet.GetComponent<LockOnBullet>().SetMagicWandAndLockOnBullet(this);
        lockOnBullet.SetActive(false);

        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.loop = true;

        attackRangeCollider = GetComponent<CircleCollider2D>();
        radius = attackRangeCollider.radius / 2;
    }

    protected override void Update()
    {
        base.Update();

        DrawAttackRange();
    }

    protected override void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !notFire)
        {
            if(!isLockOn) // 조준 중이 아니라면
            {
                lockOnBullet.transform.position = firePoint.position;
                lockOnBullet.transform.rotation = firePoint.rotation;

                lockOnBullet.SetActive(true); // lockOnBullet 발사
                AudioManager.Instance.PlayEffect(shootingSound);

                StartCoroutine(Cooltimer());
            }
            else if(isWithinShootingRange) // 조준 중이고 범위 안에 적이 있다면
            {
                GameObject magicBullet = ObjectPoolManager.Instance.Get(bulletEnum, firePoint.position, firePoint.rotation);
                magicBullet.GetComponent<MagicBullet>().InitBullet(firePoint.position, enemyTransform, 0.5f); // magicBullet 발사
                AudioManager.Instance.PlayEffect(shootingSound);

                StartCoroutine(Co_Wait());
            }
        }
    }

    protected override IEnumerator Cooltimer()
    {
        notFire = true;

        while (curCooltime > 0)
        {
            curCooltime -= Time.deltaTime;

            cooltimeImage.fillAmount = curCooltime / maxCooltime;

            yield return waitForFixedUpdate;
        }
        
        aimingPoint.SetActive(false);
        isLockOn = false; // 쿨타임 도중에만 MagicBullet 발사 가능
        notFire = false;

        attackRangeCollider.enabled = false;    
        lineRenderer.enabled = false;
        
        curCooltime = maxCooltime;
    }

    IEnumerator Co_Wait()
    {
        notFire = true;
        yield return waitForSeconds;
        notFire = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == targetLayer)
        {
            enemyTransform = collision.transform;
            isWithinShootingRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.layer == targetLayer)
        {
            isWithinShootingRange = false;
        }
    }

    public void LockOnTarget()
    {
        isLockOn = true;
        notFire = false;

        attackRangeCollider.enabled = true;
        lineRenderer.enabled = true;

        aimingPoint.SetActive(true);
    }

    private void DrawAttackRange()
    {
        if(!lineRenderer.enabled) return;
        
        lineRenderer.positionCount = polygonPoints;

        float anglePerStep = 2 * Mathf.PI / polygonPoints;

        for(int i = 0; i < polygonPoints; i++)
        {
            Vector2 point = transform.position;
            float angle = i * anglePerStep;

            point.x += Mathf.Cos(angle) * radius;
            point.y += Mathf.Sin(angle) * radius;

            lineRenderer.SetPosition(i, point);
        }
    }
}
