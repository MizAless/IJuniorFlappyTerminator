using System.Collections;
using UnityEngine;

public abstract class Shooter<OwnerType> : MonoBehaviour
    where OwnerType : MonoBehaviour, IDamagable
{
    [SerializeField] private float _shootCooldown;
    [SerializeField] private Spawner<Projectile<OwnerType>> _projectileSpawner;
    [SerializeField] private Transform _shootPoint;

    private bool _canShoot = true;

    protected float ShootCooldown => _shootCooldown;

    public void Init(Spawner<Projectile<OwnerType>> projectileSpawner)
    {
        _projectileSpawner = projectileSpawner;
        _canShoot = true;
    }

    public void Shoot()
    {
        if (_canShoot == false)
            return;

        _canShoot = false;

        var projectile = _projectileSpawner.Spawn();

        projectile.Init(_shootPoint.position, _shootPoint.rotation);

        StartCoroutine(Reload());
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(_shootCooldown);
        _canShoot = true;
    }
}
