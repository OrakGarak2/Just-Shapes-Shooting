using System.Collections;
using UnityEngine;

public class Bullet_MiniGun : Bullet
{
    protected override void Start()
    {
        base.Start();

        bulletSpeed = 25.0f;
        bulletDmg = 2.0f;
    }
}