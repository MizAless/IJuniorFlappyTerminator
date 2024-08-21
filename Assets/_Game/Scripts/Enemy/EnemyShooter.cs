using System.Collections;
using UnityEngine;

public class EnemyShooter : Shooter<Enemy>
{
    public void StartShooting()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        var wait = new WaitForSeconds(base.ShootCooldown);

        while (enabled)
        {
            yield return wait;
            base.Shoot();
        }
    }
}
