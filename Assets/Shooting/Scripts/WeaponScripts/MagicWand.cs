using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JustShapesAndShooting.ObjectPool;

public class MagicWand : Weapon
{
    /// <summary>
    /// LockOnBullet을 적에게 맞혀서 활성화화
    /// </summary>
    [SerializeField] private bool isLockOn;

    [SerializeField] Transform enemyTransform;
    [SerializeField] bool isWithinShootingRange;

    [SerializeField] GameObject lockOnBullet;

    [SerializeField] int targetLayer;
    [SerializeField] GameObject aimingPoint;

    WaitForSeconds waitForSeconds;

    protected override void Start()
    {
        base.Start();
        targetLayer = LayerMask.NameToLayer("Enemy");

        maxCooltime = 5f;
        curCooltime = maxCooltime;
        notFire = false;
        waitForSeconds = new WaitForSeconds(0.1f);

        lockOnBullet = Instantiate(lockOnBullet);
        aimingPoint = lockOnBullet.GetComponent<LockOnBullet>().SetMagicWandAndLockOnBullet(this);
        lockOnBullet.SetActive(false);
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

            yield return wffu;
        }
        
        aimingPoint.SetActive(false);
        isLockOn = false; // 쿨타임 도중에만 MagicBullet 발사 가능
        notFire = false; 
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

        aimingPoint.SetActive(true);
    }
}
