using UnityEngine;

public class Bullet_ShotGun : Bullet
{
    protected override void Start()
    {
        base.Start();

        bulletSpeed = 30.0f;
        bulletDmg = 1.2f;
    }
}
