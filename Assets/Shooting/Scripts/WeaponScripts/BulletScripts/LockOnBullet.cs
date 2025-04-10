using System.Collections;
using UnityEngine;
using JustShapesAndShooting.ObjectPool;

public class LockOnBullet : Bullet
{
    [SerializeField] MagicWand magicWand;

    public GameObject aimingPoint;

    protected override void Start()
    {
        base.Start();

        bulletSpeed = 30f;
        bulletDmg = 0f;
    }

    public GameObject SetMagicWandAndLockOnBullet(MagicWand magicWand)
    {
        this.magicWand = magicWand;

        return aimingPoint;
    }

    protected override void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == targetLayer)
        {
            gameObject.SetActive(false);
            aimingPoint.transform.SetParent(col.transform, false);
            
            magicWand.LockOnTarget();
        }
    }

    protected override void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.layer == wallLayer && gameObject.activeSelf)
        {
            gameObject.SetActive(false);
        }
    }
}
