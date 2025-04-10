using UnityEngine;
using JustShapesAndShooting.ObjectPool;

public class ShotGun : Weapon
{
    [SerializeField] private int bulletNumber;
    [SerializeField] private float rotationOffset;

    protected override void Start()
    {
        base.Start();
        maxCooltime = 0.7f;
        curCooltime = maxCooltime;
        notFire = false;
    }

    protected override void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !notFire)
        {
            AudioManager.Instance.PlayEffect(shootingSound);

            for(int i = 0; i < bulletNumber; i++)
            {
                firePoint.rotation *= Quaternion.Euler(0, 0, rotationOffset);
                ObjectPoolManager.Instance.Get(bulletEnum, firePoint.position, firePoint.rotation);
            }
            
            firePoint.rotation *= Quaternion.Euler(0, 0, -rotationOffset * bulletNumber);

            notFire = true;
            StartCoroutine(Cooltimer());
        }
    }
}
