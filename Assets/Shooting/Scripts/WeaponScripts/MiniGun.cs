using JustShapesAndShooting.ObjectPool;

public class MiniGun : Weapon
{
    protected override void Start()
    {
        base.Start();
        maxCooltime = 0.3f;
        curCooltime = maxCooltime;

    }
}
