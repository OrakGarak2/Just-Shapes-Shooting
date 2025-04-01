using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using JustShapesAndShooting.ObjectPool;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected Image cooltimeImage;

    protected WaitForFixedUpdate waitForFixedUpdate = new WaitForFixedUpdate();

    /// <summary>
    /// 현재 남은 다음 공격까지의 시간
    /// </summary>
    protected float curCooltime;

    /// <summary>
    /// 이 웨폰의 공격 속도를 알려준다.
    /// </summary>
    protected float maxCooltime;

    /// <summary>
    /// 이 웨폰이 공격할 수 있는 상태인지 아닌지 정하는 함수
    /// (true일 경우 공격 불가, false일 경우 공격 가능)
    /// </summary>
    [SerializeField] protected bool notFire;

    /// <summary>
    /// 탄환이 발사되는 곳
    /// </summary>
    [SerializeField] protected Transform firePoint;

    [SerializeField] protected GameObject bulletPrefab;

    /// <summary>
    /// 이 무기가 발사할 탄환 enum
    /// </summary>
    [SerializeField] protected PoolEnum bulletEnum;

    [SerializeField] protected AudioClip shootingSound;

    protected virtual void Start()
    {
        ObjectPoolManager.Instance.AddPrefabDictionary(bulletEnum, bulletPrefab);
        cooltimeImage = GameManager.Instance.cooltimeImage;
    }

    protected virtual void Update()
    {
        Shoot();
    }

    /// <summary>
    /// 다음 공격까지 남은 시간을 계산하고 그 남은 시간을 보여주는 함수
    /// </summary>
    /// <returns></returns>
    protected virtual IEnumerator Cooltimer()
    {
        while (curCooltime > 0)
        {
            curCooltime -= Time.deltaTime;

            cooltimeImage.fillAmount = curCooltime / maxCooltime;

            yield return waitForFixedUpdate;
        }
        notFire = false;
        curCooltime = maxCooltime;
    }

    /// <summary>
    /// 총알 발사 함수
    /// (Q키를 눌렀을 때 공격 가능 상태라면 총알 발사)
    /// </summary>
    protected virtual void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !notFire)
        {
            AudioManager.Instance.PlayEffect(shootingSound);
            Debug.Log("Q");

            ObjectPoolManager.Instance.Get(bulletEnum, firePoint.position, firePoint.rotation);

            notFire = true;
            StartCoroutine(Cooltimer());
        }
    }
}
