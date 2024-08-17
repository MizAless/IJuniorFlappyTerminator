using System.Collections;
using UnityEngine;

public class EnemyShooter : Shooter<Player>
{
    public IEnumerator Shooting()
    {
        var wait = new WaitForSeconds(base.ShootCooldown);

        while (enabled)
        {
            yield return wait;
            base.Shoot();
        }
    }
}
